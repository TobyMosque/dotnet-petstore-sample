# Data API

These endpoints are intended for environment setup and data management. They are not part of the public-facing store API.

## Generate Sample Data

```http
POST /data/generate
```

Clears all existing records and inserts a fresh dataset with fixed categories, fixed tags, and randomised pets, orders, and users.

**Response `200 OK`:** a summary string, e.g. `"Generated 5 categories, 7 tags, 50 pets, 30 orders, 20 users."`

---

## Export Data

```http
GET /data/export
```

Returns the current database contents as a `.7z` archive containing one CSV file per entity: `categories.csv`, `tags.csv`, `pets.csv`, `pet_photos.csv`, `pet_tags.csv`, `orders.csv`, `users.csv`.

**Response `200 OK`:** binary stream, `Content-Type: application/x-7z-compressed`, filename `petstore-export.7z`.

---

## Import Data

```http
POST /data/import
Content-Type: multipart/form-data
```

Accepts a `.7z` archive in the same format produced by Export. Clears the database and restores all records from the archive.

**Response `200 OK`:** `"Import complete."`
