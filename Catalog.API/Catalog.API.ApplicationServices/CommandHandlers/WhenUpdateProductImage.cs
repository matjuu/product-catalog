using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Contracts.Commands;
using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.Storage;
using Microsoft.AspNetCore.Http;
using Product = Catalog.API.Contracts.Views.Product;

namespace Catalog.API.ApplicationServices.CommandHandlers
{
    public class WhenUpdateProductImage : HandlerBase<UpdateProductImage, Product>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IProductImageStorage _productImageStorage;
        private readonly IMapper _mapper;

        private readonly List<string> _supportedImageTypes;
        private const int FILE_SIZE_LIMIT = 10000000;

        public WhenUpdateProductImage(IProductImageStorage productImageStorage, IMapper mapper, IProductsRepository productsRepository)
        {
            _supportedImageTypes = new List<string> { ".jpeg", ".jpg", ".png", ".tiff" };
            _productImageStorage = productImageStorage;
            _mapper = mapper;
            _productsRepository = productsRepository;
        }

        protected override async Task<Product> HandleCore(UpdateProductImage request)
        {
            var aggregate = await _productsRepository.Get(request.Id);
            if(aggregate == null) throw new NotFoundException();

            var imageName = Guid.NewGuid() + GetImageExtension(request.Image);
            var command = new Domain.Contracts.Commands.UpdateProductImage(imageName);

            aggregate.UpdateImage(command);
            ValidateImageFile(request.Image);
            SaveImage(request.Image, imageName);

            var response = await _productsRepository.Update(aggregate);
            return _mapper.Map<Domain.Aggregates.Product, Product>(response);
        }

        private void SaveImage(IFormFile image, string imageName)
        {
            using (var stream = new MemoryStream())
            {
                image.CopyTo(stream);

                var fileName = imageName;
                var fileBytes = stream.ToArray();
                _productImageStorage.Save(fileName, fileBytes);
            }
        }

        private void ValidateImageFile(IFormFile request)
        {
            ThrowIfDoesNotContainAnImage(request);
            ThrowIfNotSupportedFileType(request);
            ThrowIfFileSizeExceedsLimit(request);
        }

        private void ThrowIfFileSizeExceedsLimit(IFormFile image)
        {
            if (image.Length > FILE_SIZE_LIMIT)
                throw new ApiException("Image file size exceeds limits", "ImageFileTooLarge", HttpStatusCode.BadRequest);
        }

        private void ThrowIfNotSupportedFileType(IFormFile image)
        {
            var fileExtension = GetImageExtension(image);
            if (!_supportedImageTypes.Contains(fileExtension))
                throw new ApiException("Image file is not of a supported format", "ImageFormatNotSupported",
                    HttpStatusCode.BadRequest);
        }

        private static string GetImageExtension(IFormFile image)
        {
            return Path.GetExtension(image.FileName)?.ToLowerInvariant();
        }

        private void ThrowIfDoesNotContainAnImage(IFormFile image)
        {
            if (image == null) throw new ApiException("Image must be provided", "ImageNotProvided", HttpStatusCode.BadRequest);
        }
    }
}