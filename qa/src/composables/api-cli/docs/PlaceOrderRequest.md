
# PlaceOrderRequest


## Properties

Name | Type
------------ | -------------
`petId` | string
`quantity` | number
`shipDate` | Date
`status` | string
`complete` | boolean

## Example

```typescript
import type { PlaceOrderRequest } from ''

// TODO: Update the object below with actual values
const example = {
  "petId": null,
  "quantity": null,
  "shipDate": null,
  "status": null,
  "complete": null,
} satisfies PlaceOrderRequest

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as PlaceOrderRequest
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


