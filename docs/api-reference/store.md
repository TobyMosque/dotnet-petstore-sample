# Store API

## Get Inventory

```http
GET /store/inventory
```

Returns a JSON object where each key is a pet status value and each value is the count of pets with that status. Pets with a null status appear under the key `"unknown"`.

**Response `200 OK`:**
```json
{
  "available": 18,
  "pending": 5,
  "sold": 12,
  "unknown": 15
}
```

---

## Place an Order

```http
POST /store/order
Content-Type: application/json
```

**Request body:**
```json
{
  "petId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "quantity": 1,
  "shipDate": "2024-06-01T00:00:00Z",
  "status": "placed",
  "complete": false
}
```

| Field | Type | Required |
|-------|------|----------|
| `petId` | UUID \| null | no |
| `quantity` | integer \| null | no |
| `shipDate` | datetime \| null | no |
| `status` | string \| null | no |
| `complete` | boolean | yes |

**Response `200 OK`:** the created order object including its assigned `id`.

---

## Get Order by ID

```http
GET /store/order/{orderId}
```

**Response `200 OK`:** the order object.  
**Response `404`:** order not found.

---

## Delete an Order

```http
DELETE /store/order/{orderId}
```

**Response `204 No Content`:** deleted.  
**Response `404`:** order not found.
