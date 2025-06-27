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
    var strings = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    var firstNameParts = MFirstParts.Concat(FFirstParts.Cast<BasePersonNamePart>());
    var midNameParts = MMidParts.Concat(FMidParts.Cast<BasePersonNamePart>());
    var firstNameGrammars = MFirstGrammars.Concat(FFirstGrammars.Cast<BaseGrammar>());

    var foundFirstName = firstNameParts.FirstOrDefault(f => strings.Contains(f.Text));
    var foundMidName = midNameParts.FirstOrDefault(f => strings.Contains(f.Text));


    if(foundFirstName == null && foundMidName != null)
    {
      var foundFirstNameGrammar = firstNameGrammars.FirstOrDefault(f => f.Gender == foundMidName.Gender && strings.Any(s => Regex.IsMatch(s, $".?{f.End}$")));
      var foundFirstNameString = strings.FirstOrDefault(s => Regex.IsMatch(s, $".?{foundFirstNameGrammar.End}$"));
      //var firstName = new FFirstPart(foundFirstNameString) { Grammar = foundFirstNameGrammar };

      foundFirstName = foundFirstNameGrammar.Gender switch
      {
        Gender.M => new MFirstPart(foundFirstNameString, null, null) { Grammar = foundFirstNameGrammar },
        Gender.F => new FFirstPart(foundFirstNameString) { Grammar = foundFirstNameGrammar },

        _ => throw new NotImplementedException(),
      };

      strings.Remove(foundFirstNameString);
      strings.Remove(foundMidName.Text);

      var lastNameString = string.Join(' ', strings);
      var foundLastNameGram = GetGrammarForPersonNamePart(lastNameString, foundMidName.Gender, NamePart.LAST);
      BaseLastNamePart foundLastNamePart = foundMidName.Gender switch
      {
        Gender.M => new MLastPart(lastNameString) with { Grammar = foundLastNameGram },
        Gender.F => new FLastPart(lastNameString) with { Grammar = foundLastNameGram },
        _ => throw new NotImplementedException()
      };

      BasePersonName result = foundMidName.Gender switch
      {
        Gender.F => new FPersonName(new FFirstPart(foundFirstNameString) { Grammar = foundFirstNameGrammar }, (FMidPart)foundMidName, (FLastPart)foundLastNamePart),
        Gender.M => new MPersonName(new MFirstPart(foundFirstNameString, null, null) { Grammar = foundFirstNameGrammar }, (MMidPart)foundMidName, (MLastPart)foundLastNamePart),
      };


    }



    var gender = foundFirstName?.Gender ?? foundMidName.Gender;

    ret.Add(foundFirstName);
    strings.Remove(foundFirstName.Text);

    foundMidName ??= midNameParts.FirstOrDefault(f => strings.Contains(f.Text));
    ret.Add(foundMidName);
    strings.Remove(foundMidName.Text);

    var lastName = string.Join(' ', strings);
    var foundLastNameGrammar = GetGrammarForPersonNamePart(lastName, gender, NamePart.LAST);
    BaseLastNamePart lastNamePart = gender switch
    {
      Gender.M => new MLastPart(lastName) with { Grammar = foundLastNameGrammar },
      Gender.F => new FLastPart(lastName) with { Grammar = foundLastNameGrammar },
      _ => throw new NotImplementedException()
    };
    ret.Add(lastNamePart);

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
