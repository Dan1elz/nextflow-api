using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Utils;

namespace nextflow.Application.Utils;

public class LocalStorageService : IStorageService
{
    public string BasePath { get; set; } = "assets/images/products";
    public async Task<byte[]> GetAsync(string fileName, CancellationToken ct)
    {
        var filePath = Path.Combine(BasePath, fileName);
        if (!File.Exists(filePath)) return Array.Empty<byte>();

        return await File.ReadAllBytesAsync(filePath, ct);
    }

    public async Task<string> SaveAsync(IFileData file, CancellationToken ct)
    {
        if (file == null) throw new BadRequestException("O arquivo é obrigatório");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension)) throw new BadRequestException("Extensão não permitida");

        if (!Directory.Exists(BasePath)) Directory.CreateDirectory(BasePath);

        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(BasePath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        using var input = file.OpenReadStream();
        await input.CopyToAsync(stream, ct);

        return fileName;
    }

    public void DeleteAsync(string fileName)
    {
        var filePath = Path.Combine(BasePath, fileName);
        if (File.Exists(filePath)) File.Delete(filePath);
    }
}
