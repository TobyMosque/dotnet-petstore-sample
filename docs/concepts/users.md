# Users

## Registration

A user is created via `POST /user`. The `username` field is required and must be unique. All other fields (`firstName`, `lastName`, `email`, `phone`) are optional and may be null.

Attempting to create a second user with the same username results in a database error (unique constraint).

## Authentication

Login is performed via `GET /user/login?username=<value>&password=<value>`.

- If the credentials match a stored user, the response is `200 OK` with a session token string in the body.
- If no user matches, the response is `401 Unauthorized`.
- The token format is implementation-defined and opaque to the client.

Logout via `GET /user/logout` always returns `200 OK` regardless of session state.

## Profile

A user profile is retrieved by username: `GET /user/{username}`.

- Returns `404` if the username does not exist.
- The response never includes the `password` field.

Updating a user (`PUT /user/{username}`) replaces all mutable fields. The username itself cannot be changed through this endpoint — the `{username}` path parameter identifies the record.

Deleting a user (`DELETE /user/{username}`) permanently removes the record and returns `204`. Returns `404` if the user does not exist.

## User Status

`userStatus` is an integer flag:
- `0` — inactive
- `1` — active  
- `2` — banned

The API stores and returns this value but does not enforce any access restrictions based on it.
