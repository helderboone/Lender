using Microsoft.AspNetCore.Http;

namespace Lender.API.Helper
{
    public interface IPhotoAccessor
    {
        PhotoUploadResult AddPhoto(IFormFile file);

        string DeletePhoto(string publicId);
    }
}
