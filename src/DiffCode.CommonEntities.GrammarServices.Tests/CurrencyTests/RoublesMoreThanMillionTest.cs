namespace DiffCode.CommonEntities.GrammarServices.Tests.CurrencyTests;

/// <summary>
/// Класс для тестирования корректности представления прописью суммы,
/// большей одного миллиона рублей.
/// </summary>
[TestClass]
public class RoublesMoreThanMillionTest
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IServiceProvider _sp;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Roubles.Factory _rblFactory;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Roubles _rbl;

  /// <summary>
  /// Исходное число, которое должно быть преобразовано в сумму в рублях и копейках.
  /// </summary>
  public const double testValue = 2000401.02;





  public RoublesMoreThanMillionTest()
  {
    var scoll = new ServiceCollection()
      .AddAllGrammars()
      ;
    _sp = scoll.BuildServiceProvider();
    _rblFactory = _sp.GetRequiredService<Roubles.Factory>();
  }






  /// <summary>
  /// Тест на корректное создание значения, измеряемого в рублях и копейках.
  /// </summary>
  /// <remarks>
  /// В этом примере должно быть создано значение, равное 2000401 руб. 02 коп.
  /// </remarks>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  [TestMethod]
  [DataRow(testValue)]
  public void Roubles_MoreThanMillion_HasExpected_Value(double val)
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
  [DataRow(testValue, "два миллиона четыреста один рубль две копейки")]
  public void Roubles_MoreThanMillion_HasExpected_NomCase(double val, string text)
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
  [DataRow(testValue, "двух миллионов четырехсот одного рубля двух копеек")]
  public void Roubles_MoreThanMillion_HasExpected_GenCase(double val, string text)
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
  [DataRow(testValue, "двум миллионам четыремстам одному рублю двум копейкам")]
  public void Roubles_MoreThanMillion_HasExpected_DatCase(double val, string text)
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
  [DataRow(testValue, "два миллиона четыреста один рубль две копейки")]
  public void Roubles_MoreThanMillion_HasExpected_AccCase(double val, string text)
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
  [DataRow(testValue, "двумя миллионами четырьмястами одним рублём двумя копейками")]
  public void Roubles_MoreThanMillion_HasExpected_InsCase(double val, string text)
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
  [DataRow(testValue, "двух миллионах четырехстах одном рубле двух копейках")]
  public void Roubles_MoreThanMillion_HasExpected_LocCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Loc);
  }
}
