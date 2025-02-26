using DiffCode.CommonEntities.Abstractions;
using DiffCode.CommonEntities.Sources;
using Microsoft.Extensions.DependencyInjection;


namespace DiffCode.CommonEntities.GrammarServices.Extensions;

public static class IServiceProviderExtensions
{


  public static BaseGrammar GetBaseGrammar(this IServiceProvider sp, int intkey, object strkey) =>
    sp.GetRequiredKeyedService<BaseGrammar>(intkey) with { Root = strkey?.ToString() };



}
