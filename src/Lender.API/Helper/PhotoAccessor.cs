﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Lender.API.Configuration.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;

namespace Lender.API.Helper
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;

        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
        }

        public PhotoUploadResult AddPhoto(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file == null || file.Length == 0) return new PhotoUploadResult();

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                uploadResult = _cloudinary.Upload(uploadParams);
            }

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            return new PhotoUploadResult
            {
                PublicId = uploadResult?.PublicId,
                Url = uploadResult?.Url.ToString()
            };
        }

        public string DeletePhoto(string publicId)
        {
            if (string.IsNullOrEmpty(publicId)) return null;

            var deleteParams = new DeletionParams(publicId);

            var result = _cloudinary.Destroy(deleteParams);

            return result.Result == "ok" ? result.Result : null;
        }
    }
}
