# PetApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**petFindByStatusGet**](PetApi.md#petfindbystatusget) | **GET** /pet/findByStatus |  |
| [**petFindByTagsGet**](PetApi.md#petfindbytagsget) | **GET** /pet/findByTags |  |
| [**petPetIdDelete**](PetApi.md#petpetiddelete) | **DELETE** /pet/{petId} |  |
| [**petPetIdGet**](PetApi.md#petpetidget) | **GET** /pet/{petId} |  |
| [**petPost**](PetApi.md#petpost) | **POST** /pet |  |
| [**petPut**](PetApi.md#petput) | **PUT** /pet |  |



## petFindByStatusGet

> Array&lt;PetDto&gt; petFindByStatusGet(status)



### Example

```ts
import {
  Configuration,
  PetApi,
} from '';
import type { PetFindByStatusGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PetApi();

  const body = {
    // string (optional)
    status: status_example,
  } satisfies PetFindByStatusGetRequest;

  try {
    const data = await api.petFindByStatusGet(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **status** | `string` |  | [Optional] [Defaults to `undefined`] |

### Return type

[**Array&lt;PetDto&gt;**](PetDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## petFindByTagsGet

> Array&lt;PetDto&gt; petFindByTagsGet(tags)



### Example

```ts
import {
  Configuration,
  PetApi,
} from '';
import type { PetFindByTagsGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PetApi();

  const body = {
    // Array<string> (optional)
    tags: ...,
  } satisfies PetFindByTagsGetRequest;

  try {
    const data = await api.petFindByTagsGet(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **tags** | `Array<string>` |  | [Optional] |

### Return type

[**Array&lt;PetDto&gt;**](PetDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## petPetIdDelete

> petPetIdDelete(petId)



### Example

```ts
import {
  Configuration,
  PetApi,
} from '';
import type { PetPetIdDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PetApi();

  const body = {
    // string
    petId: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies PetPetIdDeleteRequest;

  try {
    const data = await api.petPetIdDelete(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **petId** | `string` |  | [Defaults to `undefined`] |

### Return type

`void` (Empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## petPetIdGet

> PetDto petPetIdGet(petId)



### Example

```ts
import {
  Configuration,
  PetApi,
} from '';
import type { PetPetIdGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PetApi();

  const body = {
    // string
    petId: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies PetPetIdGetRequest;

  try {
    const data = await api.petPetIdGet(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **petId** | `string` |  | [Defaults to `undefined`] |

### Return type

[**PetDto**](PetDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## petPost

> PetDto petPost(addPetRequest)



### Example

```ts
import {
  Configuration,
  PetApi,
} from '';
import type { PetPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PetApi();

  const body = {
    // AddPetRequest (optional)
    addPetRequest: ...,
  } satisfies PetPostRequest;

  try {
    const data = await api.petPost(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **addPetRequest** | [AddPetRequest](AddPetRequest.md) |  | [Optional] |

### Return type

[**PetDto**](PetDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## petPut

> PetDto petPut(updatePetRequest)



### Example

```ts
import {
  Configuration,
  PetApi,
} from '';
import type { PetPutRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new PetApi();

  const body = {
    // UpdatePetRequest (optional)
    updatePetRequest: ...,
  } satisfies PetPutRequest;

  try {
    const data = await api.petPut(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **updatePetRequest** | [UpdatePetRequest](UpdatePetRequest.md) |  | [Optional] |

### Return type

[**PetDto**](PetDto.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

