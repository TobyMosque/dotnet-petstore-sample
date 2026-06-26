# Store & Orders

## Flow 1 — A customer checks how many pets are available

A customer wants to know at a glance how many pets are in each status category before browsing.

The customer requests the store inventory.

**The system must:**
- Return a JSON object where each key is a status value and each value is the count of pets in that status.
- Pets whose `status` is `null` must appear under the key `"unknown"`.
- Keys with a count of zero are not required to appear in the response.
- The counts must reflect the current state of the database at the time of the request.

**Verification:** call `GET /store/inventory`, confirm the response is a JSON object with string keys and integer values, and that the sum of all values equals the total number of pets in the system.

---

## Flow 2 — A customer places an order for a pet

A customer has chosen a pet and wants to reserve it for purchase.

The customer submits an order specifying the pet, the quantity, a requested ship date, and a status of `"placed"`.

**The system must:**
- Create the order record and return it with an assigned `id`.
- Preserve all submitted field values in the response, including any `null` values for optional fields.
- The `complete` field must always be present in the response; if not submitted, it defaults to `false`.
- Accepting an order does not automatically change the associated pet's status.

**Verification:** place an order, then retrieve it by the returned ID and confirm all fields match.

---

## Flow 3 — A customer checks the status of their order

A customer submitted an order and wants to confirm it was received and check its current status.

**The system must:**
- Return the full order object for the given UUID.
- Return all fields including those that are `null` — for example, if `shipDate` was not provided, the response must contain `"shipDate": null`.
- Return `404` if the order ID is not found.

**Null field handling:** orders in the pre-loaded dataset may have `null` values for `petId`, `quantity`, `shipDate`, and `status`. Retrieving such an order must return these fields as `null`, not as absent keys.

---

## Flow 4 — A customer cancels an order

A customer changed their mind and wants to remove their order.

**The system must:**
- Delete the order and return `204 No Content`.
- Leave the associated pet unaffected — the pet's status does not change when its order is cancelled.
- Return `404` if the order ID does not exist.

**Verification:** delete an order, then attempt `GET /store/order/{id}` and confirm `404` is returned.

---

## Flow 5 — Inventory reflects changes after pet status updates

A store operator updates a pet's status from `"available"` to `"sold"`. A customer then requests the inventory.

**The system must:**
- Reflect the updated status in the inventory counts immediately — no caching layer introduces a delay.
- The count for `"available"` must decrease by one and the count for `"sold"` must increase by one compared to the values before the update.
