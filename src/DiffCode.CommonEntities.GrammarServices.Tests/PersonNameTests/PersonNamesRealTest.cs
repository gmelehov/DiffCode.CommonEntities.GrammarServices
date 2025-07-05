using DiffCode.CommonEntities.Abstractions;
using DiffCode.CommonEntities.Enums;
using System.Diagnostics;

namespace DiffCode.CommonEntities.GrammarServices.Tests.PersonNameTests;

/// <summary>
/// 
/// </summary>
[TestClass]
public class PersonNamesRealTest
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IServiceProvider _sp;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly BasePersonName.Factory _personNameFactory;



  public const string fRealNameLFM = "Каневская Зинаида Матвеевна";


  public const string fRealNameFML = "Зинаида Матвеевна Каневская";







  public PersonNamesRealTest()
  {
    var scoll = new ServiceCollection()
      .AddAllGrammars()
      ;
    _sp = scoll.BuildServiceProvider();
    _personNameFactory = _sp.GetRequiredService<BasePersonName.Factory>();
  }







  [TestMethod]
  [DataRow(fRealNameLFM, fRealNameFML)]
  public void PersonNames_FReal_LFM_Equals_FML(string srcTextLFM, string srcTextFML)
  {
    var personNameLFM = _personNameFactory(srcTextLFM);
    var personNameFML = _personNameFactory(srcTextFML);
    Assert.AreEqual(personNameLFM.Full, personNameFML.Full);
  }



  [TestMethod]
  [DataRow(fRealNameLFM)]
  public void PersonNames_FRealLFM_HasExpected_Gender(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(Gender.F, personName.Gender);
  }



  [TestMethod]
  [DataRow(fRealNameLFM)]
  public void PersonNames_FRealLFM_Has_LastName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.LastName);
  }



  [TestMethod]
  [DataRow(fRealNameLFM)]
  public void PersonNames_FRealLFM_Has_FirstName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.FirstName);
  }



  [TestMethod]
  [DataRow(fRealNameLFM)]
  public void PersonNames_FRealLFM_Has_MidName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.MidName);
  }










  [TestMethod]
  [DataRow(fRealNameFML)]
  public void PersonNames_FRealFML_HasExpected_Gender(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.AreEqual(Gender.F, personName.Gender);
  }



  [TestMethod]
  [DataRow(fRealNameFML)]
  public void PersonNames_FRealFML_Has_LastName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.LastName);
  }



  [TestMethod]
  [DataRow(fRealNameFML)]
  public void PersonNames_FRealFML_Has_FirstName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.FirstName);
  }



  [TestMethod]
  [DataRow(fRealNameFML)]
  public void PersonNames_FRealFML_Has_MidName(string srcText)
  {
    var personName = _personNameFactory(srcText);
    Assert.IsNotNull(personName.MidName);
  }





}
