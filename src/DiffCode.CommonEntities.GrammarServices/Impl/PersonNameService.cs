using DiffCode.CommonEntities.Abstractions;
using DiffCode.CommonEntities.Enums;
using DiffCode.CommonEntities.Grammars;
using DiffCode.CommonEntities.Persons;
using DiffCode.CommonEntities.Services;
using System.Text.RegularExpressions;
using static DiffCode.CommonEntities.Abstractions.Case;


namespace DiffCode.CommonEntities.GrammarServices.Impl;

/// <summary>
/// Сервис для работы с именами, отчествами и фамилиями.
/// </summary>
public class PersonNameService : IPersonNameService
{

  



  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="gender"></param>
  /// <param name="namePart"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public BaseGrammar GetGrammarForPersonNamePart(string text, Gender gender, NamePart namePart) => (gender, namePart) switch
  {
    (Gender.M, NamePart.FIRST) => MFirstGrammars.FirstOrDefault(f => GrammarGetter.Invoke(f, text)),
    (Gender.M, NamePart.MID) => MMidGrammars.FirstOrDefault(f => GrammarGetter.Invoke(f, text)),
    (Gender.M, NamePart.LAST) => MLastGrammars.FirstOrDefault(f => GrammarGetter.Invoke(f, text)),

    (Gender.F, NamePart.FIRST) => FFirstGrammars.FirstOrDefault(f => GrammarGetter.Invoke(f, text)),
    (Gender.F, NamePart.MID) => FMidGrammars.FirstOrDefault(f => GrammarGetter.Invoke(f, text)),
    (Gender.F, NamePart.LAST) => FLastGrammars.FirstOrDefault(f => GrammarGetter.Invoke(f, text)),

    (_, _) => throw new NotImplementedException()
  };

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public IEnumerable<BasePersonNamePart> GetPersonNameParts(string input)
  {
    var ret = new List<BasePersonNamePart>();
    var strings = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList().Index();
    var midNameGrammars = MMidGrammars.Concat(FMidGrammars.Cast<BaseGrammar>());

    var midNameGrammar = midNameGrammars.FirstOrDefault(f => strings.Any(s => Regex.IsMatch(s.Item, $".?{f.End}$")));
    var midNameString = strings.FirstOrDefault(s => Regex.IsMatch(s.Item, $".?{midNameGrammar.End}$"));

    string lastNameString;
    string firstNameString;
    BaseGrammar lastNameGrammar;
    BaseGrammar firstNameGrammar;
    BaseFirstNamePart firstNamePart;
    BaseLastNamePart lastNamePart;

    BaseMidNamePart foundMidName = midNameGrammar.Gender switch
    {
      Gender.M => new MMidPart(null, midNameString.Item) { Grammar = midNameGrammar },
      Gender.F => new FMidPart(null, midNameString.Item) { Grammar = midNameGrammar },
      _ => throw new NotImplementedException(),
    };

    switch (midNameString.Index)
    {
      case 1:
        lastNameString = strings.FirstOrDefault(f => f.Index == 2).Item;
        lastNameGrammar = GetGrammarForPersonNamePart(lastNameString, foundMidName.Gender, NamePart.LAST);
        firstNameString = strings.FirstOrDefault(f => f.Index == 0).Item;
        firstNameGrammar = GetGrammarForPersonNamePart(firstNameString, foundMidName.Gender, NamePart.FIRST);
        break;

      case 2:
        lastNameString = strings.FirstOrDefault(f => f.Index == 0).Item;
        lastNameGrammar = GetGrammarForPersonNamePart(lastNameString, foundMidName.Gender, NamePart.LAST);
        firstNameString = strings.FirstOrDefault(f => f.Index == 1).Item;
        firstNameGrammar = GetGrammarForPersonNamePart(firstNameString, foundMidName.Gender, NamePart.FIRST);
        break;

      default:
        throw new NotImplementedException();
    };

    firstNamePart = foundMidName.Gender switch
    {
      Gender.M => new MFirstPart(firstNameString, null, null) { Grammar = firstNameGrammar },
      Gender.F => new FFirstPart(firstNameString) { Grammar = firstNameGrammar },
      _ => throw new NotImplementedException(),
    };

    lastNamePart = foundMidName.Gender switch
    {
      Gender.M => new MLastPart(lastNameString) with { Grammar = lastNameGrammar },
      Gender.F => new FLastPart(lastNameString) with { Grammar = lastNameGrammar },
      _ => throw new NotImplementedException()
    };

    ret.AddRange(firstNamePart, foundMidName, lastNamePart);

    return ret;
  }



