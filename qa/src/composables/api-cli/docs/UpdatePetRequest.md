
# UpdatePetRequest


## Properties

Name | Type
------------ | -------------
`id` | string
`name` | string
`status` | string
`categoryId` | string
`photoUrls` | Array&lt;string&gt;
`tagIds` | Array&lt;string&gt;

## Example

```typescript
import type { UpdatePetRequest } from ''

// TODO: Update the object below with actual values
const example = {
  "id": null,
  "name": null,
  "status": null,
  "categoryId": null,
  "photoUrls": null,
  "tagIds": null,
} satisfies UpdatePetRequest

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as UpdatePetRequest
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


