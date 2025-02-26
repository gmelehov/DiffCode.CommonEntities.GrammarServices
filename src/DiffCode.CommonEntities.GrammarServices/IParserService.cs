using DiffCode.CommonEntities.Abstractions;
using DiffCode.CommonEntities.Enums;
using DiffCode.CommonEntities.Interfaces;
using System.Numerics;
using System.Text.RegularExpressions;


namespace DiffCode.CommonEntities.GrammarServices;

/// <summary>
/// Интерфейс сервиса парсинга исходных строк.
/// </summary>
public interface IParserService
{



  ///// <summary>
  ///// 
  ///// </summary>
  ///// <param name="input"></param>
  ///// <returns></returns>
  //LegalEntityName ToLegalEntityName(string input);

  ///// <summary>
  ///// 
  ///// </summary>
  ///// <param name="input"></param>
  ///// <returns></returns>
  //PartyName ToPartyName(string input);

  ///// <summary>
  ///// 
  ///// </summary>
  ///// <param name="input"></param>
  ///// <returns></returns>
  //PositionName ToPositionName(string input);

  ///// <summary>
  ///// 
  ///// </summary>
  ///// <param name="input"></param>
  ///// <returns></returns>
  //AuthorityName ToAuthorityName(string input);

  ///// <summary>
  ///// 
  ///// </summary>
  ///// <param name="input"></param>
  ///// <returns></returns>
  //DigitsName ToDigitsName<T, U, E>(BaseMeasurement<T, U, E> val, string input) where T : INumber<T> where U : IUnits<U, E> where E : struct, Enum;



  /// <summary>
  /// Возвращает признак того, что указанная строка является формулировкой оснований полномочий подписанта.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  bool IsAuthorityName(string input);

  /// <summary>
  /// Возвращает признак того, что указанная строка является названием организационно-правовой формы.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  bool IsLegalEntityName(string input);

  /// <summary>
  /// Возвращает признак того, что указанная строка является названием стороны-подписанта.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  bool IsPartyName(string input);

  /// <summary>
  /// Возвращает признак того, что указанная строка является ФИО.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  bool IsPersonName(string input);

  /// <summary>
  /// Возвращает признак того, что указанная строка является названием должности.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  bool IsPositionName(string input);

  /// <summary>
  /// Возвращает признак того, что указанная строка является названием единицы измерения.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  bool IsUnitName(string input);

  /// <summary>
  /// Возвращает признак того, что указанная строка является названием порядка числительных.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  bool IsDigitsName(string input);




  /// <summary>
  /// Возвращает первый найденный паттерн в списке паттернов для названий сторон-подписантов,
  /// которому соответствует указанная исходная строка.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  Regex GetPartyPatternFor(string input);

  /// <summary>
  /// Возвращает первый найденный паттерн в списке паттернов для названий должностей подписантов,
  /// которому соответствует указанная исходная строка.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  Regex GetPositionPatternFor(string input);

  /// <summary>
  /// Возвращает первый найденный паттерн в списке паттернов для названий организационно-правовых форм,
  /// которому соответствует указанная исходная строка.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  Regex GetLegalEntityPatternFor(string input);

  /// <summary>
  /// Возвращает первый найденный паттерн в списке паттернов для названий полномочий подписанта,
  /// которому соответствует указанная исходная строка.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  Regex GetAuthorityPatternFor(string input);

  /// <summary>
  /// Возвращает первый найденный паттерн в списке паттернов для названий единиц измерения,
  /// которому соответствует указанная исходная строка.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  Regex GetUnitPatternFor(string input);

  /// <summary>
  /// Возвращает первый найденный паттерн в списке паттернов для названий порядков числительных,
  /// которому соответствует указанная исходная строка.
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  Regex GetDigitsPatternFor(string input);



  string GetSpelledValue(decimal value, GCase gCase, Gender gender = Gender.M);



  BaseGrammar[] GetUnitsGrammarsFor<T, U, E>(BaseMeasurement<T, U, E> val) where T : INumber<T> where U : IUnits<U, E> where E : struct, Enum;


  Func<BaseGrammar, bool> GetUnitsAdjectivesGrammars(int val, int endsWith, string name);

  Func<BaseGrammar, bool> GetUnitsNounsGrammars(int val, int endsWith, string name);




  /// <summary>
  /// 
  /// </summary>
  Regex PartiesPattern { get; }

  /// <summary>
  /// 
  /// </summary>
  Regex PositionsPattern { get; }

  /// <summary>
  /// 
  /// </summary>
  Regex LegalEntitiesPattern { get; }

  /// <summary>
  /// 
  /// </summary>
  Regex AuthoritiesPattern { get; }

  /// <summary>
  /// 
  /// </summary>
  Regex UnitsPattern { get; }

  /// <summary>
  /// 
  /// </summary>
  Regex DigitsPattern { get; }


}
