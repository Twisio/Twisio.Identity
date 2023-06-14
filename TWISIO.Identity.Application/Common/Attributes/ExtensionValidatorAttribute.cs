using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TWISIO.Identity.Application.Common.Exceptions;

namespace TWISIO.Identity.Application.Common.Attributes
{
    /// <summary>
    /// Атирибут валидации расширения файла
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ExtensionValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Допустимые расширения
        /// </summary>
        public string Extensions { get; set; } = string.Empty;

        public override bool IsValid(object value)
        {
            var extensions = Extensions.Trim('.').Split(",");

            if (value is IFormFile file)
            {
                Validation(file, extensions);
                return true;
            }
            if (value is List<IFormFile> files)
            {
                foreach (var arrayFile in files)
                {
                    Validation(arrayFile, extensions);
                }
                return true;
            }
            else
                throw new BadRequestException("Разрешён только IFormFile тип данных");
        }

        private bool Validation(IFormFile file, string[] extensions)
        {
            var fileExtension = Path.GetExtension(file.FileName).Trim('.');
            if (extensions.Contains(fileExtension))
                return true;
            else
                throw new BadRequestException(ErrorMessage == string.Empty
                    ? "Неподдерживаемый формат файла"
                    : ErrorMessage!);
        }
    }
}