  /// <summary>
  /// Найдено 1 пересечение с женским отчеством.
  /// </summary>
  /// <param name="parserData"></param>
  /// <returns></returns>
  private ParserData WhenFMidNameFound(ParserData parserData, params BaseMidNamePart[] midNameParts)
  {
    parserData.MidNamePart = midNameParts[0];
    parserData.CurrentFragments.Remove(parserData.MidNamePart.Text);
    var firstNameGrammars = FFirstGrammars;
    var foundFirstNameGrammar = firstNameGrammars.FirstOrDefault(f => f.Gender == parserData.Gender && parserData.CurrentFragments.Any(s => Regex.IsMatch(s, $".?{f.End}$")));
    var foundFirstNameString = parserData.CurrentFragments.FirstOrDefault(s => Regex.IsMatch(s, $".?{foundFirstNameGrammar.End}$"));
    parserData.FirstNamePart = new FFirstPart(foundFirstNameString) { Grammar = foundFirstNameGrammar };
    parserData.CurrentFragments.Remove(foundFirstNameString);

    return parserData;
  }


  /// <summary>
  /// Найдено 1 пересечение с мужским отчеством.
  /// </summary>
  /// <param name="parserData"></param>
  /// <returns></returns>
  private ParserData WhenMMidNameFound(ParserData parserData, params BaseMidNamePart[] midNameParts)
  {
    parserData.MidNamePart = midNameParts[0];
    parserData.CurrentFragments.Remove(parserData.MidNamePart.Text);
    var firstNameGrammars = MFirstGrammars;
    var foundFirstNameGrammar = firstNameGrammars.FirstOrDefault(f => f.Gender == parserData.Gender && parserData.CurrentFragments.Any(s => Regex.IsMatch(s, $".?{f.End}$")));
    var foundFirstNameString = parserData.CurrentFragments.FirstOrDefault(s => Regex.IsMatch(s, $".?{foundFirstNameGrammar.End}$"));
    parserData.FirstNamePart = new MFirstPart(foundFirstNameString, null, null) { Grammar = foundFirstNameGrammar };
    parserData.CurrentFragments.Remove(foundFirstNameString);

    return parserData;
  }


  /// <summary>
  /// Найдено 1 пересечение с мужским отчеством и 1 пересечение с женским отчеством.
  /// </summary>
  /// <param name="parserData"></param>
  /// <returns></returns>
  private ParserData WhenMMidNameFMidNameFound(ParserData parserData, params BaseMidNamePart[] midNameParts)
  {
    parserData.MidNamePart = midNameParts.FirstOrDefault(f => f.Gender == Gender.F);
    parserData.CurrentFragments.Remove(parserData.MidNamePart.Text);
    var firstNameGrammars = FFirstGrammars;
    var foundFirstNameGrammar = firstNameGrammars.FirstOrDefault(f => f.Gender == parserData.Gender && parserData.CurrentFragments.Any(s => Regex.IsMatch(s, $".?{f.End}$")));
    var foundFirstNameString = parserData.CurrentFragments.FirstOrDefault(s => Regex.IsMatch(s, $".?{foundFirstNameGrammar.End}$"));
    parserData.FirstNamePart = new FFirstPart(foundFirstNameString) { Grammar = foundFirstNameGrammar };
    parserData.CurrentFragments.Remove(foundFirstNameString);


    return parserData;
  }


