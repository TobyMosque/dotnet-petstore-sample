
# PetDto


## Properties

Name | Type
------------ | -------------
`id` | string
`name` | string
`status` | string
`category` | [CategoryDto](CategoryDto.md)
`photoUrls` | Array&lt;string&gt;
`tags` | [Array&lt;TagDto&gt;](TagDto.md)

## Example

```typescript
import type { PetDto } from ''

// TODO: Update the object below with actual values
const example = {
  "id": null,
  "name": null,
  "status": null,
  "category": null,
  "photoUrls": null,
  "tags": null,
} satisfies PetDto

console.log(example)

// Convert the instance to a JSON string
const exampleJSON: string = JSON.stringify(example)
console.log(exampleJSON)

// Parse the JSON string back to an object
const exampleParsed = JSON.parse(exampleJSON) as PetDto
console.log(exampleParsed)
```

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


