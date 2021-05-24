/*
 * init-only properties are great when you want immutability but don't want to create enormous constructors with
 * long parameter lists when you have a complex class with many immutable properties. Property initializers also
 * make it clear which value is setting which property.
 */

using System;

var person = new Person
{
    FirstName = "Frodo",
    LastName = "Baggins"
};

// the properties on our Person class are mutable
person.FirstName = "Bilbo";
person.LastName = "Boggins";

Console.WriteLine($"{person.FirstName} {person.LastName}");

// instantiate an ImmutablePerson, passing in required constructor arguments for the immutable properties. Unfortunately we don't
// know which values are being assigned to which properties at first glance.
var immutablePerson = new ImmutablePerson("Frodo", "Baggins");

// since these properties are get-only, trying to mutate them will cause a compiler error if you comment this out.
// immutablePerson.FirstName = "Bilbo";

Console.WriteLine($"{immutablePerson.FirstName} {immutablePerson.LastName}");

// instantiate an InitOnlyPerson - this class utilizes the init keyword for allowing us to use initialization syntax when the object
// is first instantiated, but prevents any mutation of the properties after the object has finished its construction phase.
var initOnlyPerson = new InitOnlyPerson
{
    FirstName = "Frodo",
    LastName = "Baggins"
};

// trying to mutate init-only properties will cause a compiler error if you comment this out.
// initOnlyPerson.FirstName = "Bilbo";

Console.WriteLine($"{initOnlyPerson.FirstName} {initOnlyPerson.LastName}");

#region example classes

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class ImmutablePerson
{
    public string FirstName { get; }
    public string LastName { get; }

    public ImmutablePerson(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public class InitOnlyPerson
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}

#endregion