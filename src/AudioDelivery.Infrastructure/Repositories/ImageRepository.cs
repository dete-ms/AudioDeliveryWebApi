using ADImage = AudioDelivery.Domain.Entities.Image;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Infrastructure.Storage;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using AutoMapper;

namespace AudioDelivery.Infrastructure.Repositories;

public class ImageRepository : Repository<ADImage>, IImageRepository
{
    private readonly IStorageService _storageService;
    private readonly IList<string> _exampleDefaultImageGuids = new List<string>();
    private readonly ILogger<ImageRepository> _logger;

    private const int ThumbnailSize = 64;
    private const int MediumSize = 300;
    private const int LargeSize = 640;
    private const string DefaultImageDirectory = "defaults";

    public ImageRepository(
        IStorageService storageService,
        AppDbContext context,
        IMapper mapper,
        ILogger<ImageRepository> logger) : base(context, mapper)
    {
        _storageService = storageService;
        _logger = logger;

        foreach (var name in Enum.GetNames(typeof(ImageType)))
        {
            _exampleDefaultImageGuids.Add(GetDefaultImageGuid(Enum.Parse<ImageType>(name), 1).ToString());
        }
    }

    public static Stream LoadDefaultImageStream(ImageType imageType = ImageType.Album)
    {
        var assembly = typeof(ImageRepository).Assembly;
        var resourceName = $"AudioDelivery.Infrastructure.Seeders.SeedData.default_{imageType.ToString().ToLowerInvariant()}_cover.png";
        var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");
        return stream;
    }

    // Format: c{type}000000-0000-0000-0000-{sizeIndex}
    // e.g., Album=c1..., Large(640)=...1, Medium(300)=...2, Thumbnail(64)=...3
    public static Guid GetDefaultImageGuid(ImageType type, int sizeIndex)
    {
        if (sizeIndex <= 0)
            sizeIndex = 1;
        else if (sizeIndex > 3)
            sizeIndex = 3;

            var typeNum = ((int)type + 1).ToString("D2");
        var sizeNum = sizeIndex.ToString("D12");
        return Guid.Parse($"c{typeNum}00000-0000-0000-0000-{sizeNum}");
    }
    
    public async Task<ADImage> CreateImageAsync(IFormFile formFile, CancellationToken cancellationToken = default)
    {
        var imageGuid = Guid.NewGuid();

        var imageUri = await _storageService.UploadAsync(
                formFile.OpenReadStream(),
                imageGuid.ToString(),
                formFile.ContentType,
                AzureContainerStorageType.Images.ToString().ToLowerInvariant(),
                cancellationToken);

        using var imageHost = await Image.LoadAsync(formFile.OpenReadStream());

        var newImage = new ADImage()
        {
            Id = imageGuid,
            Url = imageUri,
            Width = imageHost.Width,
            Height = imageHost.Height
        };

        await base.AddAsync(newImage, cancellationToken);

        return newImage;
    }

    public async Task<List<ADImage>> CreateSizedImagesAsync(IFormFile? formFile, ImageType imageType, CancellationToken cancellationToken = default, bool isDefault = false)
    {
        if (formFile == null || formFile.Length == 0)
        {
            if (!isDefault)
            {
                throw new ArgumentException("Form file is null or empty, and no default image was requested.");
            }

            using var defaultImageStream = LoadDefaultImageStream();
            return await this.CreateSizedImagesAsync(defaultImageStream, imageType, cancellationToken, isDefault);
        }

        using var inputStream = formFile.OpenReadStream();
        return await this.CreateSizedImagesAsync(inputStream, imageType, cancellationToken, isDefault);
    }

    public async Task<List<ADImage>> CreateSizedImagesAsync(Stream? imageStream, ImageType imageType, CancellationToken cancellationToken = default, bool isDefault = false)
    {
        if (imageStream == null || imageStream.Length == 0)
        {
            if (!isDefault)
            {
                throw new ArgumentException("Image stream is null or empty, and no default image was requested.");
            }

            imageStream = LoadDefaultImageStream(imageType);
        }

        var result = await UploadAsResizedJpegsAsync(imageStream, imageType, cancellationToken, isDefault);
        await AddImagesToDatabaseAsync(result, cancellationToken);
        return result;
    }

