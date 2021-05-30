namespace CSharpNine.Records.Examples
{
    // A record can inherit from another record. However, a record can't inherit from a class, and a class can't inherit from a record.
    public record Vehicle(double MaxSpeed);
    public record Automobile(double MaxSpeed, string Make, string Model) : Vehicle(MaxSpeed);
}