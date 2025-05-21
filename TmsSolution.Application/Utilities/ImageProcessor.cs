using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.ComponentModel.DataAnnotations;

namespace TmsSolution.Application.Utilities
{
    public class ImageProcessor
    {
        public static async Task<string> ConvertIconToBase64(string iconPath)
        {
            if (string.IsNullOrEmpty(iconPath) || !File.Exists(iconPath))
                throw new ValidationException("Invalid or missing icon file.");

            using var image = await Image.LoadAsync(iconPath);
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(64, 64),
                Mode = ResizeMode.Max
            }));

            using var outputStream = new MemoryStream();
            await image.SaveAsync(outputStream, new JpegEncoder { Quality = 75 });
            var base64String = Convert.ToBase64String(outputStream.ToArray());
            return $"data:image/jpeg;base64,{base64String}";
        }
    }
}