    public Task<List<ADImage>> GetDefaultImagesAsync(ImageType imageType, CancellationToken cancellationToken = default)
    {
        var defaultGuids = new List<Guid>()
        {
            GetDefaultImageGuid(imageType, 1),
            GetDefaultImageGuid(imageType, 2),
            GetDefaultImageGuid(imageType, 3),
        };

        return _dbSet.Where(i => defaultGuids.Contains(i.Id)).ToListAsync();
    }

    public async Task<ADImage> UpdateImageAsync(Guid imageId, IFormFile newFile, CancellationToken cancellationToken = default)
    {
        if (IsDefaultImage(imageId, cancellationToken))
        {
            throw new InvalidOperationException("Cannot update a default image.");
        }

        var existingImage = await base.GetByIdTrackedAsync(imageId, cancellationToken);
        if (existingImage == null)
        {
            throw new InvalidOperationException($"Image {imageId} was not found.");
        }

        try
        {
            await _storageService.DeleteAsync(existingImage.Url, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to delete old image blob from storage for image ID {ImageId}. " +
                "The old blob will become orphaned and will be cleaned up later.", imageId);
        }

        var newBlobGuid = Guid.NewGuid();
        string newImageUrl;

        using (var imageStream = newFile.OpenReadStream())
        {
            newImageUrl = await _storageService.UploadAsync(
                imageStream,
                newBlobGuid.ToString(),
                newFile.ContentType,
                AzureContainerStorageType.Images.ToString().ToLowerInvariant(),
                cancellationToken);
        }

        int width, height;
        using (var imageHost = await Image.LoadAsync(newFile.OpenReadStream()))
        {
            width = imageHost.Width;
            height = imageHost.Height;
        }

        existingImage.Url = newImageUrl;
        existingImage.Width = width;
        existingImage.Height = height;

        base.Update(existingImage);

        return existingImage;
    }

    public async Task<List<ADImage>> ReplaceSizedImagesAsync(
        List<Guid> oldImageIds, IFormFile newFile, ImageType type, CancellationToken cancellationToken = default)
    {
        var thereAreNoDefaultImages = !oldImageIds.Any(id => IsDefaultImage(id, cancellationToken));

        if (thereAreNoDefaultImages)
        {
            var getImageTasks = oldImageIds.Select(id => base.GetByIdTrackedAsync(id, cancellationToken));
            var images = await Task.WhenAll(getImageTasks);

            foreach (var image in images)
            {
                if (image != null)
                {
                    try
                    {
                        await _storageService.DeleteAsync(image.Url, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete old image blob from storage for image ID {ImageId}. " +
                            "The old blob will become orphaned and will be cleaned up later.", image.Id);
                    }
                }
            }

            var newImages = await UploadAsResizedJpegsAsync(newFile.OpenReadStream(), type, cancellationToken, false, oldImageIds);

            foreach (var image in newImages)
            {
                base.Update(image);
            }

            return newImages.ToList();
        }

        return await CreateSizedImagesAsync(newFile, type, cancellationToken);
    }

    public async Task<bool> DeleteImageAsync(Guid imageId, bool force = false, CancellationToken cancellationToken = default)
    {
        if (IsDefaultImage(imageId))
        {
            throw new InvalidOperationException(
                $"Cannot delete default image {imageId}. Default images are permanent system resources.");
        }

        var image = await base.GetByIdAsync(imageId, cancellationToken);
        if (image == null)
        {
            return false;
        }

        if (!force)
        {
            var isOrphaned = await this.IsOrphanedAsync(imageId, cancellationToken);
            if (!isOrphaned)
            {
                throw new InvalidOperationException(
                    $"Cannot delete image {imageId} because it is still associated with one or more entities. " +
                    "Use force=true to override this check, or remove all entity associations first.");
            }
        }

        try
        {
            await _storageService.DeleteAsync(image.Url, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to delete image blob from storage for image ID {ImageId}. " +
                "The blob will become orphaned and will be cleaned up later.", imageId);
        }

        base.Delete(image);

        return true;
    }

    public async Task<int> DeleteSizedImagesAsync(List<Guid> imageIds, bool force = false, CancellationToken cancellationToken = default)
    {
        int deletedCount = 0;

        foreach (var imageId in imageIds)
        {
            try
            {
                var deleted = await this.DeleteImageAsync(imageId, force, cancellationToken);
                if (deleted)
                {
                    deletedCount++;
                }
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Failed to delete image with ID {ImageId}. This may be due to the image being in use or a default image. " +
                    "The image will be skipped and the operation will continue for other images.", imageId);
            }
        }

