// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable UseNegatedPatternInIsExpression
// ReSharper disable RedundantDiscardDesignation

/*
 * C# 9.0 introduces various pattern matching enhancements. But first, what is pattern matching?
 *
 * From https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching:
 *
 * Pattern matching is a technique where you test an expression to determine if it has certain characteristics...
 * The "is expression" supports pattern matching to test an expression and conditionally declare a new variable to
 * the result of that expression.
 *
 * So pattern matching is basically a way to check the characteristics of something and conditionally set it to a new
 * variable if it has those characteristics.
 *
 * Note: most of the examples below are shamelessly cribbed from https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns
 */

using System;

/*
 * Type Pattern Matching
 *
 * With "type" pattern matching we can check if a variable is a given type and conditionally assign it to a variable when it is.
 */

object greeting = "Hello, World!";

if (greeting is string message)
{
    Console.WriteLine(message.ToLower());  // output: hello, world!
}

// we can also apply type pattern matching to switch expressions:
static void PrintStringOrDoubleWithVariable(object foo)
{
    var message = foo switch
    {
        string bar => $"{nameof(foo)} is a string and its value is {bar.ToLower()}.",
        double baz => $"{nameof(foo)} is a number and its square root is {Math.Sqrt(baz)}.",
        null => throw new ArgumentNullException(nameof(foo)),
        _ => throw new ArgumentException("Unknown type", nameof(foo)),
    };

    Console.WriteLine(message);
}

// if we don't want to use the declared variable, we can use the discard keyword
static void PrintStringOrDoubleWithDiscard(object foo)
{
    var message = foo switch
    {
        string _ => $"{nameof(foo)} is a string.",
        double _ => $"{nameof(foo)} is a number.",
        null => throw new ArgumentNullException(nameof(foo)),
        _ => throw new ArgumentException("Unknown type of foo", nameof(foo)),
    };

    Console.WriteLine(message);
}

// starting in C# 9.0, the discard is now optional:
static void PrintStringOrDoubleWithoutDiscard(object foo)
{
    var message = foo switch
    {
        string => $"{nameof(foo)} is a string.",
        double => $"{nameof(foo)} is a number.",
        null => throw new ArgumentNullException(nameof(foo)),
        _ => throw new ArgumentException("Unknown type of foo", nameof(foo)),
    };

    Console.WriteLine(message);
}

const double foo = 12345;

PrintStringOrDoubleWithVariable(foo);
PrintStringOrDoubleWithDiscard(foo);
PrintStringOrDoubleWithoutDiscard(foo);

/*
 * Constant Pattern Matching
 *
 * You can use a constant pattern to test if an expression result equals a specified constant value. Constant pattern
 * matching existed in C# 7.0, but we'll cover some C# 9.0 enhancements below after looking at some basic constant patterns.
 */

static decimal GetGroupTicketPrice(int visitorCount) => visitorCount switch
{
    1 => 12.0m,
    2 => 20.0m,
    3 => 27.0m,
    4 => 32.0m,
    0 => 0.0m,
    _ => throw new ArgumentException($"Not supported number of visitors: {visitorCount}", nameof(visitorCount)),
};

var _ = GetGroupTicketPrice(3);

// we've also used constant pattern matching to check if something is null:
static void YellIfNull(int? maybe)
{
    if (maybe is null)
    {
        Console.WriteLine("Boo! It's null!");
    }
}

// starting in C# 9.0 you can use the "not" negation with constant pattern matching:
static void YellIfNotNull(int? maybe)
{
    if (!(maybe is null))
    {
        // this is pretty terrible looking
    }

    if (maybe != null)
    {
        // this is also not pretty and can break if anyone overrides the != operator on the given type.
    }

    if (maybe is not null)
    {
        Console.WriteLine("Woo! Not null!");
    }
}

int? maybeNull = null;
int? maybeNotNull = 123;

YellIfNull(maybeNull);
YellIfNotNull(maybeNotNull);



/*
 * Relational Pattern Matching
 *
 * Beginning with C# 9.0, you use a relational pattern to compare an expression result with a constant. In a relational
 * pattern, you can use any of the relational operators <, >, <=, or >=.
 */

