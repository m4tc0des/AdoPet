using System.Collections;

namespace CommonTestUtilities.ClassDataGenerator;

public class CultureInlineData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "pt-BR" };
        yield return new object[] { "en-US" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
