using Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        public void RemoveFile(Guid id, string container)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFile(Guid id, string base64, string container)
        {
            throw new NotImplementedException();
        }
    }
}
