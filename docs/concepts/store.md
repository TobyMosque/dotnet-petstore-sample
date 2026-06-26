# Store & Orders

## Inventory

The inventory is a live count of pets grouped by their current status. It reflects the state of the `pets` table at the moment of the request.

Each key in the inventory response is a status value (including `null` represented as `"unknown"`) and each value is the count of pets with that status.

Example:
```json
{
  "available": 18,
  "pending": 5,
  "sold": 12,
  "unknown": 15
}
```

Pets whose `status` is null appear under the key `"unknown"`.

## Placing an Order

An order records the intent to purchase a pet. All fields except `complete` are optional — the API does not validate that the referenced pet exists or is available.

`complete` defaults to `false` when not provided but is always present in the response.

## Order Status

Order status values: `placed`, `approved`, `delivered`, or null. There is no enforced state machine — status can be set freely.

## Retrieving & Cancelling

- An order can be retrieved by its UUID.
- Deleting an order permanently removes it. There is no soft-delete or cancellation concept.
- Attempting to retrieve or delete a non-existent order returns `404`.
