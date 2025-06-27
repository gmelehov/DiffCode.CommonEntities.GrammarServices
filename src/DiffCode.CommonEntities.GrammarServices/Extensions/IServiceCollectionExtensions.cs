using DiffCode.CommonEntities.Abstractions;
using DiffCode.CommonEntities.Enums;
using DiffCode.CommonEntities.Extensions;
using DiffCode.CommonEntities.Grammars;
using DiffCode.CommonEntities.GrammarServices.Impl;
using DiffCode.CommonEntities.Persons;
using DiffCode.CommonEntities.Services;
using DiffCode.CommonEntities.Units.Currency;
using DiffCode.CommonEntities.Units.Part;
using DiffCode.CommonEntities.Units.Quantity;
using DiffCode.CommonEntities.Units.Time;
using Microsoft.Extensions.DependencyInjection;
using static DiffCode.CommonEntities.Abstractions.Case;


namespace DiffCode.CommonEntities.GrammarServices.Extensions;


public static class IServiceCollectionExtensions
{


  /// <summary>
  /// Добавляет грамматики для существительных и прилагательных, используемых
  /// в названиях должностей, сторон-подписантов, оснований полномочий подписантов
  /// и названий организационно-правовых форм.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddPartiesPositionsLegalsGrammars(this IServiceCollection scoll) =>
    scoll

    .AddKeyedScoped<BaseGrammar>(1, (_, _) => new MAdj("ый", NOM("ый"), GEN("ого"), DAT("ому"), ACC("ого"), INS("ым"), LOC("ом")))
    .AddKeyedScoped<BaseGrammar>(2, (_, _) => new MNounS("ый", true, NOM("ый"), GEN("ого"), DAT("ому"), ACC("ого"), INS("ым"), LOC("ом")))
    .AddKeyedScoped<BaseGrammar>(3, (_, _) => new MAdj("ий", NOM("ий"), GEN("ого"), DAT("ому"), ACC("ого"), INS("им"), LOC("ом")))
    .AddKeyedScoped<BaseGrammar>(4, (_, _) => new MNounS("ий", true, NOM("ий"), GEN("ого"), DAT("ему"), ACC("его"), INS("им"), LOC("ем")))
    .AddKeyedScoped<BaseGrammar>(5, (_, _) => new MAdj("[ш|щ]ий", NOM("ий"), GEN("его"), DAT("ему"), ACC("его"), INS("им"), LOC("ем")))
    .AddKeyedScoped<BaseGrammar>(6, (_, _) => new FAdj("ая", NOM("ая"), GEN("ей"), DAT("ей"), ACC("ую"), INS("ей"), LOC("ей")))
    .AddKeyedScoped<BaseGrammar>(7, (_, _) => new NAdj("ое", NOM("ое"), GEN("ого"), DAT("ому"), ACC("ое"), INS("ым"), LOC("ом")))
    .AddKeyedScoped<BaseGrammar>(8, (_, _) => new NAdj("кое", NOM("ое"), GEN("ого"), DAT("ому"), ACC("ое"), INS("им"), LOC("ом")))
    .AddKeyedScoped<BaseGrammar>(9, (_, _) => new MNounS("", true, NOM(""), GEN("а"), DAT("у"), ACC("а"), INS("ом"), LOC("е")))
    .AddKeyedScoped<BaseGrammar>(10, (_, _) => new MNounS("ь", true, NOM("ь"), GEN("я"), DAT("ю"), ACC("я"), INS("ем"), LOC("е")))
    .AddKeyedScoped<BaseGrammar>(11, (_, _) => new MNounS("й", true, NOM("й"), GEN("я"), DAT("ю"), ACC("я"), INS("им"), LOC("ем")))
    .AddKeyedScoped<BaseGrammar>(12, (_, _) => new MNounS("", false, NOM(""), GEN("а"), DAT("у"), ACC(""), INS("ом"), LOC("е")))
    .AddKeyedScoped<BaseGrammar>(13, (_, _) => new NNounS("о", false, NOM("о"), GEN("а"), DAT("у"), ACC("о"), INS("ом"), LOC("е")))
    .AddKeyedScoped<BaseGrammar>(14, (_, _) => new MNounS("продавец", true, NOM("ец"), GEN("ца"), DAT("цу"), ACC("ца"), INS("цом"), LOC("це")))
    .AddKeyedScoped<BaseGrammar>(15, (_, _) => new MNounS("ец", true, NOM("ец"), GEN("ца"), DAT("цу"), ACC("ца"), INS("цем"), LOC("це")))
    .AddKeyedScoped<BaseGrammar>(16, (_, _) => new FNounS("ь", false, NOM("ь"), GEN("и"), DAT("и"), ACC("ь"), INS("ью"), LOC("и")))
    .AddKeyedScoped<BaseGrammar>(17, (_, _) => new FNounS("а", true, NOM("а"), GEN("ы"), DAT("е"), ACC("у"), INS("ой"), LOC("е")))
    .AddKeyedScoped<BaseGrammar>(18, (_, _) => new FNounS("ия", false, NOM("ия"), GEN("ии"), DAT("ии"), ACC("ию"), INS("ией"), LOC("ии")))
    .AddKeyedScoped<BaseGrammar>(19, (_, _) => new NNounS("во", false, NOM("о"), GEN("а"), DAT("у"), ACC("о"), INS("ом"), LOC("е")))
    .AddKeyedScoped<BaseGrammar>(20, (_, _) => new NNounS("ие", false, NOM("е"), GEN("я"), DAT("ю"), ACC("е"), INS("ем"), LOC("и")))


