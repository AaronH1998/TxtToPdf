namespace AaronHodgsonTextToPDF
{
    public interface IFileRepository
    {
        string[] GetLines(string inputDir);
    }
}