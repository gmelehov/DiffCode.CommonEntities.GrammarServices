using System.Diagnostics;

namespace DiffCode.CommonEntities.GrammarServices.Tests.PartyNameTests;

/// <summary>
/// Класс для тестирования корректности создания типизированного наименования стороны
/// (простое наименование, состоящее из одного существительного).
/// </summary>
[TestClass]
public class PartyNamesSimpleTest
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IServiceProvider _sp;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private PartyName.Factory _partyNameFactory;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private PartyName _partyName;




  public PartyNamesSimpleTest()
  {
    var scoll = new ServiceCollection()
      .AddAllGrammars()
      ;
    _sp = scoll.BuildServiceProvider();
    _partyNameFactory = _sp.GetRequiredService<PartyName.Factory>();
  }





  /// <summary>
  /// Тест для именительного падежа.
  /// </summary>
  /// <param name="srcText">Исходная строка с названием стороны.</param>
  /// <param name="casedText">Ожидаемый текст, в именительном падеже.</param>
  [TestMethod]
  [DataRow("исполнитель", "Исполнитель")]
  public void PartyNames_Simple_HasExpected_NomCase(string srcText, string casedText)
  {
    _partyName = _partyNameFactory(srcText);
    Assert.AreEqual(casedText, _partyName.Nom);
  }


  /// <summary>
  /// Тест для родительного падежа.
  /// </summary>
  /// <param name="srcText">Исходная строка с названием стороны.</param>
  /// <param name="casedText">Ожидаемый текст, в родительном падеже.</param>
  [TestMethod]
  [DataRow("исполнитель", "Исполнителя")]
  public void PartyNames_Simple_HasExpected_GenCase(string srcText, string casedText)
  {
    _partyName = _partyNameFactory(srcText);
    Assert.AreEqual(casedText, _partyName.Gen);
  }


  /// <summary>
  /// Тест для дательного падежа.
  /// </summary>
  /// <param name="srcText">Исходная строка с названием стороны.</param>
  /// <param name="casedText">Ожидаемый текст, в дательном падеже.</param>
  [TestMethod]
  [DataRow("исполнитель", "Исполнителю")]
  public void PartyNames_Simple_HasExpected_DatCase(string srcText, string casedText)
  {
    _partyName = _partyNameFactory(srcText);
    Assert.AreEqual(casedText, _partyName.Dat);
  }


  /// <summary>
  /// Тест для винительного падежа.
  /// </summary>
  /// <param name="srcText">Исходная строка с названием стороны.</param>
  /// <param name="casedText">Ожидаемый текст, в винительном падеже.</param>
  [TestMethod]
  [DataRow("исполнитель", "Исполнителя")]
  public void PartyNames_Simple_HasExpected_AccCase(string srcText, string casedText)
  {
    _partyName = _partyNameFactory(srcText);
    Assert.AreEqual(casedText, _partyName.Acc);
  }


  /// <summary>
  /// Тест для творительного падежа.
  /// </summary>
  /// <param name="srcText">Исходная строка с названием стороны.</param>
  /// <param name="casedText">Ожидаемый текст, в творительном падеже.</param>
  [TestMethod]
  [DataRow("исполнитель", "Исполнителем")]
  public void PartyNames_Simple_HasExpected_InsCase(string srcText, string casedText)
  {
    _partyName = _partyNameFactory(srcText);
    Assert.AreEqual(casedText, _partyName.Ins);
  }


  /// <summary>
  /// Тест для предложного падежа.
  /// </summary>
  /// <param name="srcText">Исходная строка с названием стороны.</param>
  /// <param name="casedText">Ожидаемый текст, в предложном падеже.</param>
  [TestMethod]
  [DataRow("исполнитель", "Исполнителе")]
  public void PartyNames_Simple_HasExpected_LocCase(string srcText, string casedText)
  {
    _partyName = _partyNameFactory(srcText);
    Assert.AreEqual(casedText, _partyName.Loc);
  }


}
