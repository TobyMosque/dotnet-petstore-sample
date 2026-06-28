# Pet Management

## Flow 1 — A store operator adds a new pet to the catalogue

A store operator wants to list a new animal so that customers can find and order it.

The operator submits the pet's name, an optional status, an optional category, one or more photo URLs, and zero or more tag IDs that already exist in the system.

**The system must:**
- Assign a unique ID to the new pet and return the full pet object in the response.
- Store the supplied name, status, category reference, photo URLs, and tag associations exactly as provided.
- Return the category object (id and name) if a `categoryId` was supplied, or `null` if it was omitted.
- Return each tag as an object containing its `id` and `name`.

**Verification:** retrieve the pet by its returned ID and confirm all fields match what was submitted.

---

## Flow 2 — A customer browses pets by availability status

A customer wants to see all pets that are currently available for purchase.

The customer requests pets filtered by the status value `"available"`.

**The system must:**
- Return a JSON array containing only pets whose `status` field is exactly `"available"`.
- Return an empty array if no pets have that status.
- Each item in the array must include: `id`, `name`, `status`, `category` (or null), `photoUrls`, and `tags`.

**Edge cases:**
- Requesting `status=pending` returns only pending pets.
- Requesting `status=sold` returns only sold pets.
- A pet whose status is `null` does **not** appear in any status-filtered result.
- Calling `GET /pet/findByStatus` **without a `status` parameter** returns all pets whose status is `null`. The result must be a non-empty array when the dataset contains pets with a null status.

**Verification:** call `GET /pet/findByStatus?status=available`, confirm every returned pet has `"status": "available"`.

---

## Flow 3 — A customer finds pets by characteristics using tags

A customer wants to narrow down the catalogue using descriptive labels such as `young` or `vaccinated`.

The customer sends one or more tag names as query parameters.

**The system must:**
- Return all pets that have **at least one** tag whose name matches any of the provided values.
- A pet is included in the result if it carries any one of the requested tags — it does not need to carry all of them.
- Return an empty array if no pets carry any of the specified tags.
- Tag matching is case-sensitive: `"Young"` does not match `"young"`.

**Verification:** call `GET /pet/findByTags?tags=young&tags=rescue`, confirm each returned pet has at least one tag named `"young"` or `"rescue"`.

---

## Flow 4 — A store operator updates a pet's information

A store operator needs to correct a pet's name, change its status, reassign its category, replace its photos, or update its tags.

The operator submits the pet's ID along with all updated fields. The update replaces the entire record — partial updates are not supported.

**The system must:**
- Replace the pet's name, status, category, photo URLs, and tag associations with the submitted values.
- Remove any photo URLs not present in the new list.
- Replace the tag associations with only the tags referenced in the new `tagIds` list.
- Return the updated pet object reflecting the new state.
- Return `404` if the supplied ID does not correspond to an existing pet.

**Verification:** update a pet with new values, then retrieve it by ID and confirm all fields reflect the change.

---

## Flow 5 — A store operator removes a pet from the catalogue

A store operator wants to permanently remove a pet that is no longer available.

**The system must:**
- Accept the pet's ID and delete the record.
- Return `204 No Content` on success.
- Also remove all photo URLs associated with the deleted pet.
- Return `404` if the ID does not correspond to an existing pet.

**Verification:** delete a pet, then attempt `GET /pet/{id}` and confirm the response is `404`.

---

## Flow 6 — A customer retrieves a specific pet's details

A customer has seen a pet ID (e.g., from a search result) and wants to view the complete record.

**The system must:**
- Return the full pet object for the given ID.
- Return `null` for optional fields (`status`, `category`) when those values are not set.
- Return `404` if the ID is not found.

**Null field handling:** the dataset intentionally includes pets with a null `status`. When such a pet is retrieved, the response body must contain `"status": null` — not an absent field, not an empty string.
