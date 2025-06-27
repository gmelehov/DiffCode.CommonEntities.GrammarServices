//using DiffCode.CommonEntities.Abstractions;
//using DiffCode.CommonEntities.Grammars;


//namespace DiffCode.CommonEntities.GrammarServices;

///// <summary>
///// Интерфейс фабрики для создания склоняемых по грамматическим падежам сущностей.
///// </summary>
//public interface ICommonFactory
//{





//  /// <summary>
//  /// Возвращает новый экземпляр типизированного названия стороны-подписанта
//  /// со списком склонений по грамматическим падежам.
//  /// </summary>
//  /// <param name="str">Исходная строка с названием стороны-подписанта в именительном падеже.</param>
//  /// <returns></returns>
//  PartyName CreatePartyName();

//  /// <summary>
//  /// Возвращает новый экземпляр типизированного названия должности подписанта
//  /// со списком склонений по грамматическим падежам.
//  /// </summary>
//  /// <param name="str">Исходная строка с названием должности подписанта в именительном падеже.</param>
//  /// <returns></returns>
//  PositionName CreatePositionName();

//  /// <summary>
//  /// Возвращает новый экземпляр типизированного основания полномочий подписанта
//  /// со списком склонений по грамматическим падежам.
//  /// </summary>
//  /// <param name="str">Исходная строка с основанием полномочий подписанта в именительном падеже.</param>
//  /// <returns></returns>
//  AuthorityName CreateAuthorityName();

//  /// <summary>
//  /// Возвращает новый экземпляр типизированного названия организационно-правовой формы стороны
//  /// со списком склонений по грамматическим падежам.
//  /// </summary>
//  /// <param name="str">Исходная строка с названием организационной-правовой формы стороны в именительном падеже.</param>
//  /// <returns></returns>
//  LegalEntityName CreateLegalEntityName();

//  /// <summary>
//  /// Возвращает новый экземпляр типизированного названия порядка числительных
//  /// со списком склонений по грамматическим падежам.
//  /// </summary>
//  /// <param name="val">Число, для которого нужно получить склонения его порядка.</param>
//  /// <param name="str">Название порядка числительных.</param>
//  /// <returns></returns>
//  DigitsName CreateDigitsName(int val, string str);

//  /// <summary>
//  /// Возвращает грамматику со списком склонений указанного числа (его представления прописью) по падежам.
//  /// </summary>
//  /// <param name="val">Число, для которого нужно получить склонения его представления прописью.</param>
//  /// <returns></returns>
//  NumGrammar CreateNumGrammar(int val);

//  ///// <summary>
//  ///// Возвращает ФИО со списком склонений по грамматическим падежам.
//  ///// </summary>
//  ///// <param name="str">Исходная строка с именем, отчеством и фамилией.</param>
//  ///// <returns></returns>
//  //BasePersonName CreatePersonName(string str);





//}