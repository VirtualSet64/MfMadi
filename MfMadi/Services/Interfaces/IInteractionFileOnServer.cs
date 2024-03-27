namespace MfMadi.Services.Interfaces
{
    public interface IInteractionFileOnServer
    {
        public Task<string> CreateFile(IFormFile uploadedFile);
        public void DeleteFile(string fileName);
    }
}
