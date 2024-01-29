namespace MfMadi.Services.Interfaces
{
    public interface IGenerateHtmlContent
    {
        public void GenerateHtml(string path, string contentTitle, string contentHtml);
        public void DeleteGeneratedHtmlContent(string path);
    }
}
