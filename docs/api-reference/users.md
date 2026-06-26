# Users API

## Create a User

```http
POST /user
Content-Type: application/json
```

**Request body:**
```json
{
  "username": "johndoe",
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "password": "secret",
  "phone": "+1-555-0100",
  "userStatus": 1
}
```

| Field | Type | Required |
|-------|------|----------|
| `username` | string | yes — must be unique |
| `firstName` | string \| null | no |
| `lastName` | string \| null | no |
| `email` | string \| null | no |
| `password` | string | yes |
| `phone` | string \| null | no |
| `userStatus` | integer | yes |

**Response `200 OK`:** the created user object (password not included).

---

## Get User by Username

```http
GET /user/{username}
```

**Response `200 OK`:** the user object.  
**Response `404`:** username not found.

---

## Update a User

```http
PUT /user/{username}
Content-Type: application/json
```

Same body as Create (excluding `username` — taken from the path). Replaces all mutable fields.

**Response `200 OK`:** the updated user object.  
**Response `404`:** username not found.

---

## Delete a User

```http
DELETE /user/{username}
```

**Response `204 No Content`:** deleted.  
**Response `404`:** username not found.

---

## Login

```http
GET /user/login?username=johndoe&password=secret
```

**Response `200 OK`:** a session token string.  
**Response `401 Unauthorized`:** credentials do not match.

---

## Logout

```http
GET /user/logout
```

**Response `200 OK`:** always succeeds.