  /// <summary>
  /// Найдено 2 пересечения с мужскими отчествами.
  /// </summary>
  /// <param name="parserData"></param>
  /// <returns></returns>
  private ParserData WhenTwoMMidNamesFound(ParserData parserData, params BaseMidNamePart[] midNameParts)
  {
    if(!midNameParts.Any(a => parserData.InitialFragments[0] == a.Text))
    {
      parserData.MidNamePart = midNameParts.FirstOrDefault();
    }
    else
    {
      parserData.MidNamePart = midNameParts.LastOrDefault();
    }

    parserData.CurrentFragments.Remove(parserData.MidNamePart.Text);
    var firstNameGrammars = MFirstGrammars;
    var foundFirstNameGrammar = firstNameGrammars.FirstOrDefault(f => f.Gender == parserData.Gender && parserData.CurrentFragments.Any(s => Regex.IsMatch(s, $".?{f.End}$")));
    var foundFirstNameString = parserData.CurrentFragments.FirstOrDefault(s => Regex.IsMatch(s, $".?{foundFirstNameGrammar.End}$"));
    parserData.FirstNamePart = new MFirstPart(foundFirstNameString, null, null) { Grammar = foundFirstNameGrammar };
    parserData.CurrentFragments.Remove(foundFirstNameString);


    return parserData;
  }


  /// <summary>
  /// Не найдено ни одного пересечения ни с одним отчеством.
  /// </summary>
  /// <param name="parserData"></param>
  /// <returns></returns>
  private ParserData WhenNoMidNamesFound(ParserData parserData)
  {


    return parserData;
  }














  /// <summary>
  /// Предикат для поиска грамматики, соответствующей указанной строке. 
  /// </summary>
  private Func<BaseGrammar, string, bool> GrammarGetter => (gr, txt) => Regex.IsMatch(txt, $".?{gr.End}$");

  /// <summary>
  /// Грамматики для мужских имен.
  /// </summary>
  private List<MFirst> MFirstGrammars { get; } = 
    PersonNameGrammars.MFirstNames
    .Select(s => new MFirst(s.Key, NOM(s.Value.Item1), GEN(s.Value.Item2), DAT(s.Value.Item3), ACC(s.Value.Item4), INS(s.Value.Item5), LOC(s.Value.Item6)))
    .ToList()
    ;

  /// <summary>
  /// Грамматики для женских имен.
  /// </summary>
  private List<FFirst> FFirstGrammars { get; } =
    PersonNameGrammars.FFirstNames
    .Select(s => new FFirst(s.Key, NOM(s.Value.Item1), GEN(s.Value.Item2), DAT(s.Value.Item3), ACC(s.Value.Item4), INS(s.Value.Item5), LOC(s.Value.Item6)))
    .ToList()
    ;

  /// <summary>
  /// Грамматики для мужских отчеств.
  /// </summary>
  private List<MMid> MMidGrammars { get; } = 
    PersonNameGrammars.MMidNames
    .Select(s => new MMid(s.Key, NOM(s.Value.Item1), GEN(s.Value.Item2), DAT(s.Value.Item3), ACC(s.Value.Item4), INS(s.Value.Item5), LOC(s.Value.Item6)))
    .ToList()
    ;

  /// <summary>
  /// Грамматики для женских отчеств.
  /// </summary>
  private List<FMid> FMidGrammars { get; } = 
    PersonNameGrammars.FMidNames
    .Select(s => new FMid(s.Key, NOM(s.Value.Item1), GEN(s.Value.Item2), DAT(s.Value.Item3), ACC(s.Value.Item4), INS(s.Value.Item5), LOC(s.Value.Item6)))
    .ToList()
    ;

  /// <summary>
  /// Грамматики для мужских фамилий.
  /// </summary>
  private List<MLast> MLastGrammars { get; } =
    PersonNameGrammars.MLastNames
    .Select(s => new MLast(s.Key, NOM(s.Value.Item1), GEN(s.Value.Item2), DAT(s.Value.Item3), ACC(s.Value.Item4), INS(s.Value.Item5), LOC(s.Value.Item6)))
    .ToList()
    ;

