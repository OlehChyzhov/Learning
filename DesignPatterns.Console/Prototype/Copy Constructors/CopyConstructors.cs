namespace DesignPatterns.Prototype.Copy_Constructors
{
    // One standard way to implement the Prototype pattern (deep copying) 
    // without relying on ICloneable is by using Copy Constructors.
    //
    // A Copy Constructor is a constructor that takes an instance of its own class
    // as an argument and copies all of its values into the new instance.
    //
    // Benefits:
    // - Strongly typed (no casting required).
    // - The developer has explicit control over how nested objects are copied (ensuring a deep copy).

    #region Default Objects
    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        // Copy Constructor for Address
        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class Person
    {
        public string[] Names { get; set; }
        public Address Address { get; set; }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        // Copy Constructor for Person
        public Person(Person other)
        {
            // Note: We use .Clone() on the array to ensure a deep copy of the array elements.
            // If we just did Names = other.Names, both objects would share the same array!
            Names = (string[])other.Names.Clone();

            // We explicitly call the Address copy constructor to ensure the nested object is deeply copied.
            Address = new Address(other.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }
    #endregion

    #region Example
    public static class CopyConstructors_Example
    {
        public static void Run()
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 1225));

            // We create a deep copy explicitly using the copy constructor
            var jane = new Person(john);

            // Modifying Jane's data will NOT affect John's data
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 321;

            Console.WriteLine(john); // John Smith, London Road: 1225
            Console.WriteLine(jane); // Jane Smith, London Road: 321
        }
    }
    #endregion
}