# AudioDelivery API — Postman Collection

## Import

1. Open Postman → **Import** (top-left)
2. Select `AudioDelivery.postman_collection.json`
3. The collection loads with 9 folders and 28 requests

---

## Starting the API

```bash
dotnet run --project src/AudioDelivery.Api
```

The API runs at `http://localhost:5150` (already set as the `baseUrl` collection variable).

---

## Setting Collection Variables

Most endpoints need real GUIDs from your database. After importing:

1. Click the collection name → **Variables** tab
2. Fill in the **Current Value** column as you discover IDs:

| Variable | How to get it |
|---|---|
| `userId` | Run **Get Current User Profile** after seeding, check the DB, or inspect the DataSeeder output |
| `artistId` / `artistId2` | Run **Get Related Artists** or query the DB after seeding |
| `albumId` / `albumId2` | Run **Get New Releases** — IDs are in the response |
| `trackId` / `trackId2` | Run **Get Album Tracks** with a known albumId |
| `playlistId` | Run **Create Playlist** and copy the `id` from the `201` response |
| `categoryId` | Pre-filled: `b1000000-0000-0000-0000-000000000001` (Music) — no action needed |

---

## Suggested Execution Order

Start with requests that use seeded data (no variables needed), then work down:

1. **Genres → Get Available Genre Seeds** ✅ seeded, no variables
2. **Categories → Get All Categories** ✅ seeded, no variables
3. **Categories → Get Single Category** ✅ uses pre-filled `categoryId`
4. **Albums → Get New Releases** → copy an `albumId` into variables
5. **Artists → Get Several Artists** → populate `artistId` first
6. **Tracks → Get Several Tracks** → populate `trackId` first
7. **Users → Get Current User Profile** → populate `userId` first
8. **Playlists → Create Playlist** → copy the returned `id` into `playlistId`
9. **Playlists → Add Items to Playlist** → requires both `playlistId` and `trackId`
10. **Library → Save Items to Library** → requires `userId` and `trackId`
11. **Search → Search Items** ✅ works immediately (searches by keyword "rock")

---

## Request Body Reference

### PATCH /api/v1/albums/{id}

```json
{
  "name": "Updated Album Title",
  "albumType": 0,
  "isPublic": true,
  "label": "Test Label",
  "artistIds": ["{{artistId}}"]
}
```

`albumType` values: `0` = Album, `1` = Single, `2` = Compilation

### POST /api/v1/users/{userId}/playlists

```json
{
  "name": "My Test Playlist",
  "isPublic": true,
  "collaborative": false,
  "description": "Created via Postman"
}
```

### PUT /api/v1/playlists/{id}

All fields optional — only provided fields are updated:

```json
{
  "name": "Renamed Playlist",
  "isPublic": false,
  "collaborative": true,
  "description": "Updated via Postman"
}
```

### POST /api/v1/playlists/{id}/items

```json
{
  "uris": ["spotify:track:{{trackId}}"],
  "position": 0
}
```

`position` is zero-based. Omit to append to the end.

### PUT/DELETE /api/v1/me/library

```json
{
  "uris": "spotify:track:{{trackId}}"
}
```

Multiple items: `"uris": "spotify:track:{id1},spotify:album:{id2}"` (max 40)

---

## Seeded Category GUIDs

Categories and Genres are seeded with deterministic GUIDs — use these directly without querying:

| GUID | Category |
|---|---|
| `b1000000-0000-0000-0000-000000000001` | Music |
| `b1000000-0000-0000-0000-000000000002` | New Releases |
| `b1000000-0000-0000-0000-000000000003` | Upcoming Releases |
| `b1000000-0000-0000-0000-000000000004` | Made For You |
| `b1000000-0000-0000-0000-000000000005` | Discover |
| `b1000000-0000-0000-0000-000000000006` | Fresh Finds |

Genres follow the same pattern (same GUID format, different table):

| GUID | Genre |
|---|---|
| `b1000000-0000-0000-0000-000000000001` | Rock |
| `b1000000-0000-0000-0000-000000000002` | Alternative Rock |
| `b1000000-0000-0000-0000-000000000003` | Indie Rock |

> **Note:** Category and Genre GUIDs share the same pattern but live in separate tables, so there is no collision.

---

## Notes

- **Authentication (Phase 8 TODO):** The `userId` query parameter on `/me`, `/me/library`, and `/me/library/contains` is a temporary workaround. Once JWT is implemented these will resolve the user from the token automatically.
- **DataSeeder:** User, Artist, Album, Track, and Playlist entities are seeded with `Guid.NewGuid()` — IDs change each time the database is re-seeded. Re-populate your collection variables after re-seeding.
