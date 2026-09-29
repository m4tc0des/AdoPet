using System.Collections;

namespace CommonTestUtilities.ClassDataGenerator;

public class ErrorsGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "" };
        yield return new object[] { "     " };
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        yield return new object[] { null };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
