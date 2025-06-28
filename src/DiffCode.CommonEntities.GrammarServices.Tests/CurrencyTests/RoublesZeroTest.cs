using DiffCode.CommonEntities.Units.Currency;
using System.Diagnostics;


namespace DiffCode.CommonEntities.GrammarServices.Tests.CurrencyTests;

/// <summary>
/// Класс для тестирования корректности представления прописью нулевой суммы в рублях и копейках.
/// </summary>
[TestClass]
public class RoublesZeroTest
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IServiceProvider _sp;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Roubles.Factory _rblFactory;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Roubles _rbl;




  public RoublesZeroTest()
  {
    var scoll = new ServiceCollection()
      .AddAllGrammars()
      ;
    _sp = scoll.BuildServiceProvider();
    _rblFactory = _sp.GetService<Roubles.Factory>();
  }





  /// <summary>
  /// Тест на корректное создание значения, измеряемого в рублях и копейках.
  /// </summary>
  /// <remarks>
  /// В этом примере должно быть создано значение, равное 0 руб. 0 коп.
  /// </remarks>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  [TestMethod]
  [DataRow(0.0)]
  public void Roubles_Zero_HasExpected_Value(double val)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual((decimal)val, _rbl.Value);
  }


  /// <summary>
  /// Тест для именительного падежа.
  /// </summary>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в именительном падеже.</param>
  [TestMethod]
  [DataRow(0.0, "ноль рублей ноль копеек")]
  public void Roubles_Zero_HasExpected_NomCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Nom);
  }


  /// <summary>
  /// Тест для родительного падежа.
  /// </summary>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в родительном падеже.</param>
  [TestMethod]
  [DataRow(0.0, "нуля рублей нуля копеек")]
  public void Roubles_Zero_HasExpected_GenCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Gen);
  }


  /// <summary>
  /// Тест для дательного падежа.
  /// </summary>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в дательном падеже.</param>
  [TestMethod]
  [DataRow(0.0, "нулю рублей нулю копеек")]
  public void Roubles_Zero_HasExpected_DatCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Dat);
  }


  /// <summary>
  /// Тест для винительного падежа.
  /// </summary>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в винительном падеже.</param>
  [TestMethod]
  [DataRow(0.0, "ноль рублей ноль копеек")]
  public void Roubles_Zero_HasExpected_AccCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Acc);
  }


  /// <summary>
  /// Тест для творительного падежа.
  /// </summary>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в творительном падеже.</param>
  [TestMethod]
  [DataRow(0.0, "нулем рублей нулем копеек")]
  public void Roubles_Zero_HasExpected_InsCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Ins);
  }


  /// <summary>
  /// Тест для предложного падежа.
  /// </summary>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в предложном падеже.</param>
  [TestMethod]
  [DataRow(0.0, "нуле рублей нуле копеек")]
  public void Roubles_Zero_HasExpected_LocCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Loc);
  }

}
