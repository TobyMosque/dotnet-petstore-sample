# Pets API

## Add a Pet

```http
POST /pet
Content-Type: application/json
```

**Request body:**
```json
{
  "name": "Buddy",
  "status": "available",
  "categoryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "photoUrls": ["https://example.com/buddy.jpg"],
  "tagIds": ["a1b2c3d4-e5f6-7890-abcd-ef1234567890"]
}
```

| Field | Type | Required |
|-------|------|----------|
| `name` | string | yes |
| `status` | string \| null | no |
| `categoryId` | UUID \| null | no |
| `photoUrls` | string[] | no |
| `tagIds` | UUID[] | no |

**Response `200 OK`:** the created pet object.

---

## Update a Pet

```http
PUT /pet
Content-Type: application/json
```

Same body as Add, plus `id` (required):

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Buddy Updated",
  "status": "pending",
  "categoryId": null,
  "photoUrls": [],
  "tagIds": []
}
```

**Response `200 OK`:** the updated pet object.  
**Response `404`:** pet not found.

---

## Find Pets by Status

```http
GET /pet/findByStatus?status=available
```

Returns an array of pets whose `status` exactly matches the query value.

```json
[
  {
    "id": "...",
    "name": "Buddy",
    "status": "available",
    "category": { "id": "...", "name": "Dogs" },
    "photoUrls": ["https://example.com/buddy.jpg"],
    "tags": [{ "id": "...", "name": "young" }]
  }
]
```

**Response `200 OK`:** array (may be empty).

---

## Find Pets by Tags

```http
GET /pet/findByTags?tags=young&tags=vaccinated
```

Returns pets that have **at least one** of the specified tag names.

**Response `200 OK`:** array (may be empty).

---

## Get Pet by ID

```http
GET /pet/{petId}
```

**Response `200 OK`:** the pet object.  
**Response `404`:** pet not found.

---

## Delete a Pet

```http
DELETE /pet/{petId}
```

**Response `204 No Content`:** deleted successfully.  
**Response `404`:** pet not found.
