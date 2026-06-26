# Pet Concepts

## Lifecycle

A pet moves through a lifecycle represented by its `status` field:

```
null → available → pending → sold
```

- **null** — status not yet assigned; the pet exists but is not listed
- **available** — the pet is listed and can be ordered
- **pending** — an order has been placed; the pet is reserved
- **sold** — the transaction is complete

Status transitions are not enforced server-side — any client may set any status at any time. The status is purely informational.

## Categories

Each pet belongs to at most one category (e.g., Dogs, Cats, Birds). A category has a name, which may be null in the dataset. Removing a pet does not remove its category.

## Tags

Tags are free-form labels (e.g., `young`, `vaccinated`). A pet may have zero or more tags. Tags are shared across pets — the same tag entity can appear on multiple pets.

## Photos

A pet may have multiple photo URLs. Photos are owned by the pet and are deleted when the pet is deleted.

## Search Behaviour

**By status** (`GET /pet/findByStatus?status=<value>`):
- Returns all pets whose `status` exactly matches the given value.
- Passing an empty string or omitting the parameter returns pets with a null status.
- Status comparison is case-sensitive.

**By tags** (`GET /pet/findByTags?tags=<tag1>&tags=<tag2>`):
- Returns all pets that have **at least one** of the listed tag names.
- Tag name comparison is case-sensitive.
- Passing no tag names returns an empty list.

## Deletion

Deleting a pet permanently removes it and all its associated photo URLs. Tags and categories are not affected.
