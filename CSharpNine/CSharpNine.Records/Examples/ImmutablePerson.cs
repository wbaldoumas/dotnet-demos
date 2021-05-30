using System;

namespace CSharpNine.Records.Examples
{
    public class ImmutablePerson : IEquatable<ImmutablePerson>
    {
        public string FirstName { get; }
        public string LastName { get; }

        public ImmutablePerson(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }

        public override string ToString() => $"{nameof(ImmutablePerson)} {{ {nameof(FirstName)} = {FirstName}, {nameof(LastName)} = {LastName} }}";

        public bool Equals(ImmutablePerson other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((ImmutablePerson) obj);
        }

        public override int GetHashCode() => HashCode.Combine(FirstName, LastName);

        public static bool operator ==(ImmutablePerson left, ImmutablePerson right) => Equals(left, right);

        public static bool operator !=(ImmutablePerson left, ImmutablePerson right) => !Equals(left, right);
    }
}