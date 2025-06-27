using DiffCode.CommonEntities.Abstractions;
using DiffCode.CommonEntities.Enums;
using DiffCode.CommonEntities.Interfaces;
using DiffCode.CommonEntities.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;


namespace DiffCode.CommonEntities.GrammarServices.Impl;

/// <summary>
/// Реализация сервиса парсинга исходных строк.
/// </summary>
public class ParserService(
  [FromKeyedServices("units")] BaseGrammar[] grammars,
  [FromKeyedServices("digits")] BaseGrammar[] digitsGrammars,
  Func<string, BaseGrammar> func
    ) 
  : IParserService
{

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly BaseGrammar[] _unitsGrammars = grammars;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly BaseGrammar[] _digitsGrammars = digitsGrammars;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Func<string, BaseGrammar> _fn = func;





  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public DigitsName ToDigitsName(int val, string input)
  {
    Func<string, IEnumerable<BaseGrammar>> grammarsFunc = st =>
       _digitsGrammars.Where(GetUnitsNounsGrammars(int.Parse($"0{val.ToString()}"[^2..^1]), int.Parse($"0{val.ToString()}"[^1..]), st));

    return new DigitsName(grammarsFunc, () => input);
  }




  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public bool IsAuthorityName(string input) => Authorities.Patterns.Any(a => Regex.IsMatch(input.ToLower(), a));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public bool IsLegalEntityName(string input) => LegalEntities.Patterns.Any(a => Regex.IsMatch(input.ToLower(), a));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public bool IsPartyName(string input) => Parties.Patterns.Any(a => Regex.IsMatch(input.ToLower(), a));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public bool IsPersonName(string input) => throw new NotImplementedException();

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public bool IsPositionName(string input) => Positions.Patterns.Any(a => Regex.IsMatch(input.ToLower(), a));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public bool IsUnitName(string input) => Constants.Units.Patterns.Any(a => Regex.IsMatch(input.ToLower(), a));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public bool IsDigitsName(string input) => Constants.Digits.Patterns.Any(a => Regex.IsMatch(input.ToLower(), a));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public Regex GetPartyPatternFor(string input) => new Regex(Parties.Patterns.FirstOrDefault(f => Regex.IsMatch(input.ToLower(), f)));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public Regex GetPositionPatternFor(string input) => new Regex(Positions.Patterns.FirstOrDefault(f => Regex.IsMatch(input.ToLower(), f)));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public Regex GetLegalEntityPatternFor(string input) => new Regex(LegalEntities.Patterns.FirstOrDefault(f => Regex.IsMatch(input.ToLower(), f)));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public Regex GetAuthorityPatternFor(string input) => new Regex(Authorities.Patterns.FirstOrDefault(f => Regex.IsMatch(input.ToLower(), f)));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public Regex GetUnitPatternFor(string input) => new Regex(Constants.Units.Patterns.FirstOrDefault(f => Regex.IsMatch(input.ToLower(), f)));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public Regex GetDigitsPatternFor(string input) => new Regex(Constants.Digits.Patterns.FirstOrDefault(f => Regex.IsMatch(input.ToLower(), f)));



  public BaseGrammar[] GetUnitsGrammarsFor<T, U, E>(BaseMeasurement<T, U, E> val) where T : INumber<T> where U : IUnits<U, E> where E : struct, Enum
  {
    var names = val.Measure.Full.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var grammars = _unitsGrammars.Where(a => names.Contains(a.End));
    List<BaseGrammar> ret = [];

    ret.AddRange(grammars.Where(w => GetUnitsAdjectivesGrammars(int.Parse($"0{val.AsString}"[^2..^1]), int.Parse($"0{val.AsString}"[^1..]), w.End).Invoke(w)));
    ret.AddRange(grammars.Where(w => GetUnitsNounsGrammars(int.Parse($"0{val.AsString}"[^2..^1]), int.Parse($"0{val.AsString}"[^1..]), w.End).Invoke(w)));

    return ret.ToArray();
  }


  public BaseGrammar[] GetUnitsGrammarsFor(int val, string input)
  {
    var names = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var grammars = _unitsGrammars.Where(a => names.Contains(a.End));
    List<BaseGrammar> ret = [];

    ret.AddRange(grammars.Where(w => GetUnitsAdjectivesGrammars(int.Parse($"0{val.ToString()}"[^2..^1]), int.Parse($"0{val.ToString()}"[^1..]), w.End).Invoke(w)));
    ret.AddRange(grammars.Where(w => GetUnitsNounsGrammars(int.Parse($"0{val.ToString()}"[^2..^1]), int.Parse($"0{val.ToString()}"[^1..]), w.End).Invoke(w)));

    return ret.ToArray();
  }





  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public Regex PartiesPattern => new Regex(@"^((" + string.Join(@"|", Parties.Adjectives.Select(s => s.ToLower())) + @")*\s*)*(" + string.Join(@"|", Parties.Nouns.Select(s => s.ToLower())) + @")");

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public Regex PositionsPattern => new Regex(@"^((" + string.Join(@"|", Positions.Adjectives.Select(s => s.ToLower())) + @")*\s*)*(" + string.Join(@"|", Positions.Nouns.Select(s => s.ToLower())) + @")");

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public Regex LegalEntitiesPattern => new Regex(@"^((" + string.Join(@"|", LegalEntities.Adjectives.Select(s => s.ToLower())) + @")*\s*)*(" + string.Join(@"|", LegalEntities.Nouns.Select(s => s.ToLower())) + @")");

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public Regex AuthoritiesPattern => new Regex(@"^((" + string.Join(@"|", Authorities.Nouns.Select(s => s.ToLower())) + @")*\s*)");

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public Regex UnitsPattern => new Regex(@"^((" + string.Join(@"|", Constants.Units.Adjectives.Select(s => s.ToLower())) + @")*\s*)*(" + string.Join(@"|", Constants.Units.Nouns.Select(s => s.ToLower())) + @")");

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public Regex DigitsPattern => new Regex(@"^((" + string.Join(@"|", Constants.Digits.Nouns.Select(s => s.ToLower())) + @")*\s*)");






  public string GetSpelledValue(decimal value, GCase gCase, Gender gender = Gender.M) => string.Join(" ",
      value
      .ToString(@"#,##0", NumberFormat)
      .Split(' ')
      .Reverse()
      .Select((s, i) => new ValueTuple<int, int, Enums.Digits>(int.Parse(s), int.Parse(s.LastOrDefault().ToString()), (Enums.Digits)(i * 3)))
      .Where(w => value < 1000 ? true : w.Item1 > 0 && value >= 1000)
      .Select(ss => $"{new CasedValue(ss.Item1, gCase, gender, ss.Item3).Result} {ToDigitsName(ss.Item1, GetDigitsText(ss.Item3))[gCase]}")
      .Where(w => !w.StartsWith(" "))
      .Reverse()
      ).Trim();



  public Func<BaseGrammar, bool> GetUnitsAdjectivesGrammars(int val, int endsWith, string name)
    => gr => gr.PoS.Equals(PartOfSpeech.Adjective) && gr.End == name && gr.Arity == (val, endsWith) switch
    {
      (not 1, 1) => Arity.S,
      (_, 0 or 5 or 6 or 7 or 8 or 9) => Arity.P,
      (1, _) => Arity.P,
      (not 1, 2 or 3 or 4) => Arity.P,
      _ => Arity.P
    };


  public Func<BaseGrammar, bool> GetUnitsNounsGrammars(int val, int endsWith, string name)
    => gr => gr.PoS.Equals(PartOfSpeech.Noun) && gr.End == name && gr.Arity == (val, endsWith) switch
    {
      (not 1, 1) => Arity.S,
      (_, 0 or 5 or 6 or 7 or 8 or 9) => Arity.P,
      (1, _) => Arity.P,
      (not 1, 2 or 3 or 4) => Arity.PSpec,
      _ => Arity.P
    };



  private NumberFormatInfo NumberFormat
  {
    get
    {
      var formatInfo = new CultureInfo("ru-RU", true).NumberFormat;
      formatInfo.NumberGroupSeparator = " ";
      return formatInfo;
    }
  }


  private string GetDigitsText(Enums.Digits digits) => digits switch
  {
    Enums.Digits.None => "",
    Enums.Digits.Thousands => "тысяча",
    Enums.Digits.Millions => "миллион",
    Enums.Digits.Billions => "миллиард",
    Enums.Digits.Trillions => "триллион",
    Enums.Digits.Quadrillions => "квадриллион",
    Enums.Digits.Quintillions => "квинтиллион",
    Enums.Digits.Sextillions => "секстиллион",
    _ => ""
  };


}
