//using DiffCode.CommonEntities.Abstractions;
//using DiffCode.CommonEntities.Enums;
//using DiffCode.CommonEntities.Extensions;
//using DiffCode.CommonEntities.Grammars;
//using DiffCode.CommonEntities.Services;
//using DiffCode.CommonEntities.Units.Quantity;
//using Microsoft.Extensions.DependencyInjection;
//using System.Diagnostics;
//using static DiffCode.CommonEntities.Abstractions.Case;


//namespace DiffCode.CommonEntities.GrammarServices.Impl;

///// <summary>
///// Фабрика для создания склоняемых по грамматическим падежам сущностей.
///// </summary>
///// <param name="serviceProvider"></param>
//public class CommonFactory(IServiceProvider serviceProvider) : ICommonFactory
//{
//  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//  private readonly IServiceProvider _sp = serviceProvider;



  




//  public PartyName CreatePartyName()
//  {
//    var psrv = _sp.GetRequiredService<IParserService>();
//    Func<string, IEnumerable<BaseGrammar>> grammarsFunc = st =>
//      psrv.PartiesPattern.Match(st.DecapitalizeFirstChar()).Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => _sp.GetRequiredKeyedService<BaseGrammar>(s));

//    return new PartyName();
//  }


//  public PositionName CreatePositionName()
//  {
//    var psrv = _sp.GetRequiredService<IParserService>();
//    Func<string, IEnumerable<BaseGrammar>> grammarsFunc = st =>
//      psrv.PositionsPattern.Match(st.DecapitalizeFirstChar()).Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => _sp.GetRequiredKeyedService<BaseGrammar>(s));

//    return new PositionName();
//  }


//  public AuthorityName CreateAuthorityName()
//  {
//    var psrv = _sp.GetRequiredService<IParserService>();
//    Func<string, IEnumerable<BaseGrammar>> grammarsFunc = st =>
//      psrv.AuthoritiesPattern.Match(st.DecapitalizeFirstChar()).Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => _sp.GetRequiredKeyedService<BaseGrammar>(s));

//    return new AuthorityName();
//  }


//  public LegalEntityName CreateLegalEntityName()
//  {
//    var psrv = _sp.GetRequiredService<IParserService>();
//    Func<string, IEnumerable<BaseGrammar>> grammarsFunc = st =>
//      psrv.LegalEntitiesPattern.Match(st.DecapitalizeFirstChar()).Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => _sp.GetRequiredKeyedService<BaseGrammar>(s));

//    return new LegalEntityName();
//  }


//  public DigitsName CreateDigitsName(int val, string str)
//  {
//    var psrv = _sp.GetRequiredService<IParserService>();
//    Func<string, IEnumerable<BaseGrammar>> grammarsFunc = st =>
//      _sp.GetRequiredKeyedService<BaseGrammar[]>("digits").Where(psrv.GetUnitsNounsGrammars(int.Parse($"0{val.ToString()}"[^2..^1]), int.Parse($"0{val.ToString()}"[^1..]), st));

//    return new DigitsName(grammarsFunc, () => str);
//  }


//  public NumGrammar CreateNumGrammar(int val)
//  {
//    var psrv = _sp.GetRequiredService<IParserService>();
//    var ret = new NumGrammar(
//      NOM(psrv.GetSpelledValue(val, GCase.NOM)), GEN(psrv.GetSpelledValue(val, GCase.GEN)),
//      DAT(psrv.GetSpelledValue(val, GCase.DAT)), ACC(psrv.GetSpelledValue(val, GCase.ACC)),
//      INS(psrv.GetSpelledValue(val, GCase.INS)), LOC(psrv.GetSpelledValue(val, GCase.LOC))
//      );
//    return ret;
//  }


//  //public BasePersonName CreatePersonName(string str)
//  //{
//  //  var psrv = _sp.GetRequiredService<IPersonNameService>();
//  //  return psrv.GetPersonName(str);
//  //}







//}