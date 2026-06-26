# Acceptance Criteria

This section describes the expected behaviour of the PetStore API from the perspective of each user role. Each flow defines what a user wants to accomplish, the steps involved, and the outcomes the system must produce.

These criteria are the source of truth for verifying that the API works correctly. Any automated test suite should trace its assertions back to at least one of the flows described here.

## Roles

| Role | Description |
|------|-------------|
| **Store Operator** | Manages the pet catalogue and fulfils orders |
| **Customer** | Browses pets, places orders |
| **Registered User** | Has an account; can log in and manage their profile |

## Sections

- [Pet Management](./pet-management) — adding, finding, updating, and removing pets
- [Store & Orders](./store) — inventory, placing orders, tracking and cancelling
- [User Management](./users) — registration, login, profile management

## Testing Notes

- The API is already populated with data when the environment starts. See the [Getting Started](/guide/getting-started) page for dataset details.
- All IDs in request paths must be valid UUIDs. Passing an unknown UUID to a resource endpoint must return `404`.
- Field values documented as `null` in the data model must be returned as JSON `null`, never omitted from the response body.
- The API does not paginate results. All matching records are returned in a single response.
