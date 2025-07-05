namespace DiffCode.CommonEntities.GrammarServices.Tests.PersonNameTests;

/// <summary>
/// Класс для тестирования корректности создания моделей личных данных
/// из исходных строк, содержащих выдуманные мужские и женские имена, отчества и фамилии.
/// </summary>
/// <remarks>
/// Цель - проверить корректность работы библиотеки с выдуманными (несуществующими) мужскими
/// и женскими именами, отчествами и фамилиями, которые при этом подчиняются общим правилам 
/// склонения по грамматическим падежам.
/// </remarks>
[TestClass]
public class PersonNamesFakeTest
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IServiceProvider _sp;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly BasePersonName.Factory _personNameFactory;



  #region КОНСТАНТЫ ДЛЯ ЖЕНСКОГО ФИО ////////////////////////////////////////////////////////////////////////////////////////////


  /// <summary>
  /// Исходная строка с фамилией, именем и отчеством в формате "ФАМИЛИЯ-ИМЯ-ОТЧЕСТВО".
  /// </summary>
  public const string fFakeNameLFM = "Кенгурого Кузязя Хетшепсутовна";

  /// <summary>
  /// Исходная строка с фамилией, именем и отчеством в формате "ИМЯ-ОТЧЕСТВО-ФАМИЛИЯ".
  /// </summary>
  public const string fFakeNameFML = "Кузязя Хетшепсутовна Кенгурого";

  /// <summary>
  /// Ожидаемое текстовое представление фамилии, созданной из исходной строки 
  /// <see cref="fFakeNameFML"/>/<see cref="fFakeNameLFM"/>.
  /// </summary>
  public const string fFakeLastName = "Кенгурого";

  /// <summary>
  /// Ожидаемое текстовое представление отчества, созданного из исходной строки 
  /// <see cref="fFakeNameFML"/>/<see cref="fFakeNameLFM"/>.
  /// </summary>
  public const string fFakeMidName = "Хетшепсутовна";

  /// <summary>
  /// Ожидаемое текстовое представление имени, созданного из исходной строки 
  /// <see cref="fFakeNameFML"/>/<see cref="fFakeNameLFM"/>.
  /// </summary>
  public const string fFakeFirstName = "Кузязя";


  #endregion


  #region КОНСТАНТЫ ДЛЯ МУЖСКОГО ФИО ////////////////////////////////////////////////////////////////////////////////////////////


  /// <summary>
  /// Исходная строка с фамилией, именем и отчеством в формате "ФАМИЛИЯ-ИМЯ-ОТЧЕСТВО".
  /// </summary>
  public const string mFakeNameLFM = "Алигагатор Мавсикакий Оладьевич";

  /// <summary>
  /// Исходная строка с фамилией, именем и отчеством в формате "ИМЯ-ОТЧЕСТВО-ФАМИЛИЯ".
  /// </summary>
  public const string mFakeNameFML = "Мавсикакий Оладьевич Алигагатор";

  /// <summary>
  /// Ожидаемое текстовое представление фамилии, созданной из исходной строки 
  /// <see cref="mFakeNameFML"/>/<see cref="mFakeNameLFM"/>.
  /// </summary>
  public const string mFakeLastName = "Алигагатор";

  /// <summary>
  /// Ожидаемое текстовое представление отчества, созданного из исходной строки 
  /// <see cref="mFakeNameFML"/>/<see cref="mFakeNameLFM"/>.
  /// </summary>
  public const string mFakeMidName = "Оладьевич";

  /// <summary>
  /// Ожидаемое текстовое представление имени, созданного из исходной строки 
  /// <see cref="mFakeNameFML"/>/<see cref="mFakeNameLFM"/>.
  /// </summary>
  public const string mFakeFirstName = "Мавсикакий";


  #endregion





  public PersonNamesFakeTest()
  {
    var scoll = new ServiceCollection()
      .AddAllGrammars()
      ;
    _sp = scoll.BuildServiceProvider();
    _personNameFactory = _sp.GetRequiredService<BasePersonName.Factory>();
  }




  /// <summary>
  /// Тест на корректное создание двух эквивалентных моделей личных данных 
  /// из исходных строк, содержащих одно и то же имя, отчество и фамилию, 
  /// но построенных по различным схемам ("ФИО" и "ИОФ").
  /// </summary>
  /// <remarks>
  /// Две созданные в этом примере модели личных данных сравниваются по агрегатному
  /// свойству <see cref="BasePersonName.Full"/>.
  /// </remarks>
  /// <param name="srcTextLFM">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  /// <param name="srcTextFML">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, fFakeNameFML)]
  public void PersonNames_FFake_LFM_Equals_FML(string srcTextLFM, string srcTextFML)
  {
    var personNameLFM = _personNameFactory(srcTextLFM);
    var personNameFML = _personNameFactory(srcTextFML);
    Assert.AreEqual(personNameLFM.Full, personNameFML.Full);
  }


  /// <summary>
  /// Тест на корректное создание двух эквивалентных моделей личных данных 
  /// из исходных строк, содержащих одно и то же имя, отчество и фамилию, 
  /// но построенных по различным схемам ("ФИО" и "ИОФ").
  /// </summary>
  /// <remarks>
  /// Две созданные в этом примере модели личных данных сравниваются по агрегатному
  /// свойству <see cref="BasePersonName.Full"/>.
  /// </remarks>
  /// <param name="srcTextLFM">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  /// <param name="srcTextFML">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, mFakeNameFML)]
  public void PersonNames_MFake_LFM_Equals_FML(string srcTextLFM, string srcTextFML)
  {
    var personNameLFM = _personNameFactory(srcTextLFM);
    var personNameFML = _personNameFactory(srcTextFML);
    Assert.AreEqual(personNameLFM.Full, personNameFML.Full);
  }



  #region ТЕСТЫ ДЛЯ ЖЕНСКИХ ИМЕН, ОТЧЕСТВ И ФАМИЛИЙ ПО СХЕМЕ "ФИО" //////////////////////////////////////////////////////////////


  /// <summary>
  /// Тест на корректное определение гендерной принадлежности из исходной строки,
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  [TestMethod]
  [DataRow(fFakeNameLFM)]
  public void PersonNames_FFakeLFM_HasExpected_Gender(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(Gender.F, personName.Gender);
  }


  /// <summary>
  /// Тест на корректное обнаружение фамилии в исходной строке, 
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  [TestMethod]
  [DataRow(fFakeNameLFM)]
  public void PersonNames_FFakeLFM_Has_LastName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.LastName);
  }


  /// <summary>
  /// Тест на корректное создание фамилии из исходной строки,
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  /// <param name="lastName">Ожидаемое текстовое представление фамилии, созданной из исходной строки.</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, fFakeLastName)]
  public void PersonNames_FFakeLFM_HasExpected_LastName(string srcText, string lastName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(lastName, personName.LastName.Text);
  }


  /// <summary>
  /// Тест на корректное обнаружение имени в исходной строке, 
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  [TestMethod]
  [DataRow(fFakeNameLFM)]
  public void PersonNames_FFakeLFM_Has_FirstName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.FirstName);
  }


  /// <summary>
  /// Тест на корректное создание имени из исходной строки,
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  /// <param name="firstName">Ожидаемое текстовое представление имени, созданного из исходной строки.</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, fFakeFirstName)]
  public void PersonNames_FFakeLFM_HasExpected_FirstName(string srcText, string firstName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(firstName, personName.FirstName.Text);
  }


  /// <summary>
  /// Тест на корректное обнаружение отчества в исходной строке, 
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  [TestMethod]
  [DataRow(fFakeNameLFM)]
  public void PersonNames_FFakeLFM_Has_MidName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.MidName);
  }


  /// <summary>
  /// Тест на корректное создание отчества из исходной строки,
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  /// <param name="midName">Ожидаемое текстовое представление отчества, созданного из исходной строки.</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, fFakeMidName)]
  public void PersonNames_FFakeLFM_HasExpected_MidName(string srcText, string midName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(midName, personName.MidName.Text);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в именительном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в именительном падеже.</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, "Кенгурого Кузязя Хетшепсутовна")]
  public void PersonNames_FFake_HasExpected_NomCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Nom);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в родительном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в родительном падеже.</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, "Кенгурого Кузязи Хетшепсутовны")]
  public void PersonNames_FFake_HasExpected_GenCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Gen);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в дательном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в дательном падеже.</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, "Кенгурого Кузязе Хетшепсутовне")]
  public void PersonNames_FFake_HasExpected_DatCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Dat);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в винительном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в винительном падеже.</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, "Кенгурого Кузязю Хетшепсутовну")]
  public void PersonNames_FFake_HasExpected_AccCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Acc);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в творительном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в творительном падеже.</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, "Кенгурого Кузязей Хетшепсутовной")]
  public void PersonNames_FFake_HasExpected_InsCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Ins);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в предложном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в предложном падеже.</param>
  [TestMethod]
  [DataRow(fFakeNameLFM, "Кенгурого Кузязе Хетшепсутовне")]
  public void PersonNames_FFake_HasExpected_LocCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Loc);
  }

  #endregion



  #region ТЕСТЫ ДЛЯ ЖЕНСКИХ ИМЕН, ОТЧЕСТВ И ФАМИЛИЙ ПО СХЕМЕ "ИОФ" //////////////////////////////////////////////////////////////


  /// <summary>
  /// Тест на корректное определение гендерной принадлежности из исходной строки,
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(fFakeNameFML)]
  public void PersonNames_FFakeFML_HasExpected_Gender(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(Gender.F, personName.Gender);
  }


  /// <summary>
  /// Тест на корректное обнаружение фамилии в исходной строке, 
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(fFakeNameFML)]
  public void PersonNames_FFakeFML_Has_LastName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.LastName);
  }


  /// <summary>
  /// Тест на корректное создание фамилии из исходной строки,
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  /// <param name="lastName">Ожидаемое текстовое представление фамилии, созданной из исходной строки.</param>
  [TestMethod]
  [DataRow(fFakeNameFML, fFakeLastName)]
  public void PersonNames_FFakeFML_HasExpected_LastName(string srcText, string lastName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(lastName, personName.LastName.Text);
  }


  /// <summary>
  /// Тест на корректное обнаружение имени в исходной строке, 
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(fFakeNameFML)]
  public void PersonNames_FFakeFML_Has_FirstName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.FirstName);
  }


  /// <summary>
  /// Тест на корректное создание имени из исходной строки,
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  /// <param name="firstName">Ожидаемое текстовое представление имени, созданного из исходной строки.</param>
  [TestMethod]
  [DataRow(fFakeNameFML, fFakeFirstName)]
  public void PersonNames_FFakeFML_HasExpected_FirstName(string srcText, string firstName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(firstName, personName.FirstName.Text);
  }


  /// <summary>
  /// Тест на корректное обнаружение отчества в исходной строке, 
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(fFakeNameFML)]
  public void PersonNames_FFakeFML_Has_MidName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.MidName);
  }


  /// <summary>
  /// Тест на корректное создание отчества из исходной строки,
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  /// <param name="midName">Ожидаемое текстовое представление отчества, созданного из исходной строки.</param>
  [TestMethod]
  [DataRow(fFakeNameFML, fFakeMidName)]
  public void PersonNames_FFakeFML_HasExpected_MidName(string srcText, string midName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(midName, personName.MidName.Text);
  }

  #endregion



  #region ТЕСТЫ ДЛЯ МУЖСКИХ ИМЕН, ОТЧЕСТВ И ФАМИЛИЙ ПО СХЕМЕ "ФИО" //////////////////////////////////////////////////////////////


  /// <summary>
  /// Тест на корректное определение гендерной принадлежности из исходной строки,
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  [TestMethod]
  [DataRow(mFakeNameLFM)]
  public void PersonNames_MFakeLFM_HasExpected_Gender(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(Gender.M, personName.Gender);
  }


  /// <summary>
  /// Тест на корректное обнаружение фамилии в исходной строке, 
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  [TestMethod]
  [DataRow(mFakeNameLFM)]
  public void PersonNames_MFakeLFM_Has_LastName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.LastName);
  }


  /// <summary>
  /// Тест на корректное создание фамилии из исходной строки,
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  /// <param name="lastName">Ожидаемое текстовое представление фамилии, созданной из исходной строки.</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, mFakeLastName)]
  public void PersonNames_MFakeLFM_HasExpected_LastName(string srcText, string lastName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(lastName, personName.LastName.Text);
  }


  /// <summary>
  /// Тест на корректное обнаружение имени в исходной строке, 
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  [TestMethod]
  [DataRow(mFakeNameLFM)]
  public void PersonNames_MFakeLFM_Has_FirstName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.FirstName);
  }


  /// <summary>
  /// Тест на корректное создание имени из исходной строки,
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  /// <param name="firstName">Ожидаемое текстовое представление имени, созданного из исходной строки.</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, mFakeFirstName)]
  public void PersonNames_MFakeLFM_HasExpected_FirstName(string srcText, string firstName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(firstName, personName.FirstName.Text);
  }


  /// <summary>
  /// Тест на корректное обнаружение отчества в исходной строке, 
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  [TestMethod]
  [DataRow(mFakeNameLFM)]
  public void PersonNames_MFakeLFM_Has_MidName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.MidName);
  }


  /// <summary>
  /// Тест на корректное создание отчества из исходной строки,
  /// построенной по схеме "ФИО".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ФИО".</param>
  /// <param name="midName">Ожидаемое текстовое представление отчества, созданного из исходной строки.</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, mFakeMidName)]
  public void PersonNames_MFakeLFM_HasExpected_MidName(string srcText, string midName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(midName, personName.MidName.Text);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в именительном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в именительном падеже.</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, "Алигагатор Мавсикакий Оладьевич")]
  public void PersonNames_MFake_HasExpected_NomCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Nom);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в родительном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в родительном падеже.</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, "Алигагатора Мавсикакия Оладьевича")]
  public void PersonNames_MFake_HasExpected_GenCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Gen);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в дательном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в дательном падеже.</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, "Алигагатору Мавсикакию Оладьевичу")]
  public void PersonNames_MFake_HasExpected_DatCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Dat);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в винительном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в винительном падеже.</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, "Алигагатора Мавсикакия Оладьевича")]
  public void PersonNames_MFake_HasExpected_AccCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Acc);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в творительном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в творительном падеже.</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, "Алигагатором Мавсикакием Оладьевичем")]
  public void PersonNames_MFake_HasExpected_InsCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Ins);
  }


  /// <summary>
  /// Тест на корректное склонение полного представления ФИО в предложном падеже.
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством.</param>
  /// <param name="caseText">Ожидаемое полное представление ФИО в предложном падеже.</param>
  [TestMethod]
  [DataRow(mFakeNameLFM, "Алигагаторе Мавсикакии Оладьевиче")]
  public void PersonNames_MFake_HasExpected_LocCase(string srcText, string caseText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(caseText, personName.Loc);
  }

  #endregion



  #region ТЕСТЫ ДЛЯ МУЖСКИХ ИМЕН, ОТЧЕСТВ И ФАМИЛИЙ ПО СХЕМЕ "ИОФ" //////////////////////////////////////////////////////////////


  /// <summary>
  /// Тест на корректное определение гендерной принадлежности из исходной строки,
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(mFakeNameFML)]
  public void PersonNames_MFakeFML_HasExpected_Gender(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(Gender.M, personName.Gender);
  }


  /// <summary>
  /// Тест на корректное обнаружение фамилии в исходной строке, 
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(mFakeNameFML)]
  public void PersonNames_MFakeFML_Has_LastName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.LastName);
  }


  /// <summary>
  /// Тест на корректное создание фамилии из исходной строки,
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  /// <param name="lastName">Ожидаемое текстовое представление фамилии, созданной из исходной строки.</param>
  [TestMethod]
  [DataRow(mFakeNameFML, mFakeLastName)]
  public void PersonNames_MFakeFML_HasExpected_LastName(string srcText, string lastName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(lastName, personName.LastName.Text);
  }


  /// <summary>
  /// Тест на корректное обнаружение имени в исходной строке, 
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(mFakeNameFML)]
  public void PersonNames_MFakeFML_Has_FirstName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.FirstName);
  }


  /// <summary>
  /// Тест на корректное создание имени из исходной строки,
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  /// <param name="firstName">Ожидаемое текстовое представление имени, созданного из исходной строки.</param>
  [TestMethod]
  [DataRow(mFakeNameFML, mFakeFirstName)]
  public void PersonNames_MFakeFML_HasExpected_FirstName(string srcText, string firstName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(firstName, personName.FirstName.Text);
  }


  /// <summary>
  /// Тест на корректное обнаружение отчества в исходной строке, 
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  [TestMethod]
  [DataRow(mFakeNameFML)]
  public void PersonNames_MFakeFML_Has_MidName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.MidName);
  }


  /// <summary>
  /// Тест на корректное создание отчества из исходной строки,
  /// построенной по схеме "ИОФ".
  /// </summary>
  /// <param name="srcText">Исходная строка с именем, фамилией и отчеством, по схеме "ИОФ".</param>
  /// <param name="midName">Ожидаемое текстовое представление отчества, созданного из исходной строки.</param>
  [TestMethod]
  [DataRow(mFakeNameFML, mFakeMidName)]
  public void PersonNames_MFakeFML_HasExpected_MidName(string srcText, string midName)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(midName, personName.MidName.Text);
  }

  #endregion
}
