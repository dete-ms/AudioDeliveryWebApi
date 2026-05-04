using AudioDelivery.Application.Playlists;
using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Application.Artists;
using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Users;
using AudioDelivery.Application.Users.DTOs;
using AudioDelivery.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for the current user's resources, including followed artists and playlists.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IPlaylistService _playlistService;
        private readonly IUserService _userService;

        public MeController(
            IArtistService artistService,
            IPlaylistService playlistService,
            IUserService userService)
        {
            _artistService = artistService;
            _playlistService = playlistService;
            _userService = userService;
        }

        [HttpGet("following/artists")]
        [ProducesResponseType(typeof(PaginatedResult<ArtistSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFollowedArtists(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
        {
            // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8

            var result = await _artistService.GetFollowedArtistsAsync(userId, offset, limit, cancellationToken);
            return Ok(result);
        }

        [HttpGet("playlists")]
        [ProducesResponseType(typeof(PaginatedResult<PlaylistSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentUserPlaylists(Guid userId, CancellationToken cancellationToken = default)
        {
            // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8

            var result = await _playlistService.GetCurrentUserPlaylistsAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get detailed profile information about the current user.
        /// Temporarily accepts userId as a query parameter until auth is implemented.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentUser([FromQuery] Guid userId)
        {
            // TODO: Replace userId query param with auth-based current user (from JWT claims)
            var result = await _userService.GetCurrentUserAsync(userId);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
