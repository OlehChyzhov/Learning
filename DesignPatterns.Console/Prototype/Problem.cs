namespace DesignPatterns.Prototype.Problem
{
    // The Prototype pattern is used when creating an object is expensive or complex,
    // and you'd rather copy an existing instance than create a new one from scratch.
    //
    // Problem: C# provides a built-in ICloneable interface, but it is generally considered bad practice.
    // 
    // Why is ICloneable bad?
    // 1) It returns an 'object', which means the caller is forced to cast the result back to the correct type.
    // 2) It does NOT specify whether the clone is a Deep Copy (all nested objects are copied) 
    //    or a Shallow Copy (only top-level references are copied, sharing nested objects).
    // This ambiguity leads to bugs where developers expect a deep copy but get a shallow copy instead.

    #region Problem Example
    public class Address : ICloneable
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        // ICloneable requires this method, but it returns 'object'
        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }
    }

    public static class ICloneableProblem_Example
    {
        public static void Run()
        {
            var address1 = new Address("London Road", 123);

            // Forced to cast because Clone() returns an 'object'
            var address2 = (Address)address1.Clone();

            // While it works for simple types, the ambiguity of deep vs shallow 
            // makes ICloneable dangerous for complex nested objects.
        }
    }
    #endregion
}