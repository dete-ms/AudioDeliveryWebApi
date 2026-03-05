using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the self-referencing many-to-many relationship between Users (followers).
/// NOTE: Cascade delete is intentionally set to NoAction on both FKs because SQL Server
/// does not allow multiple cascade paths from the same table (Users) to the same table (UserFollowedUser).
/// User follow relationships must be removed explicitly in the service layer before deleting a user.
/// </summary>
public class UserFollowedUser
{
    public Guid FollowerId { get; set; }
    public User Follower { get; set; } = null!;

    public Guid FollowedUserId { get; set; }
    public User FollowedUser { get; set; } = null!;
}
