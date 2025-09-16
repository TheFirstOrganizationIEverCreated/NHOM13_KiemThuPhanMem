namespace SoftwareTestingExercisesOfChapter4And5.Commons;

internal class Common
{
    public static string CollectionString<CollectionType> (CollectionType collection) where CollectionType : System.Collections.IList
    {
        var collectionString = "{";
        for (var index = 0; index < collection.Count; index++)
        {
            if (index < collection.Count - 1)
            {
                collectionString += $"{collection[index]} ; ";
            }
            else
            {
                collectionString += collection[index];
            }
        }
        collectionString += "}";

        return collectionString;
    }
}
