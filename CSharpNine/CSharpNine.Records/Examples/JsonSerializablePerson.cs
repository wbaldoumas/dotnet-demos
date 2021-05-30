using System.Text.Json.Serialization;

namespace CSharpNine.Records.Examples
{
    // You can also apply attributes to the positional declared properties:
    public record JsonSerializablePerson(
        [property: JsonPropertyName("firstName")] string FirstName,
        [property: JsonPropertyName("lastName")] string LastName
    );
}