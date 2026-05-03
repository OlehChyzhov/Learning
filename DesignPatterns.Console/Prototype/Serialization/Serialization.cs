using System.Text.Json;

namespace DesignPatterns.Prototype.Serialization
{
    // Serialization is a "shortcut" for deep copying.
    // It bypasses manual field-by-field copying by converting the object 
    // to a string (JSON) and back, creating a fresh instance.

    #region Default Objects
    public static class ExtensionMethods
    {
        // Serializes then deserializes to create a completely independent copy.
        // Requires public properties and parameterless constructors.
        public static T DeepCopy<T>(this T self)
        {
            var json = JsonSerializer.Serialize(self);
            return JsonSerializer.Deserialize<T>(json)!;
        }
    }

    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address() => StreetName = string.Empty;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString() => $"{StreetName} {HouseNumber}";
    }

    public class Person
    {
        public string[] Names { get; set; }
        public Address Address { get; set; }

        public Person()
        {
            Names = new string[0];
            Address = new();
        }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public override string ToString() => $"{string.Join(" ", Names)}, Address: {Address}";
    }

    public class Employee : Person
    {
        public int Salary { get; set; }

        public Employee() { }
        public Employee(string[] names, Address address, int salary) : base(names, address)
        {
            Salary = salary;
        }

        public override string ToString() => $"{base.ToString()}, Salary: {Salary}";
    }
    #endregion

    #region Example
    public static class PrototypeSerialization_Example
    {
        public static void Run()
        {
            var john = new Employee(["John", "Smith"], new Address("London Street", 123), 6000);

            // Deep copies the entire object graph automatically
            Employee employeeCopy = john.DeepCopy();
            Person personCopy = john.DeepCopy<Person>();

            employeeCopy.Names[0] = "Employee";
            personCopy.Names[0] = "Person";

            Console.WriteLine($"Original: {john}");
            Console.WriteLine($"Employee Copy: {employeeCopy}");
            Console.WriteLine($"Person Copy: {personCopy}");
        }
    }
    #endregion
}