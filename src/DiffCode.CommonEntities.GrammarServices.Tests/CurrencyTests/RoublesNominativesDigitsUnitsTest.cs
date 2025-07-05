namespace DiffCode.CommonEntities.GrammarServices.Tests.CurrencyTests;

/// <summary>
/// Класс для тестирования корректного согласования числительных и названий разрядов
/// с названиями основной и дробной валюты (рубль и копейка).
/// </summary>
[TestClass]
public class RoublesNominativesDigitsUnitsTest
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
  public const double testValue = 682351.32;





  public RoublesNominativesDigitsUnitsTest()
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
  /// В этом примере должно быть создано значение, равное 682351 руб. 32 коп.
  /// </remarks>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  [TestMethod]
  [DataRow(testValue)]
  public void Roubles_HasExpected_Value(double val)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual((decimal)val, _rbl.Value);
  }


  /// <summary>
  /// Тест для именительного падежа.
  /// </summary>
  /// <remarks>
  /// В этом примере число тысяч (682) прописью должно быть согласовано с женским родом
  /// названия разряда (тысяча), число единиц (351) прописью должно быть согласовано с
  /// мужским родом основной валюты (рубль), а дробная часть (82) прописью должна быть
  /// согласована с женским родом дробной валютной единицы (копейка).
  /// </remarks>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в именительном падеже.</param>
  [TestMethod]
  [DataRow(testValue, "шестьсот восемьдесят две тысячи триста пятьдесят один рубль тридцать две копейки")]
  public void Roubles_HasExpected_NomCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Nom);
  }


  /// <summary>
  /// Тест для родительного падежа.
  /// </summary>
  /// <remarks>
  /// В этом примере число тысяч (682) прописью должно быть согласовано с женским родом
  /// названия разряда (тысяча), число единиц (351) прописью должно быть согласовано с
  /// мужским родом основной валюты (рубль), а дробная часть (82) прописью должна быть
  /// согласована с женским родом дробной валютной единицы (копейка).
  /// </remarks>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в родительном падеже.</param>
  [TestMethod]
  [DataRow(testValue, "шестисот восьмидесяти двух тысяч трехсот пятидесяти одного рубля тридцати двух копеек")]
  public void Roubles_HasExpected_GenCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Gen);
  }


  /// <summary>
  /// Тест для дательного падежа.
  /// </summary>
  /// <remarks>
  /// В этом примере число тысяч (682) прописью должно быть согласовано с женским родом
  /// названия разряда (тысяча), число единиц (351) прописью должно быть согласовано с
  /// мужским родом основной валюты (рубль), а дробная часть (82) прописью должна быть
  /// согласована с женским родом дробной валютной единицы (копейка).
  /// </remarks>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в дательном падеже.</param>
  [TestMethod]
  [DataRow(testValue, "шестистам восьмидесяти двум тысячам тремстам пятидесяти одному рублю тридцати двум копейкам")]
  public void Roubles_HasExpected_DatCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Dat);
  }


  /// <summary>
  /// Тест для винительного падежа.
  /// </summary>
  /// <remarks>
  /// В этом примере число тысяч (682) прописью должно быть согласовано с женским родом
  /// названия разряда (тысяча), число единиц (351) прописью должно быть согласовано с
  /// мужским родом основной валюты (рубль), а дробная часть (82) прописью должна быть
  /// согласована с женским родом дробной валютной единицы (копейка).
  /// </remarks>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в винительном падеже.</param>
  [TestMethod]
  [DataRow(testValue, "шестьсот восемьдесят две тысячи триста пятьдесят один рубль тридцать две копейки")]
  public void Roubles_HasExpected_AccCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Acc);
  }


  /// <summary>
  /// Тест для творительного падежа.
  /// </summary>
  /// <remarks>
  /// В этом примере число тысяч (682) прописью должно быть согласовано с женским родом
  /// названия разряда (тысяча), число единиц (351) прописью должно быть согласовано с
  /// мужским родом основной валюты (рубль), а дробная часть (82) прописью должна быть
  /// согласована с женским родом дробной валютной единицы (копейка).
  /// </remarks>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в творительном падеже.</param>
  [TestMethod]
  [DataRow(testValue, "шестьюстами восемьюдесятью двумя тысячами тремястами пятьюдесятью одним рублём тридцатью двумя копейками")]
  public void Roubles_HasExpected_InsCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Ins);
  }


  /// <summary>
  /// Тест для предложного падежа.
  /// </summary>
  /// <remarks>
  /// В этом примере число тысяч (682) прописью должно быть согласовано с женским родом
  /// названия разряда (тысяча), число единиц (351) прописью должно быть согласовано с
  /// мужским родом основной валюты (рубль), а дробная часть (82) прописью должна быть
  /// согласована с женским родом дробной валютной единицы (копейка).
  /// </remarks>
  /// <param name="val">Ожидаемое количество в рублях и копейках.</param>
  /// <param name="text">Ожидаемый текст прописью, в предложном падеже.</param>
  [TestMethod]
  [DataRow(testValue, "шестистах восьмидесяти двух тысячах трехстах пятидесяти одном рубле тридцати двух копейках")]
  public void Roubles_HasExpected_LocCase(double val, string text)
  {
    _rbl = _rblFactory((decimal)val);
    Assert.AreEqual(text, _rbl.Loc);
  }


}
