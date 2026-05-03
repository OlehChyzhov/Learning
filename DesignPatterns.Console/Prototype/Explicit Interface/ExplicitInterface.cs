namespace DesignPatterns.Prototype.Explicit_Interface
{
    // Another excellent way to implement the Prototype pattern is to create 
    // a custom, strongly-typed generic interface.
    //
    // By naming the method 'DeepCopy', we remove all ambiguity (unlike ICloneable).
    // The generic type <T> ensures we return the exact type, eliminating the need for casting.

    #region Solution Interface
    /// <summary>
    /// Explicit generic interface for deep copying objects.
    /// </summary>
    /// <typeparam name="T">The type of object to be copied.</typeparam>
    public interface IPrototype<T>
    {
        T DeepCopy();
    }
    #endregion

    #region Default Objects
    public class Address : IPrototype<Address>
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        // Implementing the explicit deep copy interface
        public Address DeepCopy()
        {
            return new Address(StreetName, HouseNumber);
        }
    }

    public class Person : IPrototype<Person>
    {
        public string[] Names { get; set; }
        public Address Address { get; set; }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

        // Implementing the explicit deep copy interface
        public Person DeepCopy()
        {
            // We recursively call DeepCopy() on the nested Address object
            return new Person((string[])Names.Clone(), Address.DeepCopy());
        }
    }
    #endregion

    #region Example
    public static class ExplicitInterface_Example
    {
        public static void Run()
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 1225));

            // Clean, strongly-typed, and unambiguous deep copy execution
            var jane = john.DeepCopy();
            jane.Names[0] = "Jane";

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
    #endregion
}