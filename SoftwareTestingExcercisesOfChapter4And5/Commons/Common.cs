namespace SoftwareTestingExercisesOfChapter4And5.Commons;

using System.Text;

internal class Common
{
    public static string CollectionString<CollectionType> (CollectionType collection) where CollectionType : System.Collections.IList
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append('{');
        for (var index = 0; index < collection.Count; index++)
        {
            if (index < collection.Count - 1)
            {
                stringBuilder.Append($"{collection[index]} ; ");
            }
            else
            {
                stringBuilder.Append(collection[index]);
            }
        }
        stringBuilder.Append('}');

        return stringBuilder.ToString();
    }
}