  /// <summary>
  /// Грамматики для женских фамилий.
  /// </summary>
  private List<FLast> FLastGrammars { get; } =
    PersonNameGrammars.FLastNames
    .Select(s => new FLast(s.Key, NOM(s.Value.Item1), GEN(s.Value.Item2), DAT(s.Value.Item3), ACC(s.Value.Item4), INS(s.Value.Item5), LOC(s.Value.Item6)))
    .ToList()
    ;

  /// <summary>
  /// Мужские имена в виде объектных моделей, преобразованных из строки с текстом.
  /// </summary>
  private List<MFirstPart> MFirstParts => PersonNames.MNames
    .Select(s => new MFirstPart(s.Item1, s.Item2, s.Item3))
    .Select(s => s with { Grammar = GetGrammarForPersonNamePart(s.Text, s.Gender, s.Part) })
    .ToList()
    ;

  /// <summary>
  /// Женские имена в виде объектных моделей, преобразованных из строки с текстом.
  /// </summary>
  private List<FFirstPart> FFirstParts => PersonNames.FNames
    .Select(s => new FFirstPart(s))
    .Select(s => s with { Grammar = GetGrammarForPersonNamePart(s.Text, s.Gender, s.Part )})
    .ToList()
    ;

  /// <summary>
  /// Мужские отчества в виде объектных моделей, преобразованных из строки с текстом.
  /// </summary>
  private List<MMidPart> MMidParts => MFirstParts
    .SelectMany(s => s.Derived.Where(w => w.Gender == Gender.M).Cast<MMidPart>())
    .Select(s => s with { Grammar = GetGrammarForPersonNamePart(s.Text, s.Gender, s.Part) })
    .ToList()
    ;

  /// <summary>
  /// Женские отчества в виде объектных моделей, преобразованных из строки с текстом.
  /// </summary>
  private List<FMidPart> FMidParts => MFirstParts
    .SelectMany(s => s.Derived.Where(w => w.Gender == Gender.F).Cast<FMidPart>())
    .Select(s => s with { Grammar = GetGrammarForPersonNamePart(s.Text, s.Gender, s.Part) })
    .ToList()
    ;










  /// <summary>
  /// Вспомогательная модель данных для парсера.
  /// </summary>
  private class ParserData
  {


    /// <summary>
    /// Часть имени - личное имя, найденное в списке известных мужских/женских личных имен,
    /// либо сформированное из соответствующей грамматики.
    /// </summary>
    public BaseFirstNamePart FirstNamePart { get; set; }

    /// <summary>
    /// Часть имени - отчество, найденное в списке известных мужских/женских отчеств,
    /// либо сформированное из соответствующей грамматики.
    /// </summary>
    public BaseMidNamePart MidNamePart { get; set; }

    /// <summary>
    /// Часть имени - фамилия, сформированная из соответствующей грамматики.
    /// </summary>
    public BaseLastNamePart LastNamePart { get; set; }

    /// <summary>
    /// Гендерная принадлежность, извлеченная из личного имени/отчества.
    /// </summary>
    public Gender Gender => FirstNamePart?.Gender ?? MidNamePart?.Gender ?? Gender.N;

    /// <summary>
    /// Исходная строка, разбитая на массив подстрок через пробел.
    /// </summary>
    public List<string> InitialFragments { get; }

    /// <summary>
    /// Массив подстрок (<see cref="InitialFragments"/>), из которого последовательно удаляются
    /// те или иные подстроки по мере их сопоставления с частями имен.
    /// </summary>
    public List<string> CurrentFragments { get; set; }

    /// <summary>
    /// ФИО, сформированное из личного имени, отчества и фамилии.
    /// </summary>
    public BasePersonName PersonName =>
      FirstNamePart != null && MidNamePart != null && LastNamePart != null
      ?
      Gender switch
      {
        Gender.F => new FPersonName((FFirstPart)FirstNamePart, (FMidPart)MidNamePart, (FLastPart)LastNamePart),
        Gender.M => new MPersonName((MFirstPart)FirstNamePart, (MMidPart)MidNamePart, (MLastPart)LastNamePart),
        _ => null
      }
      : null;

  }

}
