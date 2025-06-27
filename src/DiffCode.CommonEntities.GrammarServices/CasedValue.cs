using DiffCode.CommonEntities.Enums;
using static DiffCode.CommonEntities.Enums.GCase;


namespace DiffCode.CommonEntities.GrammarServices;


internal readonly record struct CasedValue
{
  public CasedValue(int val, GCase @case, Gender gen)
  {
    Value = val;
    Case = @case;
    Gender = gen;
  }
  public CasedValue(int val, GCase @case, Gender gen, Enums.Digits digits)
  {
    Value = val;
    Case = @case;
    Gender = (int)digits <= 0 ? gen : digits == Enums.Digits.Thousands ? Gender.F : Gender.M;
  }




  /// <summary>
  /// Число.
  /// </summary>
  public readonly int Value { get; }

  /// <summary>
  /// Грамматический падеж.
  /// </summary>
  public readonly GCase Case { get; }

  /// <summary>
  /// Грамматический род.
  /// </summary>
  public readonly Gender Gender { get; }

  /// <summary>
  /// Число прописью.
  /// </summary>
  public readonly string Result => (Value, Case, Gender) switch
  {

    (0, NOM or ACC, _) => "ноль",
    (0, GEN, _) => "нуля",
    (0, DAT, _) => "нулю",
    (0, INS, _) => "нулем",
    (0, LOC, _) => "нуле",



    (1, NOM or ACC, Gender.M) => "один",
    (1, NOM, Gender.F) => "одна",
    (1, ACC, Gender.F) => "одну",
    (1, GEN, Gender.M) => "одного",
    (1, GEN, Gender.F) => "одной",
    (1, DAT, Gender.M) => "одному",
    (1, DAT, Gender.F) => "одной",
    (1, INS, Gender.M) => "одним",
    (1, INS, Gender.F) => "одной",
    (1, LOC, Gender.M) => "одном",
    (1, LOC, Gender.F) => "одной",


    (2, NOM or ACC, Gender.M) => "два",
    (2, NOM or ACC, Gender.F) => "две",
    (2, GEN, _) => "двух",
    (2, DAT, _) => "двум",
    (2, INS, _) => "двумя",
    (2, LOC, _) => "двух",


    (3, NOM or ACC, _) => "три",
    (3, GEN or LOC, _) => "трех",
    (3, DAT, _) => "трем",
    (3, INS, _) => "тремя",


    (4, NOM or ACC, _) => "четыре",
    (4, GEN or LOC, _) => "четырех",
    (4, DAT, _) => "четырем",
    (4, INS, _) => "четырьмя",


    (5, NOM or ACC, _) => "пять",
    (5, GEN or DAT or LOC, _) => "пяти",
    (5, INS, _) => "пятью",


    (6, NOM or ACC, _) => "шесть",
    (6, GEN or DAT or LOC, _) => "шести",
    (6, INS, _) => "шестью",


    (7, NOM or ACC, _) => "семь",
    (7, GEN or DAT or LOC, _) => "семи",
    (7, INS, _) => "семью",


    (8, NOM or ACC, _) => "восемь",
    (8, GEN or DAT or LOC, _) => "восьми",
    (8, INS, _) => "восемью",


    (9, NOM or ACC, _) => "девять",
    (9, GEN or DAT or LOC, _) => "девяти",
    (9, INS, _) => "девятью",


    (10, NOM or ACC, _) => "десять",
    (10, GEN or DAT or LOC, _) => "десяти",
    (10, INS, _) => "десятью",


    (11, NOM or ACC, _) => "одиннадцать",
    (11, GEN or DAT or LOC, _) => "одиннадцати",
    (11, INS, _) => "одиннадцатью",


    (12, NOM or ACC, _) => "двенадцать",
    (12, GEN or DAT or LOC, _) => "двенадцати",
    (12, INS, _) => "двенадцатью",


    (13, NOM or ACC, _) => "тринадцать",
    (13, GEN or DAT or LOC, _) => "тринадцати",
    (13, INS, _) => "тринадцатью",


    (14, NOM or ACC, _) => "четырнадцать",
    (14, GEN or DAT or LOC, _) => "четырнадцати",
    (14, INS, _) => "четырнадцатью",


    (15, NOM or ACC, _) => "пятнадцать",
    (15, GEN or DAT or LOC, _) => "пятнадцати",
    (15, INS, _) => "пятнадцатью",


    (16, NOM or ACC, _) => "шестнадцать",
    (16, GEN or DAT or LOC, _) => "шестнадцати",
    (16, INS, _) => "шестнадцатью",


    (17, NOM or ACC, _) => "семнадцать",
    (17, GEN or DAT or LOC, _) => "семнадцати",
    (17, INS, _) => "семнадцатью",


    (18, NOM or ACC, _) => "восемнадцать",
    (18, GEN or DAT or LOC, _) => "восемнадцати",
    (18, INS, _) => "восемнадцатью",


    (19, NOM or ACC, _) => "девятнадцать",
    (19, GEN or DAT or LOC, _) => "девятнадцати",
    (19, INS, _) => "девятнадцатью",


    (20 or 30, NOM or ACC, _) => $"{New(Value / 10, NOM, Gender.M).Result}дцать",
    (20 or 30, GEN or DAT or LOC, _) => $"{New(Value / 10, NOM, Gender.M).Result}дцати",
    (20 or 30, INS, _) => $"{New(Value / 10, NOM, Gender.M).Result}дцатью",


    ( > 20 and < 30 or
     > 30 and < 40 or
     > 40 and < 50 or
     > 50 and < 60 or
     > 60 and < 70 or
     > 70 and < 80 or
     > 80 and < 90, NOM, _) => $"{New(Tens, NOM, Gender).Result} {New(Value - Tens, NOM, Gender).Result}",

    ( > 20 and < 30 or
     > 30 and < 40 or
     > 40 and < 50 or
     > 50 and < 60 or
     > 60 and < 70 or
     > 70 and < 80 or
     > 80 and < 90, ACC, _) => $"{New(Tens, ACC, Gender).Result} {New(Value - Tens, ACC, Gender).Result}",

    ( > 20 and < 30 or
     > 30 and < 40 or
     > 40 and < 50 or
     > 50 and < 60 or
     > 60 and < 70 or
     > 70 and < 80 or
     > 80 and < 90, GEN, _) => $"{New(Tens, GEN, Gender).Result} {New(Value - Tens, GEN, Gender).Result}",

    ( > 20 and < 30 or
     > 30 and < 40 or
     > 40 and < 50 or
     > 50 and < 60 or
     > 60 and < 70 or
     > 70 and < 80 or
     > 80 and < 90, DAT, _) => $"{New(Tens, DAT, Gender).Result} {New(Value - Tens, DAT, Gender).Result}",

    ( > 20 and < 30 or
     > 30 and < 40 or
     > 40 and < 50 or
     > 50 and < 60 or
     > 60 and < 70 or
     > 70 and < 80 or
     > 80 and < 90, LOC, _) => $"{New(Tens, LOC, Gender).Result} {New(Value - Tens, LOC, Gender).Result}",

    ( > 20 and < 30 or
     > 30 and < 40 or
     > 40 and < 50 or
     > 50 and < 60 or
     > 60 and < 70 or
     > 70 and < 80 or
     > 80 and < 90, INS, _) => $"{New(Tens, INS, Gender).Result} {New(Value - Tens, INS, Gender).Result}",


    (40, NOM or ACC, _) => $"сорок",
    (40, GEN or DAT or LOC or INS, _) => $"сорока",


    (50 or 60 or 70 or 80, NOM or ACC, _) => $"{New(Value / 10, NOM, Gender).Result}десят",
    (50 or 60 or 70 or 80, GEN or DAT or LOC, _) => $"{New(Value / 10, GEN, Gender).Result}десяти",
    (50 or 60 or 70 or 80, INS, _) => $"{New(Value / 10, INS, Gender).Result}десятью",


    (90, NOM or ACC, _) => $"девяносто",
    (90, GEN or DAT or LOC or INS, _) => $"девяноста",


    (100, NOM or ACC, _) => $"сто",
    (100, GEN or DAT or LOC or INS, _) => $"ста",


    (200, NOM or ACC, _) => $"двести",
    (200, GEN, _) => $"{New(HundredsDigit, GEN, Gender).Result}сот",
    (200, DAT, _) => $"{New(HundredsDigit, DAT, Gender).Result}стам",
    (200, INS, _) => $"{New(HundredsDigit, INS, Gender).Result}стами",
    (200, LOC, _) => $"{New(HundredsDigit, LOC, Gender).Result}стах",


    (300 or 400, NOM or ACC, _) => $"{New(HundredsDigit, NOM, Gender).Result}ста",
    (300 or 400, GEN, _) => $"{New(HundredsDigit, GEN, Gender).Result}сот",
    (300 or 400, DAT, _) => $"{New(HundredsDigit, DAT, Gender).Result}стам",
    (300 or 400, INS, _) => $"{New(HundredsDigit, INS, Gender).Result}стами",
    (300 or 400, LOC, _) => $"{New(HundredsDigit, LOC, Gender).Result}стах",


    (500 or 600 or 700 or 800 or 900, NOM or ACC, _) => $"{New(HundredsDigit, NOM, Gender).Result}сот",
    (500 or 600 or 700 or 800 or 900, GEN, _) => $"{New(HundredsDigit, GEN, Gender).Result}сот",
    (500 or 600 or 700 or 800 or 900, DAT, _) => $"{New(HundredsDigit, DAT, Gender).Result}стам",
    (500 or 600 or 700 or 800 or 900, INS, _) => $"{New(HundredsDigit, INS, Gender).Result}стами",
    (500 or 600 or 700 or 800 or 900, LOC, _) => $"{New(HundredsDigit, LOC, Gender).Result}стах",


    ( >= 0 and < 10, _, _) => $"{New(Value, Case, Gender).Result}",
    ( > 10 and < 100, _, _) => $"{New(Tens, Case, Gender).Result} {New(Value - Tens, Case, Gender).Result}",
    ( > 100 and < 1000, _, _) => $"{New(Hundreds, Case, Gender).Result} {New(Value - Hundreds, Case, Gender).Result}",


    _ => "",
  };




  private int ThousandsDigit => (int)Math.Floor(Value / 1000D);

  private int HundredsDigit => (int)Math.Floor(Value / 100D);

  private int TensDigit => (int)Math.Floor(Value / 10D);

  private int Thousands => ThousandsDigit * 1000;

  private int Hundreds => HundredsDigit * 100;

  private int Tens => TensDigit * 10;




  public static CasedValue New(int val, GCase @case, Gender gen) => new CasedValue(val, @case, gen);
}