static string Classify(double measurement) => measurement switch
{
    < -4.0 => "Too low",
    > 10.0 => "Too high",
    double.NaN => "Unknown",
    _ => "Acceptable",
};

Console.WriteLine(Classify(13));  // output: Too high
Console.WriteLine(Classify(double.NaN));  // output: Unknown
Console.WriteLine(Classify(2.4));  // output: Acceptable

// If you want check for a range of values, you can use the conjunctive "and" pattern, which is part of C# 9.0's logical
// pattern matching enhancements covered below.
static string GetCalendarSeason(DateTime date) => date.Month switch
{
    >= 3 and < 6 => "spring",
    >= 6 and < 9 => "summer",
    >= 9 and < 12 => "autumn",
    12 or (>= 1 and < 3) => "winter",
    _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
};

Console.WriteLine(GetCalendarSeason(new DateTime(2021, 3, 14)));  // output: spring
Console.WriteLine(GetCalendarSeason(new DateTime(2021, 7, 19)));  // output: summer
Console.WriteLine(GetCalendarSeason(new DateTime(2021, 2, 17)));  // output: winter



/*
 * Logical pattern matching - Beginning with C# 9.0, you use the "not", "and", and "or" pattern combinators to create the
 * following logical patterns:
 *
 * - Negation "not" pattern that matches an expression when the negated pattern doesn't match the expression.
 * - Conjunctive "and" pattern that matches an expression when both patterns match the expression.
 * - Disjunctive "or" pattern that matches an expression when either of patterns matches the expression.
 */

// negation
static bool IsNotZero(int num) => num is not 0;

Console.WriteLine($"Zero status: {IsNotZero(123)}");

// conjunctive
static string ClassifyMeasurement(double measurement) => measurement switch
{
    < -40.0 => "Too low",
    >= -40.0 and < 0 => "Low",
    >= 0 and < 10.0 => "Acceptable",
    >= 10.0 and < 20.0 => "High",
    >= 20.0 => "Too high",
    double.NaN => "Unknown",
};

Console.WriteLine(ClassifyMeasurement(13));  // output: High
Console.WriteLine(ClassifyMeasurement(-100));  // output: Too low
Console.WriteLine(ClassifyMeasurement(5.7));  // output: Acceptable

// disjunctive
static string GetDisjunctiveCalendarSeason(DateTime date) => date.Month switch
{
    3 or 4 or 5 => "spring",
    6 or 7 or 8 => "summer",
    9 or 10 or 11 => "autumn",
    12 or 1 or 2 => "winter",
    _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
};

Console.WriteLine(GetDisjunctiveCalendarSeason(new DateTime(2021, 1, 19)));  // output: winter
Console.WriteLine(GetDisjunctiveCalendarSeason(new DateTime(2021, 10, 9)));  // output: autumn
Console.WriteLine(GetDisjunctiveCalendarSeason(new DateTime(2021, 5, 11)));  // output: spring

// You can repeatedly use the pattern combinators in a pattern The "and" pattern combinator has higher precedence than "or".

// To explicitly specify the precedence, use parentheses:
static bool IsLetter(char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');

const char testChar = '*';

Console.WriteLine($"{testChar} is letter status: {IsLetter(testChar)}");


// you can also use parentheses like this to indicate groupings:
static void Foo(object input)
{
    if (input is not (float or double))
    {
        return;
    }

    Console.WriteLine($"{nameof(input)} is either a float or a double.");
}

object bar = 123.45;

Foo(bar);



/*
 * Property Pattern Matching
 *
 * Beginning with C# 8.0, you use a property pattern to match an expression's properties or fields against nested patterns.
 *
 * A property pattern matches an expression when an expression result is non-null and every nested pattern matches the corresponding property
 * or field of the expression result.
 */

static bool IsConferenceDay(DateTime date) => date is { Year: 2020, Month: 5, Day: 19 or 20 or 21 };

var isConferenceDay = IsConferenceDay(DateTime.Now);

if (isConferenceDay)
{
    Console.WriteLine("It's a conference day!");
}