        return deletedCount;
    }

    public async Task<bool> IsOrphanedAsync(Guid imageId, CancellationToken cancellationToken = default)
    {
        if (this.IsDefaultImage(imageId, cancellationToken)) return false;

        var hasAlbums = await _context.AlbumImages
            .AnyAsync(ai => ai.ImageId == imageId);
        if (hasAlbums) return false;

        var hasArtists = await _context.ArtistImages
            .AnyAsync(ai => ai.ImageId == imageId);
        if (hasArtists) return false;

        var hasPlaylists = await _context.PlaylistImages
            .AnyAsync(pi => pi.ImageId == imageId);
        if (hasPlaylists) return false;

        var hasUsers = await _context.UserImages
            .AnyAsync(ui => ui.ImageId == imageId);
        if (hasUsers) return false;

        var hasCategories = await _context.CategoryImages
            .AnyAsync(ci => ci.ImageId == imageId);
        if (hasCategories) return false;

        return true;
    }

    private static string GetContainerDirectory(ImageType imageType)
    {
        return imageType switch
        {
            ImageType.User => "users",
            ImageType.Album => "albums",
            ImageType.Artist => "artists",
            ImageType.Playlist => "playlists",
            ImageType.Category => "categories",
            _ => throw new ArgumentOutOfRangeException(nameof(imageType), $"Unsupported image type: {imageType}")
        };
    }

    private bool IsDefaultImage(Guid imageId, CancellationToken cancellationToken = default)
    {
        return _exampleDefaultImageGuids.Any(e => e.StartsWith(imageId.ToString().Substring(0, 8)));
    }

    private async Task<List<ADImage>> UploadAsResizedJpegsAsync(
        Stream imageStream, 
        ImageType imageType, 
        CancellationToken cancellationToken = default,
        bool isDefault = false,
        List<Guid>? specificFileNames = null)
    {
        var images = new List<ADImage>();
        var sizes = new int[] { LargeSize, MediumSize, ThumbnailSize };
        
        var imageGuids = specificFileNames == null 
            ? new List<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }
            : specificFileNames;

        var fileDirectory = new List<string>() 
        {
            GetContainerDirectory(imageType),
            GetContainerDirectory(imageType),
            GetContainerDirectory(imageType),
        };

        if (isDefault || IsDefaultImage(imageGuids.First()))
        {
            // For default images, we want to use the predefined GUIDs

            for (int i = 0; i < sizes.Length; i++)
            {
                imageGuids[i] = GetDefaultImageGuid(imageType, i + 1);
                fileDirectory[i] = $"{fileDirectory[i]}/{DefaultImageDirectory}";
            }

            images = await base.Query().Where(img => imageGuids.Contains(img.Id)).ToListAsync(cancellationToken);

            if (images.Any())
            {
                return images;
            }
        }

        using var originalImage = await Image.LoadAsync(imageStream, cancellationToken);

        var counter = 0;
        foreach (var size in sizes)
        {
            using var resized = originalImage.Clone(x => x.Resize(size, size));
            using var stream = new MemoryStream();
            await resized.SaveAsJpegAsync(stream, new JpegEncoder
            {
                Quality = 85
            }, cancellationToken);

            stream.Position = 0;

            var imageUri = string.Empty;
            try
            {
                imageUri = await _storageService.UploadAsync(
                    stream,
                    $"{fileDirectory[counter]}/{imageGuids[counter]}",
                    "image/jpeg",
                    AzureContainerStorageType.Images.ToString().ToLowerInvariant(),
                    cancellationToken
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload image to storage for size {Size} and image type {ImageType}.", size, imageType);
                continue;
            }

            images.Add(new ADImage() 
            { 
                Id = imageGuids[counter], 
                Url = imageUri, 
                Width = size, 
                Height = size 
            });
            
            counter++;
        }

        return images;
    }

    private Task AddImagesToDatabaseAsync(IEnumerable<ADImage> images, CancellationToken cancellationToken = default)
    {
        var tasks = images.Select(image => base.AddAsync(image, cancellationToken));
        return Task.WhenAll(tasks);
    }
}
