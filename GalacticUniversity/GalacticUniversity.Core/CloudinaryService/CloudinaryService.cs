﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GalacticUniversity.Core.CloudinaryService
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
                );
            _cloudinary = new Cloudinary(account);
        }
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            using var stream = file.OpenReadStream();
            var uploadsParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                
                Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face"),
               


            };
            var uploadResult = await _cloudinary.UploadAsync(uploadsParams);
            if (uploadResult == null || uploadResult.SecureUrl == null)
            {
                return null;
            }
            return uploadResult.SecureUrl.ToString();
            }
        public async Task<string> UploadDocumentAsync(IFormFile file)
        {
         
            if (file == null || file.Length == 0)
            {
                return null;
            }

            try
            {
                
                string fileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
                if (string.IsNullOrEmpty(fileExtension) || fileExtension != ".pdf")
                {
                    return null;
                }

               
                using var stream = file.OpenReadStream();

            
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    
                };

               
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult == null || uploadResult.SecureUrl == null)
                {
                    return null;
                }

                return uploadResult.SecureUrl.ToString();
            }
            catch (Exception)
            {
                
                return null;
            }
        }

    }

}
