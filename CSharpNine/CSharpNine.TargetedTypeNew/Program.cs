using System;
using System.Collections.Generic;

Console.WriteLine("Hello World!");

// Starting with C# 3.0, we could start using the "var" keyword rather than specifying the full type when instantiating local variables
var person = new Person
{
    FirstName = "Bilbo",
    LastName = "Baggins"
};

// In C# 9.0, we now have another way to instantiate local variables which is less verbose, using new():
Person otherPerson = new()
{
    FirstName = "Frodo",
    LastName = "Baggins"
};

// For local variables, using var versus new() doesn't really provide much value and is up to personal preference. The new() syntax
// really starts to shine when you use it in generic collections:
var people = new List<Person>
{
    new() { FirstName = "Arnold", LastName = "Palmer" },
    new() { FirstName = "Sarah", LastName = "Smith" },
    new() { FirstName = "Jack", LastName = "White" },
    new() { FirstName = "Jack", LastName = "Black" }
};

people.Add(new() { FirstName = "Jessica", LastName = "Cates"});

// it's also especially useful for initializing properties, which have never been able to leverage the var keyword:
// Here's what a typical property initialization might look like:

public class Team
{
    public Person Coach { get; init; } = new Person();
    public Dictionary<int, List<Person>> MembersByScore { get; init; } = new Dictionary<int, List<Person>>();
}

// with C# 9.0's targeted-type new() we can clean this up quite a bit:
public class NewTeam
{
    public Person Coach { get; init; } = new();
    public Dictionary<int, List<Person>> MembersByScore { get; init; } = new();
}

public class Person
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}