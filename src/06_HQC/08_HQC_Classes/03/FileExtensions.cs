namespace CohesionAndCoupling;
public static class FileExtensions
{
    private static string GetExtension(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return string.Empty;
        }

        int indexOfLastDot = fileName.LastIndexOf('.');
        if (indexOfLastDot == -1 || indexOfLastDot == fileName.Length - 1)
        {
            return string.Empty;
        }

        return fileName.Substring(indexOfLastDot + 1);
    }

    public static string GetFileExtension(this string fileName)
    {
        return GetExtension(fileName);
    }

    private static string GetFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return string.Empty;
        }

        int indexOfLastDot = fileName.LastIndexOf('.');
        if (indexOfLastDot == -1)
        {
            return fileName;
        }

        return fileName.Substring(0, indexOfLastDot);
    }

    public static string GetFileNameWithoutExtension(this string fileName)
    {
        return GetFileName(fileName);
    }
}