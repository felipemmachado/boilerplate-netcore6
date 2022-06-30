using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFile(Guid id, string base64, string container);
        void RemoveFile(Guid id, string container);
    }
}
