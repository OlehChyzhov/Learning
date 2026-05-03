namespace DesignPatterns.Prototype.Inheritance
{
    #region Default Objects
    public interface IDeepCopyable<T> where T : new()
    {
        // Target is an already instantiated object of type T
        void CopyTo(T target);

        // Default Interface Method (C# 8+) provides a standard entry point
        T DeepCopy()
        {
            T t = new T();
            CopyTo(t);
            return t;
        }
    }

    public class Address : IDeepCopyable<Address>
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

        public void CopyTo(Address target)
        {
            target.StreetName = StreetName;
            target.HouseNumber = HouseNumber;
        }
    }

    public class Person : IDeepCopyable<Person>
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

        public void CopyTo(Person target)
        {
            target.Names = (string[])Names.Clone();
            // Recursive deep copy call
            target.Address = Address.DeepCopy();
        }
    }

    public class Employee : Person, IDeepCopyable<Employee>
    {
        public int Salary;

        public Employee() { }
        public Employee(string[] names, Address address, int salary) : base(names, address)
        {
            Salary = salary;
        }

        public override string ToString() => $"{base.ToString()}, Salary: {Salary}";

        public void CopyTo(Employee target)
        {
            // Call base.CopyTo first to handle Names and Address
            base.CopyTo(target);
            target.Salary = Salary;
        }
    }

    public static class ExtensionMethods
    {
        // General extension for any IDeepCopyable object
        public static T DeepCopy<T>(this IDeepCopyable<T> item) where T : new()
        {
            return item.DeepCopy();
        }

        // Specialized extension for Person (and its children like Employee).
        // This allows you to call .DeepCopy<Person>() on an Employee object
        // to get a Person-typed copy specifically.
        public static T DeepCopy<T>(this T person) where T : Person, new()
        {
            return ((IDeepCopyable<T>)person).DeepCopy();
        }
    }
    #endregion

    #region Example
    public static class PrototypeInheritance_Example
    {
        public static void Run()
        {
            var john = new Employee(["John", "Smith"], new Address("London Street", 123), 6000);

            // Copying as the full Employee type
            Employee employeeCopy = john.DeepCopy();

            // Copying specifically into the base Person type
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