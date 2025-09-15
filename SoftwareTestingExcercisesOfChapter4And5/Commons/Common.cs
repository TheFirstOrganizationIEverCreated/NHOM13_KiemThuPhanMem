namespace SoftwareTestingExercisesOfChapter4And5.Commons;

internal class Common
{
    public static string CollectionString<CollectionType> (CollectionType collection) where CollectionType : System.Collections.IEnumerable
    {
        return "{" + string.Join(" ; ", collection) + "}";
    }
}
