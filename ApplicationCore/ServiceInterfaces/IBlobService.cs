using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IBlobService
    {
        Task<Uri> UploadFileBlobAsync(Stream content, string contentType, string fileName);
        Task<bool> CheckIfBlobExistsAsync(string blobName);
        string ContainerName { get; set; }
    }
}
