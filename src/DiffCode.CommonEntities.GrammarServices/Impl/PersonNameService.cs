using DiffCode.CommonEntities.Abstractions;
using DiffCode.CommonEntities.Enums;
using DiffCode.CommonEntities.Grammars;
using DiffCode.CommonEntities.Persons;
using DiffCode.CommonEntities.Sources;
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

    var foundFirstName = firstNameParts.FirstOrDefault(f => strings.Contains(f.Text));
    var gender = foundFirstName.Gender;

    ret.Add(foundFirstName);
    strings.Remove(foundFirstName.Text);

    var foundMidName = midNameParts.FirstOrDefault(f => strings.Contains(f.Text));
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
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public BasePersonName GetPersonName(string input)
  {
    var personNameParts = GetPersonNameParts(input);
    var firstNamePart = personNameParts.FirstOrDefault(f => f.Part == NamePart.FIRST);
    var midNamePart = personNameParts.FirstOrDefault(f => f.Part == NamePart.MID);
    var lastNamePart = personNameParts.FirstOrDefault(f => f.Part == NamePart.LAST);

    var gender = firstNamePart.Gender;

    return gender switch
    {
      Gender.M => new MPersonName((MFirstPart)firstNamePart, (MMidPart)midNamePart, (MLastPart)lastNamePart),
      Gender.F => new FPersonName((FFirstPart)firstNamePart, (FMidPart)midNamePart, (FLastPart)lastNamePart),
      _ => throw new NotImplementedException()
    };
  }



  /// <summary>
  /// Предикат для поиска грамматики, соответствующей указанной строке. 
  /// </summary>
  private Func<BaseGrammar, string, bool> GrammarGetter => (gr, txt) => Regex.IsMatch(txt, $".?{gr.Root}{gr[GCase.NOM]?.Text}$");

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

}