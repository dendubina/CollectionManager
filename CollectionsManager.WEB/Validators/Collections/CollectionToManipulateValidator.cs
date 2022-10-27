using System.Collections.Generic;
using System.IO;
using System.Linq;
using CollectionManager.WEB.Validators.CustomFields;
using CollectionsManager.BLL.DTO.Collections;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CollectionManager.WEB.Validators.Collections
{
    public class CollectionToManipulateValidator : AbstractValidator<CollectionToManipulateDto>
    {
        private const double MaxImageSizeMB = 1;

        private readonly Dictionary<string, List<byte[]>> _fileSignatures = new()
        {
            {
                ".png",
                new List<byte[]>
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
                }
            },
            {
                ".bmp",
                new List<byte[]>
                {
                    new byte[] { 0x42, 0x4D },
                }
            },
            {
                ".jpeg",
                new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            {
                ".jpg",
                new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                }
            },
        };

        public CollectionToManipulateValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty();

            RuleFor(x => x.Category)
                .IsInEnum();

            RuleFor(x => x.Description)
                .NotNull().NotEmpty();

            RuleForEach(x => x.CustomFields)
                .SetValidator(new CustomFieldToManipulateValidator());

            RuleFor(x => x.Image)
                .Must(IsValidImageExtension).WithMessage("Wrong image extension")
                .Must(IsValidImageSize).WithMessage($"Image size must be less than {MaxImageSizeMB}MB")
                .When(product => product.Image is not null);
        }

        private bool IsValidImageExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!_fileSignatures.ContainsKey(extension))
            {
                return false;
            }

            using var reader = new BinaryReader(file.OpenReadStream());

            var signatures = _fileSignatures[extension];

            var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

            return signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
        }

        private bool IsValidImageSize(IFormFile file)
        {
            return !(file.Length > 1048576 * MaxImageSizeMB); // 1 MB * MaxImageSize
        }
    }
}
