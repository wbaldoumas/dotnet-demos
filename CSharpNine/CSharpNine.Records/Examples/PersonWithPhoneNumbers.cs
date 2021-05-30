namespace CSharpNine.Records.Examples
{
    // you can also have additional properties that aren't part of the record's constructor:
    public record PersonWithPhoneNumbers(string FirstName, string LastName)
    {
        public string[] PhoneNumbers { get; init; }
    };
}