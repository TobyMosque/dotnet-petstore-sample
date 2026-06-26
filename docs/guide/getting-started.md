# Getting Started

## Base URL

All API endpoints are served under:

```
http://localhost:5000
```

## Request Format

- Content-Type: `application/json`
- All request and response bodies are JSON
- IDs are UUIDs (e.g., `"3fa85f64-5717-4562-b3fc-2c963f66afa6"`)
- Dates use ISO 8601 format (e.g., `"2024-03-15T10:00:00Z"`)

## Response Conventions

| Status | Meaning |
|--------|---------|
| `200 OK` | Success, body contains the result |
| `204 No Content` | Success, no body (used for deletes) |
| `400 Bad Request` | Invalid request payload |
| `401 Unauthorized` | Authentication required |
| `404 Not Found` | Resource does not exist |

## Quick Example

```http
GET /pet/findByStatus?status=available HTTP/1.1
Host: localhost:5000
Accept: application/json
```

```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Buddy",
    "status": "available",
    "category": { "id": "...", "name": "Dogs" },
    "photoUrls": ["https://example.com/buddy.jpg"],
    "tags": [{ "id": "...", "name": "young" }]
  }
]
```

## Initial Data

The API ships with a pre-loaded dataset containing:

- **5 categories**: Dogs, Cats, Birds, Reptiles, Fish
- **7 tags**: young, old, male, female, vaccinated, trained, rescue
- **50 pets** across all categories, each with 1–4 photos and 0–3 tags
- **30 orders** linked to existing pets
- **20 users** with varying profile completeness

Some fields are intentionally `null` in the dataset (e.g., a pet may have no status, a user may have no email). This reflects real-world data quality and the API must handle it gracefully.
