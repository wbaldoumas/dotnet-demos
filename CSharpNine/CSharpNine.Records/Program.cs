using CSharpNine.Records.Examples;
using System;

/*
 * Let's start with some examples of our vanilla ImmutablePerson class, showing our implemented equality, ToString() and other methods.
 */

ImmutablePerson immutablePerson = new("Tony", "Hawk");
ImmutablePerson otherImmutablePerson = new("Tony", "Hawk");

Console.WriteLine(
    immutablePerson == otherImmutablePerson
        ? $"{nameof(ImmutablePerson)}s are equal!"
        : $"{nameof(ImmutablePerson)}s are not equal!"
);

Console.WriteLine(immutablePerson);

var (firstName, lastName) = immutablePerson;

Console.WriteLine(firstName);
Console.WriteLine(lastName);

/*
 * What if we could have all of the above functionality on a user-defined reference type, for free, all synthesized by the compiler
 * and under the hood?
 */

/*
 * You use the record keyword to define a reference type that provides built-in functionality for encapsulating data. While records
 * can be mutable, they are primarily intended for supporting immutable data models. Records also provide all of the functionality
 * in our manually implemented ImmutablePerson class, plus more!
 */

/*
 * Using positional syntax for property definitions tells the compiler to generate the following under the hood:
 *
 * 1. A public init-only auto-property for each positional parameter provided in the record declaration.
 * 2. A primary constructor whose parameters match the positional parameters on the record declaration.
 * 3. A Deconstruct method with an "out" parameter for each positional parameter provided in the record declaration.
 */

Person person = new("Tony", "Hawk");
Person otherPerson = new("Tony", "Hawk");

// records by default provide value equality out of the box, with implemented == and != operators:
if (person == otherPerson)
{
    Console.WriteLine($"{nameof(Person)}s are equal!");
}

// records also have built-in formatting for display, which can especially come in handy when looking through logs
Console.WriteLine(person);

var (first, last) = person;

Console.WriteLine(first);
Console.WriteLine(last);

/*
 * One of the more interesting things about Records are their support for non-destructive mutation. If you need
 * to mutate the properties of a record you can use the new "with" keyword to perform non-destructive mutation.
 *
 * A with expression makes a new record instance that is a copy of an existing record instance, with specified
 * properties and fields modified. You use object initializer syntax to specify the values to be changed, as shown
 * in the following example:
 */

var anotherPerson = person with { LastName = "Cordova" };

// you can even make a copy by  having an empty with clause
var copyPerson = anotherPerson with { };

// Some additional notes:

/*
 * Note that the supported immutability is shallow immutability - After initialization, you can't change the value of
 * value-type properties or the reference of reference-type properties. However, the data that a reference-type property
 * refers to can be changed.
 */

/*
 * Similar to shallow immutability, copies made with the "with" operator are shallow copies - any reference properties are
 * copied by reference, not value.
 */

/*
 * Generic Constraints - records satisfy the "class" constraint with regards to generics.
 */

// For more info: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record