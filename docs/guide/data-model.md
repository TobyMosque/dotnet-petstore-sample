# Data Model

## Pet

A pet is the central entity. It belongs to one optional category and can have many tags and many photo URLs.

| Field | Type | Required | Notes |
|-------|------|----------|-------|
| `id` | UUID | yes | Assigned by the server |
| `name` | string | yes | Cannot be empty |
| `status` | string \| null | no | One of `available`, `pending`, `sold`, or null |
| `category` | object \| null | no | The category the pet belongs to |
| `photoUrls` | string[] | yes | At least one URL expected; can be empty list |
| `tags` | object[] | yes | Zero or more tags |

**Category** is optional (`categoryId` may be null). When present:

| Field | Type |
|-------|------|
| `id` | UUID |
| `name` | string \| null |

**Tag**:

| Field | Type |
|-------|------|
| `id` | UUID |
| `name` | string \| null |

---

## Order

An order represents a purchase request for a pet.

| Field | Type | Required | Notes |
|-------|------|----------|-------|
| `id` | UUID | yes | Assigned by the server |
| `petId` | UUID \| null | no | The pet being ordered |
| `quantity` | integer \| null | no | Number of units |
| `shipDate` | datetime \| null | no | Requested ship date |
| `status` | string \| null | no | One of `placed`, `approved`, `delivered`, or null |
| `complete` | boolean | yes | Whether the order is finalised |

---

## User

| Field | Type | Required | Notes |
|-------|------|----------|-------|
| `id` | UUID | yes | Assigned by the server |
| `username` | string | yes | Must be unique across all users |
| `firstName` | string \| null | no | |
| `lastName` | string \| null | no | |
| `email` | string \| null | no | |
| `phone` | string \| null | no | |
| `userStatus` | integer | yes | `0` = inactive, `1` = active, `2` = banned |
