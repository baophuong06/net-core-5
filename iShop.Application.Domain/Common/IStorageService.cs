using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace iShop.Application.Domain.Common
{
   public interface IStorageService
    {
        string GetFileUrl(string fileName);
        Task SaveFileAsnyc(Stream mediaBinaryStream, string fileName);
        Task DeleteFileAsnyc(string fileName);
    }
}
