using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AudioDelivery.Application.Common.Interfaces;

public interface IImageRepository : IRepository<Image>
{
    /// <summary>
    /// Asynchronously creates a new image from the specified uploaded file.
    /// </summary>
    /// <param name="file">The uploaded file to use for creating the image. Must be a valid image file. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created image.</returns>
    Task<Image> CreateImageAsync(IFormFile file, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a collection of images in various predefined sizes from the provided file.
    /// </summary>
    /// <remarks>The method processes the input file and generates multiple images according to predefined
    /// size requirements. The caller is responsible for ensuring that the input file is a valid and supported image. If
    /// the operation is canceled via the cancellation token, the returned task will be canceled.</remarks>
    /// <param name="formFile">The uploaded image file to process. Must be a valid image format supported by the system.</param>
    /// <param name="defaultImageType">The image type to use as the default for processing and output sizing.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <param name="isDefault"><see langword="true"/> to mark the generated images as default images; otherwise, <see langword="false"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Image"/>s generated in the
    /// specified sizes. The list will be empty if the input file is invalid or no images are created.</returns>
    Task<List<Image>> CreateSizedImagesAsync(IFormFile? formFile, ImageType defaultImageType, CancellationToken cancellationToken = default, bool isDefault = false);

    /// <summary>
    /// Asynchronously creates a set of images in various predefined sizes from the provided image stream.
    /// </summary>
    /// <param name="imageStream">The input stream containing the image data to be processed. The stream must be readable and positioned at the
    /// beginning of the image data.</param>
    /// <param name="defaultImageType">The image type to use for the generated images if the type cannot be determined from the input stream.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <param name="isDefault"><see langword="true"/> to mark the generated images as default images; otherwise, <see langword="false"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Image"/>s generated in the
    /// specified sizes. The list will be empty if the input file is invalid or no images are created.</returns>
    Task<List<Image>> CreateSizedImagesAsync(Stream? imageStream, ImageType defaultImageType, CancellationToken cancellationToken = default, bool isDefault = false);

    /// <summary>
    /// Asynchronously retrieves the list of default images for the specified image type.
    /// </summary>
    /// <param name="imageType">The type of image for which to retrieve the default images.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Image"/>
    /// objects representing the default images for the specified type. The list will be empty if no default images are
    /// available.</returns>
    Task<List<Image>> GetDefaultImagesAsync(ImageType imageType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates the image with the specified identifier using the provided file.
    /// </summary>
    /// <param name="imageId">The unique identifier of the image to update.</param>
    /// <param name="newFile">The new image file to replace the existing image. Must not be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="Image"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the image is a default image (cannot be updated).</exception>
    Task<Image> UpdateImageAsync(Guid imageId, IFormFile newFile, CancellationToken cancellationToken = default);

    /// <summary>
    /// Replaces a collection of existing sized images with a new image of the specified type asynchronously.
    /// </summary>
    /// <remarks>All images specified by the provided identifiers are replaced with the new image, which is
    /// processed according to the specified image type. The operation is performed asynchronously and supports
    /// cancellation via the provided token.</remarks>
    /// <param name="oldImageIds">The list of unique identifiers for the images to be replaced. Each identifier corresponds to an existing image
    /// that will be removed and substituted with the new image.</param>
    /// <param name="newFile">The new image file to use as the replacement. Must be a valid, non-null file containing image data.</param>
    /// <param name="imageType">The type of image to create for the replacement. Determines the sizing and processing applied to the new image.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of the newly created images
    /// that replaced the originals. The list will be empty if no images were replaced.</returns>
    /// <exception cref="ArgumentException">Thrown if oldImageIds count is not 3.</exception>
    /// <exception cref="InvalidOperationException">Thrown if any of the old images are default images.</exception>
    Task<List<Image>> ReplaceSizedImagesAsync(List<Guid> oldImageIds, IFormFile newFile, ImageType imageType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the image with the specified identifier.
    /// </summary>
    /// <param name="imageId">The unique identifier of the image to delete.</param>
    /// <param name="force">If <see langword="true"/>, deletes the image even if it is referenced elsewhere; otherwise, the operation may
    /// fail if the image is in use.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous delete operation. The task result is <see langword="true"/> if the image
    /// was successfully deleted; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the image is still in use (when force=false) or if it's a default image.</exception>
    Task<bool> DeleteImageAsync(Guid imageId, bool force = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes all sized image variants associated with the specified image IDs.
    /// </summary>
    /// <param name="imageIds">A list of unique identifiers for the images whose sized variants are to be deleted. Cannot be null or empty.</param>
    /// <param name="force">If set to <see langword="true"/>, forces deletion even if the images are currently in use; otherwise, only
    /// deletes images that are not in use.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of sized images that were
    /// successfully deleted.</returns>
    /// <exception cref="InvalidOperationException">Thrown if any image is a default image.</exception>
    Task<int> DeleteSizedImagesAsync(List<Guid> imageIds, bool force = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns true if the image has no remaining associations across
    /// any of the five join tables.
    /// </summary>
    Task<bool> IsOrphanedAsync(Guid imageId, CancellationToken cancellationToken = default);
}
