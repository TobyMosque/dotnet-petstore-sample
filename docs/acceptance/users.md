# User Management

## Flow 1 — A new visitor registers an account

A visitor wants to create an account so they can log in and manage their profile.

The visitor submits a username, a password, and optionally their first name, last name, email, and phone number. They also set their initial `userStatus`.

**The system must:**
- Create the user and return the user object with an assigned `id`.
- The `username` must be unique — if a user with the same username already exists, the request must fail.
- The response must never include the `password` field.
- Optional fields (`firstName`, `lastName`, `email`, `phone`) submitted as `null` must be returned as `null` in the response.

**Verification:** create a user, then retrieve them by username and confirm the returned object matches all submitted fields (excluding password).

---

## Flow 2 — A registered user logs in

A user wants to authenticate to confirm their identity.

The user supplies their username and password as query parameters to `GET /user/login`.

**The system must:**
- Return `200 OK` with a non-empty token string when the username and password match a stored user.
- Return `401 Unauthorized` when no user matches the provided credentials.
- The token format is opaque — callers should treat it as a string and not parse it.

**Verification:**
- Log in with valid credentials and confirm a non-empty string is returned.
- Attempt login with an incorrect password and confirm `401` is returned.
- Attempt login with a username that does not exist and confirm `401` is returned.

---

## Flow 3 — A user views their own profile

A logged-in user wants to see their stored profile details.

**The system must:**
- Return the full user object for the given username.
- Include all fields, using `null` for optional fields that were not provided at registration.
- Return `404` if the username does not exist.
- Never include the `password` field in the response.

**Null field handling:** users in the pre-loaded dataset intentionally have `null` values for some fields (e.g., `email`, `phone`). Retrieving such a user must return `"email": null` — not an absent key.

---

## Flow 4 — A user updates their contact information

A user wants to correct their email address or phone number.

The user sends an update request to `PUT /user/{username}` with their revised details.

**The system must:**
- Replace all mutable fields with the submitted values.
- A field submitted as `null` overwrites the previous value — it does not leave the field unchanged.
- Return the updated user object.
- Return `404` if the username does not exist.

**Verification:** update a user's `email` from a non-null value to `null`, then retrieve the user and confirm `"email": null` is returned.

---

## Flow 5 — An administrator removes a user account

An administrator wants to permanently delete a user account.

**The system must:**
- Delete the user and return `204 No Content`.
- Return `404` if the username does not exist.

**Verification:** delete a user, then attempt `GET /user/{username}` and confirm `404` is returned.

---

## Flow 6 — A user logs out

A logged-in user ends their session.

**The system must:**
- Return `200 OK` regardless of whether the user was actually logged in.
- No session state is required — the endpoint is always successful.