    .AddKeyedScoped("доверительный", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("финансовый", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("управляющая", (sp, _) => sp.GetBaseGrammar(6, _))
    .AddKeyedScoped("административный", (sp, _) => sp.GetBaseGrammar(1, _))

    .AddKeyedScoped("генеральный", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("главный", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("доверенное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("исполнительный", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("коммерческий", (sp, _) => sp.GetBaseGrammar(3, _))
    .AddKeyedScoped("неформальный", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("первый", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("старший", (sp, _) => sp.GetBaseGrammar(5, _))
    .AddKeyedScoped("управляющий", (sp, _) => sp.GetBaseGrammar(5, _))
    .AddKeyedScoped("технический", (sp, _) => sp.GetBaseGrammar(3, _))

    .AddKeyedScoped("директор", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("заведующий", (sp, _) => sp.GetBaseGrammar(4, _))
    .AddKeyedScoped("заместитель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("инженер", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("бухгалтер", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("конструктор", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("менеджер", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("начальник", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("партнер", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("председатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("президент", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("вице-президент", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("редактор", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("руководитель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("собственник", (sp, _) => sp.GetBaseGrammar(9, _))

    .AddKeyedScoped("устав", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("приказ", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("доверенность", (sp, _) => sp.GetBaseGrammar(16, _))
    .AddKeyedScoped("распоряжение", (sp, _) => sp.GetBaseGrammar(20, _))

    .AddKeyedScoped("автор", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("агент", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("арендатор", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("арендодатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("бригада", (sp, _) => sp.GetBaseGrammar(17, _))
    .AddKeyedScoped("гарант", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("даритель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("дилер", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("доверитель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("должник", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("дольщик", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("жертвователь", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("задаткодатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("задаткополучатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("заемщик", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("заимодавец", (sp, _) => sp.GetBaseGrammar(15, _))
    .AddKeyedScoped("заказчик", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("залогодатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("залогополучатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("застройщик", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("инвестор", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("исполнитель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("клиент", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("комиссионер", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("комитент", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("компания", (sp, _) => sp.GetBaseGrammar(18, _))
    .AddKeyedScoped("контрагент", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("кредитор", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("лизингодатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("лизингополучатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("лицензиар", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("лицензиат", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("лицо", (sp, _) => sp.GetBaseGrammar(13, _))
    .AddKeyedScoped("одаряемый", (sp, _) => sp.GetBaseGrammar(2, _))
    .AddKeyedScoped("пассажир", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("перевозчик", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("плательщик", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("поверенный", (sp, _) => sp.GetBaseGrammar(2, _))
    .AddKeyedScoped("подрядчик", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("поклажедатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("покупатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("получатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("пользователь", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("поручитель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("поставщик", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("правообладатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("принципал", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("приобретатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("продавец", (sp, _) => sp.GetBaseGrammar(14, _))
    .AddKeyedScoped("работник", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("работодатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("ссудодатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("ссудополучатель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("субагент", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("субкомиссионер", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("судовладелец", (sp, _) => sp.GetBaseGrammar(15, _))
    .AddKeyedScoped("организация", (sp, _) => sp.GetBaseGrammar(18, _))
    .AddKeyedScoped("ученик", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("учредитель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("фрахтователь", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("хранитель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("цедент", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("цессионарий", (sp, _) => sp.GetBaseGrammar(11, _))
    .AddKeyedScoped("экспедитор", (sp, _) => sp.GetBaseGrammar(9, _))

    .AddKeyedScoped("полное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("акционерное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("унитарное", (sp, _) => sp.GetBaseGrammar(7, _))

    .AddKeyedScoped("производственный", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("хозяйственный", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("индивидуальный", (sp, _) => sp.GetBaseGrammar(1, _))
    .AddKeyedScoped("простое", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("инвестиционное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("некоммерческое", (sp, _) => sp.GetBaseGrammar(8, _))
    .AddKeyedScoped("публичное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("непубличное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("федеральное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("государственное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("муниципальное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("бюджетное", (sp, _) => sp.GetBaseGrammar(7, _))
    .AddKeyedScoped("областное", (sp, _) => sp.GetBaseGrammar(7, _))

    .AddKeyedScoped("общество", (sp, _) => sp.GetBaseGrammar(19, _))
    .AddKeyedScoped("товарищество", (sp, _) => sp.GetBaseGrammar(19, _))
    .AddKeyedScoped("предприятие", (sp, _) => sp.GetBaseGrammar(20, _))
    .AddKeyedScoped("кооператив", (sp, _) => sp.GetBaseGrammar(9, _))
    .AddKeyedScoped("предприниматель", (sp, _) => sp.GetBaseGrammar(10, _))
    .AddKeyedScoped("партнерство", (sp, _) => sp.GetBaseGrammar(19, _))
    .AddKeyedScoped("учреждение", (sp, _) => sp.GetBaseGrammar(20, _))

    ;



  /// <summary>
  /// Добавляет грамматики для существительных и прилагательных, используемых
  /// в названиях единиц измерения.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddUnitsGrammars(this IServiceCollection scoll) =>
    scoll
    .AddKeyedScoped<BaseGrammar[]>("units",
      (sp, _) =>
      [
        new MNounS("рубль", NOM("рубль"), GEN("рубля"), DAT("рублю"), ACC("рубль"), INS("рублём"), LOC("рубле")),
        new MNounP("рубль", NOM("рублей"), GEN("рублей"), DAT("рублям"), ACC("рублей"), INS("рублями"), LOC("рублях")),
        new MNounPSpec("рубль", NOM("рубля"), GEN("рублей"), DAT("рублям"), ACC("рубля"), INS("рублями"), LOC("рублях")),
        new MNounN("рубль", NOM("рублей"), GEN("рублей"), DAT("рублей"), ACC("рублей"), INS("рублей"), LOC("рублей")),

        new FNounS("копейка", NOM("копейка"), GEN("копейки"), DAT("копейке"), ACC("копейку"), INS("копейкой"), LOC("копейке")),
        new FNounP("копейка", NOM("копеек"), GEN("копеек"), DAT("копейкам"), ACC("копеек"), INS("копейками"), LOC("копейках")),
        new FNounPSpec("копейка", NOM("копейки"), GEN("копеек"), DAT("копейкам"), ACC("копейки"), INS("копейками"), LOC("копейках")),
        new FNounN("копейка", NOM("копеек"), GEN("копеек"), DAT("копеек"), ACC("копеек"), INS("копеек"), LOC("копеек")),

        new MNounS("месяц", NOM("месяц"), GEN("месяца"), DAT("месяцу"), ACC("месяц"), INS("месяцем"), LOC("месяце")),
        new MNounP("месяц", NOM("месяцев"), GEN("месяцев"), DAT("месяцам"), ACC("месяцев"), INS("месяцами"), LOC("месяцах")),
        new MNounPSpec("месяц", NOM("месяца"), GEN("месяцев"), DAT("месяцам"), ACC("месяца"), INS("месяцами"), LOC("месяцах")),
        new MNounN("месяц", NOM("месяцев"), GEN("месяцев"), DAT("месяцев"), ACC("месяцев"), INS("месяцев"), LOC("месяцев")),

        new MNounS("час", NOM("час"), GEN("часа"), DAT("часу"), ACC("час"), INS("часом"), LOC("часе")),
        new MNounP("час", NOM("часов"), GEN("часов"), DAT("часам"), ACC("часов"), INS("часами"), LOC("часах")),
        new MNounPSpec("час", NOM("часа"), GEN("часов"), DAT("часам"), ACC("часа"), INS("часами"), LOC("часах")),
        new MNounN("час", NOM("часов"), GEN("часов"), DAT("часов"), ACC("часов"), INS("часов"), LOC("часов")),

        new FNounS("минута", NOM("минута"), GEN("минуты"), DAT("минуте"), ACC("минуту"), INS("минутой"), LOC("минуте")),
        new FNounP("минута", NOM("минут"), GEN("минут"), DAT("минутам"), ACC("минут"), INS("минутами"), LOC("минутах")),
        new FNounPSpec("минута", NOM("минуты"), GEN("минут"), DAT("минутам"), ACC("минуты"), INS("минутами"), LOC("минутах")),
        new FNounN("минута", NOM("минут"), GEN("минут"), DAT("минут"), ACC("минут"), INS("минут"), LOC("минут")),

        new FNounS("неделя", NOM("неделя"), GEN("недели"), DAT("неделе"), ACC("неделю"), INS("неделей"), LOC("неделе")),
        new FNounP("неделя", NOM("недель"), GEN("недель"), DAT("неделям"), ACC("недель"), INS("неделями"), LOC("неделях")),
        new FNounPSpec("неделя", NOM("недели"), GEN("недель"), DAT("неделям"), ACC("недели"), INS("неделями"), LOC("неделях")),
        new FNounN("неделя", NOM("недель"), GEN("недель"), DAT("недель"), ACC("недель"), INS("недель"), LOC("недель")),

        new MNounS("день", NOM("день"), GEN("дня"), DAT("дню"), ACC("день"), INS("днём"), LOC("дне")),
        new MNounP("день", NOM("дней"), GEN("дней"), DAT("дням"), ACC("дней"), INS("днями"), LOC("днях")),
        new MNounPSpec("день", NOM("дня"), GEN("дней"), DAT("дням"), ACC("дня"), INS("днями"), LOC("днях")),
        new MNounN("день", NOM("дней"), GEN("дней"), DAT("дней"), ACC("дней"), INS("дней"), LOC("дней")),

        new MNounS("квартал", NOM("квартал"), GEN("квартала"), DAT("кварталу"), ACC("квартал"), INS("кварталом"), LOC("квартале")),
        new MNounP("квартал", NOM("кварталов"), GEN("кварталов"), DAT("кварталам"), ACC("кварталов"), INS("кварталами"), LOC("кварталах")),
        new MNounPSpec("квартал", NOM("квартала"), GEN("кварталов"), DAT("кварталам"), ACC("квартала"), INS("кварталами"), LOC("кварталах")),
        new MNounN("квартал", NOM("кварталов"), GEN("кварталов"), DAT("кварталов"), ACC("кварталов"), INS("кварталов"), LOC("кварталов")),

        new MNounS("год", NOM("год"), GEN("года"), DAT("году"), ACC("год"), INS("годом"), LOC("годе")),
        new MNounP("год", NOM("лет"), GEN("лет"), DAT("годам"), ACC("лет"), INS("годами"), LOC("годах")),
        new MNounPSpec("год", NOM("года"), GEN("лет"), DAT("годам"), ACC("года"), INS("годами"), LOC("годах")),
        new MNounN("год", NOM("лет"), GEN("лет"), DAT("лет"), ACC("лет"), INS("лет"), LOC("лет")),

        new MAdj("рабочий", NOM("рабочий"), GEN("рабочего"), DAT("рабочему"), ACC("рабочий"), INS("рабочим"), LOC("рабочем")),
        new MAdjP("рабочий", NOM("рабочих"), GEN("рабочих"), DAT("рабочим"), ACC("рабочих"), INS("рабочими"), LOC("рабочих")),
        new MAdjN("рабочий", NOM("рабочих"), GEN("рабочих"), DAT("рабочих"), ACC("рабочих"), INS("рабочих"), LOC("рабочих")),

        new MAdj("календарный", NOM("календарный"), GEN("календарного"), DAT("календарному"), ACC("календарный"), INS("календарным"), LOC("календарном")),
        new MAdjP("календарный", NOM("календарных"), GEN("календарных"), DAT("календарным"), ACC("календарных"), INS("календарными"), LOC("календарных")),
        new MAdjN("календарный", NOM("календарных"), GEN("календарных"), DAT("календарных"), ACC("календарных"), INS("календарных"), LOC("календарных")),


        new MNounS("экземпляр", NOM("экземпляр"), GEN("экземпляра"), DAT("экземпляру"), ACC("экземпляр"), INS("экземпляром"), LOC("экземпляре")),
        new MNounP("экземпляр", NOM("экземпляров"), GEN("экземпляров"), DAT("экземплярам"), ACC("экземпляров"), INS("экземплярами"), LOC("экземплярах")),
        new MNounPSpec("экземпляр", NOM("экземпляра"), GEN("экземпляров"), DAT("экземплярам"), ACC("экземпляра"), INS("экземплярами"), LOC("экземплярах")),
        new MNounN("экземпляр", NOM("экземпляров"), GEN("экземпляров"), DAT("экземпляров"), ACC("экземпляров"), INS("экземпляров"), LOC("экземпляров")),


        new FNounS("штука", NOM("штука"), GEN("штуки"), DAT("штуке"), ACC("штуку"), INS("штукой"), LOC("штуке")),
        new FNounP("штука", NOM("штук"), GEN("штук"), DAT("штукам"), ACC("штук"), INS("штуками"), LOC("штуках")),
        new FNounPSpec("штука", NOM("штуки"), GEN("штук"), DAT("штукам"), ACC("штуки"), INS("штуками"), LOC("штуках")),
        new FNounN("штука", NOM("штук"), GEN("штук"), DAT("штук"), ACC("штук"), INS("штук"), LOC("штук")),


        new MNounS("процент", NOM("процент"), GEN("процента"), DAT("проценту"), ACC("процент"), INS("процентом"), LOC("проценте")),
        new MNounP("процент", NOM("процентов"), GEN("процентов"), DAT("процентам"), ACC("процентов"), INS("процентами"), LOC("процентах")),
        new MNounPSpec("процент", NOM("процента"), GEN("процентов"), DAT("процентам"), ACC("процента"), INS("процентами"), LOC("процентах")),
        new MNounN("процент", NOM("процентов"), GEN("процентов"), DAT("процентов"), ACC("процентов"), INS("процентов"), LOC("процентов")),

      ])
    ;



  /// <summary>
  /// Добавляет грамматики для существительных, используемых в названиях порядков числительных.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddDigitsGrammars(this IServiceCollection scoll) =>
    scoll
    .AddKeyedScoped<BaseGrammar[]>("digits",
      (sp, _) =>
      [

        new FNounS("десятитысячная", NOM("десятитысячная"), GEN("десятитысячной"), DAT("десятитысячной"), ACC("десятитысячную"), INS("десятитысячной"), LOC("десятитысячной")),
        new FNounP("десятитысячная", NOM("десятитысячных"), GEN("десятитысячных"), DAT("десятитысячным"), ACC("десятитысячных"), INS("десятитысячными"), LOC("десятитысячных")),
        new FNounPSpec("десятитысячная", NOM("десятитысячные"), GEN("десятитысячных"), DAT("десятитысячным"), ACC("десятитысячные"), INS("десятитысячными"), LOC("десятитысячных")),

        new FNounS("тысячная", NOM("тысячная"), GEN("тысячной"), DAT("тысячной"), ACC("тысячную"), INS("тысячной"), LOC("тысячной")),
        new FNounP("тысячная", NOM("тысячных"), GEN("тысячных"), DAT("тысячным"), ACC("тысячных"), INS("тысячными"), LOC("тысячных")),
        new FNounPSpec("тысячная", NOM("тысячные"), GEN("тысячных"), DAT("тысячным"), ACC("тысячные"), INS("тысячными"), LOC("тысячных")),

        new FNounS("сотая", NOM("сотая"), GEN("сотой"), DAT("сотой"), ACC("сотую"), INS("сотой"), LOC("сотой")),
        new FNounP("сотая", NOM("сотых"), GEN("сотых"), DAT("сотым"), ACC("сотых"), INS("сотыми"), LOC("сотых")),
        new FNounPSpec("сотая", NOM("сотые"), GEN("сотых"), DAT("сотым"), ACC("сотые"), INS("сотыми"), LOC("сотых")),

        new FNounS("десятая", NOM("десятая"), GEN("десятой"), DAT("десятой"), ACC("десятую"), INS("десятой"), LOC("десятой")),
        new FNounP("десятая", NOM("десятых"), GEN("десятых"), DAT("десятым"), ACC("десятых"), INS("десятыми"), LOC("десятых")),
        new FNounPSpec("десятая", NOM("десятые"), GEN("десятых"), DAT("десятым"), ACC("десятые"), INS("десятыми"), LOC("десятых")),

        new FNounS("целая", NOM("целая"), GEN("целой"), DAT("целой"), ACC("целую"), INS("целой"), LOC("целой")),
        new FNounP("целая", NOM("целых"), GEN("целых"), DAT("целым"), ACC("целых"), INS("целыми"), LOC("целых")),
        new FNounPSpec("целая", NOM("целых"), GEN("целых"), DAT("целым"), ACC("целых"), INS("целыми"), LOC("целых")),

        new FNounS("тысяча", NOM("тысяча"), GEN("тысячи"), DAT("тысяче"), ACC("тысячу"), INS("тысячей"), LOC("тысяче")),
        new FNounP("тысяча", NOM("тысяч"), GEN("тысяч"), DAT("тысячам"), ACC("тысяч"), INS("тысячами"), LOC("тысячах")),
        new FNounPSpec("тысяча", NOM("тысячи"), GEN("тысяч"), DAT("тысячам"), ACC("тысячи"), INS("тысячами"), LOC("тысячах")),

        new MNounS("миллион", NOM("миллион"), GEN("миллиона"), DAT("миллиону"), ACC("миллион"), INS("миллионом"), LOC("миллионе")),
        new MNounP("миллион", NOM("миллионов"), GEN("миллионов"), DAT("миллионам"), ACC("миллионов"), INS("миллионами"), LOC("миллионах")),
        new MNounPSpec("миллион", NOM("миллиона"), GEN("миллионов"), DAT("миллионам"), ACC("миллиона"), INS("миллионами"), LOC("миллионах")),

        new MNounS("миллиард", NOM("миллиард"), GEN("миллиарда"), DAT("миллиарду"), ACC("миллиард"), INS("миллиардом"), LOC("миллиарде")),
        new MNounP("миллиард", NOM("миллиардов"), GEN("миллиардов"), DAT("миллиардам"), ACC("миллиардов"), INS("миллиардами"), LOC("миллиардах")),
        new MNounPSpec("миллиард", NOM("миллиарда"), GEN("миллиардов"), DAT("миллиардам"), ACC("миллиарда"), INS("миллиардами"), LOC("миллиардах")),

        new MNounS("триллион", NOM("триллион"), GEN("триллиона"), DAT("триллиону"), ACC("триллион"), INS("триллионом"), LOC("триллионе")),
        new MNounP("триллион", NOM("триллионов"), GEN("триллионов"), DAT("триллионам"), ACC("триллионов"), INS("триллионами"), LOC("триллионах")),
        new MNounPSpec("триллион", NOM("триллиона"), GEN("триллионов"), DAT("триллионам"), ACC("триллиона"), INS("триллионами"), LOC("триллионах")),

        new MNounS("квадриллион", NOM("квадриллион"), GEN("квадриллиона"), DAT("квадриллиону"), ACC("квадриллион"), INS("квадриллионом"), LOC("квадриллионе")),
        new MNounP("квадриллион", NOM("квадриллионов"), GEN("квадриллионов"), DAT("квадриллионам"), ACC("квадриллионов"), INS("квадриллионами"), LOC("квадриллионах")),
        new MNounPSpec("квадриллион", NOM("квадриллиона"), GEN("квадриллионов"), DAT("квадриллионам"), ACC("квадриллиона"), INS("квадриллионами"), LOC("квадриллионах")),

        new MNounS("квинтиллион", NOM("квинтиллион"), GEN("квинтиллиона"), DAT("квинтиллиону"), ACC("квинтиллион"), INS("квинтиллионом"), LOC("квинтиллионе")),
        new MNounP("квинтиллион", NOM("квинтиллионов"), GEN("квинтиллионов"), DAT("квинтиллионам"), ACC("квинтиллионов"), INS("квинтиллионами"), LOC("квинтиллионах")),
        new MNounPSpec("квинтиллион", NOM("квинтиллиона"), GEN("квинтиллионов"), DAT("квинтиллионам"), ACC("квинтиллиона"), INS("квинтиллионами"), LOC("квинтиллионах")),

        new MNounS("секстиллион", NOM("секстиллион"), GEN("секстиллиона"), DAT("секстиллиону"), ACC("секстиллион"), INS("секстиллионом"), LOC("секстиллионе")),
        new MNounP("секстиллион", NOM("секстиллионов"), GEN("секстиллионов"), DAT("секстиллионам"), ACC("секстиллионов"), INS("секстиллионами"), LOC("секстиллионах")),
        new MNounPSpec("секстиллион", NOM("секстиллиона"), GEN("секстиллионов"), DAT("секстиллионам"), ACC("секстиллиона"), INS("секстиллионами"), LOC("секстиллионах")),

      ])
    ;






  /// <summary>
  /// Добавляет фабрики для числительных и названий единиц измерения.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddCommonGrammarFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<Func<int, NumGrammar>>(sp => val =>
    {
      var psrv = sp.GetRequiredService<IParserService>();
      var ret = new NumGrammar(
        NOM(psrv.GetSpelledValue(val, GCase.NOM)), GEN(psrv.GetSpelledValue(val, GCase.GEN)),
        DAT(psrv.GetSpelledValue(val, GCase.DAT)), ACC(psrv.GetSpelledValue(val, GCase.ACC)),
        INS(psrv.GetSpelledValue(val, GCase.INS)), LOC(psrv.GetSpelledValue(val, GCase.LOC))
        );
      return ret;
    })
    .AddScoped<NumGrammar.Factory>(sp => (Gender gender = Gender.M) => val =>
    {
      var psrv = sp.GetRequiredService<IParserService>();
      var ret = new NumGrammar(
        NOM(psrv.GetSpelledValue(val, GCase.NOM, gender)), GEN(psrv.GetSpelledValue(val, GCase.GEN, gender)),
        DAT(psrv.GetSpelledValue(val, GCase.DAT, gender)), ACC(psrv.GetSpelledValue(val, GCase.ACC, gender)),
        INS(psrv.GetSpelledValue(val, GCase.INS, gender)), LOC(psrv.GetSpelledValue(val, GCase.LOC, gender))
        );
      return ret;
    })
    .AddScoped<MeasuresFactory>(
      sp => str => v => sp.GetRequiredService<IParserService>().GetUnitsGrammarsFor(v, str)
      )
    .AddScoped<DigitsName.GrammarsFactory>(
      sp => () => (v, st) =>
      {
        var digitsGrammars = sp.GetRequiredKeyedService<BaseGrammar[]>("digits");
        var psrv = sp.GetRequiredService<IParserService>();

        return digitsGrammars.Where(psrv.GetUnitsNounsGrammars(int.Parse($"0{v.ToString()}"[^2..^1]), int.Parse($"0{v.ToString()}"[^1..]), st));
      })
    ;


  /// <summary>
  /// Добавляет фабрики для создания названий сторон-подписантов.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddPartyNameFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<PartyName.GrammarsFactory>(
      sp => () => st => sp.GetRequiredService<IParserService>()
      .PartiesPattern
      .Match(st.DecapitalizeFirstChar())
      .Value
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(s => sp.GetRequiredKeyedService<BaseGrammar>(s))
      )
    .AddScoped<PartyName.Factory>(sp => str => new PartyName(str, sp.GetRequiredService<PartyName.GrammarsFactory>()))
    ;


  /// <summary>
  /// Добавляет фабрики для создания названий полномочий подписантов.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddAuthorityNameFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<AuthorityName.GrammarsFactory>(
      sp => () => st => sp.GetRequiredService<IParserService>()
      .AuthoritiesPattern
      .Match(st.DecapitalizeFirstChar())
      .Value
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(s => sp.GetRequiredKeyedService<BaseGrammar>(s))
      )
    .AddScoped<AuthorityName.Factory>(sp => str => new AuthorityName(str, sp.GetRequiredService<AuthorityName.GrammarsFactory>()))
    ;


  /// <summary>
  /// Добавляет фабрики для создания названий организационно-правовых форм сторон-подписантов.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddLegalEntityNameFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<LegalEntityName.GrammarsFactory>(
      sp => () => st => sp.GetRequiredService<IParserService>()
      .LegalEntitiesPattern
      .Match(st.DecapitalizeFirstChar())
      .Value
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(s => sp.GetRequiredKeyedService<BaseGrammar>(s))
      )
    .AddScoped<LegalEntityName.Factory>(sp => (str, sh) => new LegalEntityName(str, sh, sp.GetRequiredService<LegalEntityName.GrammarsFactory>()))
    ;


  /// <summary>
  /// Добавляет фабрики для создания названий должностей подписантов.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddPositionNameFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<PositionName.GrammarsFactory>(
      sp => () => st => sp.GetRequiredService<IParserService>()
      .PositionsPattern
      .Match(st.DecapitalizeFirstChar())
      .Value
      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(s => sp.GetRequiredKeyedService<BaseGrammar>(s))
      )
    .AddScoped<PositionName.Factory>(sp => str => new PositionName(str, sp.GetRequiredService<PositionName.GrammarsFactory>()))
    ;


  /// <summary>
  /// Добавляет фабрики для создания моделей личных данных (ФИО, пол).
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public static IServiceCollection AddPersonNameFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<BasePersonName.PartsFactory>(
      sp => () => str => sp.GetRequiredService<IPersonNameService>().GetPersonNameParts(str)
      )
    .AddScoped<MPersonName.Factory>(sp => str => new MPersonName(str, sp.GetRequiredService<BasePersonName.PartsFactory>()))
    .AddScoped<FPersonName.Factory>(sp => str => new FPersonName(str, sp.GetRequiredService<BasePersonName.PartsFactory>()))
    .AddScoped<BasePersonName.Factory>(sp => str =>
    {
      var parts = sp.GetRequiredService<BasePersonName.PartsFactory>()()(str);
      var gender = parts?.FirstOrDefault(f => f.Part == NamePart.FIRST).Gender;

      return gender switch
      {
        Gender.M => sp.GetRequiredService<MPersonName.Factory>()(str),
        Gender.F => sp.GetRequiredService<FPersonName.Factory>()(str),
        _ => throw new NotImplementedException(),
      };
    })
    ;


  /// <summary>
  /// Добавляет фабрики для создания значений с валютой.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddCurrencyUnitsFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<BaseCurrency.CasesFactory>(
      sp => () => c =>
      {
        var _numSpell = sp.GetRequiredService<NumGrammar.Factory>();
        var _measureFn = sp.GetRequiredService<MeasuresFactory>();

        var ret = new List<Case>(6);
        var sp2 = _measureFn?.Invoke(c.Measure.Full).Invoke(c.WholePart);
        var sp1 = _numSpell?.Invoke(sp2[0].Gender).Invoke(c.WholePart);
        var sp4 = _measureFn?.Invoke(c.Measure.FrUnits.Full).Invoke(c.FractionalPart);
        var sp3 = _numSpell?.Invoke(sp4[0].Gender).Invoke(c.FractionalPart);

        Func<GCase, string> grammarsText = c => $"{sp1[c].Text} {sp2[0][c].Text} {sp3[c].Text} {sp4[0][c].Text}";

        ret.AddRange(
          NOM(grammarsText(GCase.NOM)), GEN(grammarsText(GCase.GEN)),
          DAT(grammarsText(GCase.DAT)), ACC(grammarsText(GCase.ACC)),
          INS(grammarsText(GCase.INS)), LOC(grammarsText(GCase.LOC))
          );

        return ret;
      })
    .AddScoped<Roubles.Factory>(
      sp => v => new Roubles(v, sp.GetRequiredService<BaseCurrency.CasesFactory>())
      )
    ;


  /// <summary>
  /// Добавляет фабрики для создания значений с единицей измерения количества.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddQuantityUnitsFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<BaseQuantity.CasesFactory>(
      sp => () => c =>
      {
        var _numSpell = sp.GetRequiredService<NumGrammar.Factory>()();
        var _measureFn = sp.GetRequiredService<MeasuresFactory>();

        var ret = new List<Case>(6);
        var sp1 = _numSpell?.Invoke(c.Value);
        var sp2 = _measureFn?.Invoke(c.Measure.Full)?.Invoke(c.Value);

        Func<GCase, string> grammarsText = c => $"{sp1[c].Text} {sp2[0][c].Text}";

        ret.AddRange(
          NOM(grammarsText(GCase.NOM)), GEN(grammarsText(GCase.GEN)),
          DAT(grammarsText(GCase.DAT)), ACC(grammarsText(GCase.ACC)),
          INS(grammarsText(GCase.INS)), LOC(grammarsText(GCase.LOC))
          );

        return ret;
      })
    .AddScoped<Instances.Factory>(
      sp => v => new Instances(v, sp.GetRequiredService<BaseQuantity.CasesFactory>())
      )
    .AddScoped<Pieces.Factory>(
      sp => v => new Pieces(v, sp.GetRequiredService<BaseQuantity.CasesFactory>())
      )
    ;


  /// <summary>
  /// Добавляет фабрики для создания значений с единицей измерения времени.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddTimeUnitsFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<BaseTime<int>.CasesFactory>(
      sp => () => t =>
      {
        var _numSpell = sp.GetRequiredService<NumGrammar.Factory>()();
        var _measureFn = sp.GetRequiredService<MeasuresFactory>();
        var ret = new List<Case>(6);

        var sp1 = _numSpell?.Invoke(t.Value);
        var sp2 = _measureFn?.Invoke(t.Measure.Full).Invoke(t.Value);

        Func<GCase, string> grammarsText = c => $"{sp1[c].Text} {string.Join(" ", sp2.Select(s => s[c].Text))}";

        ret.AddRange(
          NOM(grammarsText(GCase.NOM)), GEN(grammarsText(GCase.GEN)),
          DAT(grammarsText(GCase.DAT)), ACC(grammarsText(GCase.ACC)),
          INS(grammarsText(GCase.INS)), LOC(grammarsText(GCase.LOC))
          );

        return ret;
      })
    .AddScoped<CalendDays.Factory>(
      sp => v => new CalendDays(v, sp.GetRequiredService<BaseTime<int>.CasesFactory>())
      )
    .AddScoped<WorkDays.Factory>(
      sp => v => new WorkDays(v, sp.GetRequiredService<BaseTime<int>.CasesFactory>())
      )
    .AddScoped<Days.Factory>(
      sp => v => new Days(v, sp.GetRequiredService<BaseTime<int>.CasesFactory>())
      )
    .AddScoped<Weeks.Factory>(
      sp => v => new Weeks(v, sp.GetRequiredService<BaseTime<int>.CasesFactory>())
      )
    .AddScoped<Months.Factory>(
      sp => v => new Months(v, sp.GetRequiredService<BaseTime<int>.CasesFactory>())
      )
    .AddScoped<Years.Factory>(
      sp => v => new Years(v, sp.GetRequiredService<BaseTime<int>.CasesFactory>())
      )
    ;


  /// <summary>
  /// Добавляет фабрики для создания значений с единицей измерения частей/долей.
  /// </summary>
  /// <param name="scoll"></param>
  /// <returns></returns>
  public static IServiceCollection AddPartUnitsFactories(this IServiceCollection scoll) =>
    scoll
    .AddScoped<BasePart.CasesFactory>(
      sp => () => p =>
      {
        var ret = new List<Case>(6);

        var _numSpell = sp.GetRequiredService<NumGrammar.Factory>();
        var _measureFn = sp.GetRequiredService<MeasuresFactory>();
        var _digitsFn = sp.GetRequiredService<DigitsName.GrammarsFactory>();

        string digits = p.FractionalPart.ToString().Length switch
        {
          0 => null,
          1 => "десятая",
          2 => "сотая",
          3 => "тысячная",
          4 => "десятитысячная",
          _ => throw new NotImplementedException()
        };

        Func<GCase, string> grammarsText;
        var sp1 = _numSpell?.Invoke().Invoke(p.WholePart);
        var sp3 = _numSpell?.Invoke(Gender.F).Invoke(p.FractionalPart);
        var sp5 = _measureFn?.Invoke(p.Measure.Full).Invoke(p.WholePart);

        if (digits == null)
        {
          grammarsText = c => $"{sp1[c].Text} {sp3[c].Text} {sp5[0][c].Text}";
        }
        else
        {
          var sp2 = _digitsFn?.Invoke().Invoke(p.WholePart, "целая").ToList();
          var sp4 = _digitsFn?.Invoke().Invoke(p.FractionalPart, digits).ToList();
          grammarsText = c => $"{sp1[c].Text} {sp2[0][c].Text} {sp3[c].Text} {sp4[0][c].Text} {p.Measure.Short}";
        }

        ret.AddRange(
          NOM(grammarsText(GCase.NOM)), GEN(grammarsText(GCase.GEN)),
          DAT(grammarsText(GCase.DAT)), ACC(grammarsText(GCase.ACC)),
          INS(grammarsText(GCase.INS)), LOC(grammarsText(GCase.LOC))
          );

        return ret;
      })
    .AddScoped<Percents.Factory>(
      sp => v => new Percents(v, sp.GetRequiredService<BasePart.CasesFactory>())
      )
    ;






  public static IServiceCollection AddAllGrammars(this IServiceCollection scoll) =>
    scoll
    .AddPartiesPositionsLegalsGrammars()
    .AddUnitsGrammars()
    .AddDigitsGrammars()
    .AddScoped<IParserService, ParserService>()
    .AddScoped<IPersonNameService, PersonNameService>()
    .AddScoped<Func<string, BaseGrammar>>(sp => str => sp.GetRequiredKeyedService<BaseGrammar>(str))
    .AddCommonGrammarFactories()
    .AddAuthorityNameFactories()
    .AddPartyNameFactories()
    .AddLegalEntityNameFactories()
    .AddPositionNameFactories()
    .AddPersonNameFactories()
    .AddCurrencyUnitsFactories()
    .AddQuantityUnitsFactories()
    .AddTimeUnitsFactories()
    .AddPartUnitsFactories()

    ;


}
