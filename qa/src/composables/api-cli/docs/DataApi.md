# DataApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**dataExportGet**](DataApi.md#dataexportget) | **GET** /data/export |  |
| [**dataGeneratePost**](DataApi.md#datageneratepost) | **POST** /data/generate |  |
| [**dataImportPost**](DataApi.md#dataimportpost) | **POST** /data/import |  |
| [**dataSeedPost**](DataApi.md#dataseedpost) | **POST** /data/seed |  |



## dataExportGet

> dataExportGet()



### Example

```ts
import {
  Configuration,
  DataApi,
} from '';
import type { DataExportGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DataApi();

  try {
    const data = await api.dataExportGet();
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters

This endpoint does not need any parameter.

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


## dataGeneratePost

> dataGeneratePost()



### Example

```ts
import {
  Configuration,
  DataApi,
} from '';
import type { DataGeneratePostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DataApi();

  try {
    const data = await api.dataGeneratePost();
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters

This endpoint does not need any parameter.

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


## dataImportPost

> dataImportPost(file)



### Example

```ts
import {
  Configuration,
  DataApi,
} from '';
import type { DataImportPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DataApi();

  const body = {
    // Blob (optional)
    file: BINARY_DATA_HERE,
  } satisfies DataImportPostRequest;

  try {
    const data = await api.dataImportPost(body);
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
| **file** | `Blob` |  | [Optional] [Defaults to `undefined`] |

### Return type

`void` (Empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: `multipart/form-data`
- **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## dataSeedPost

> dataSeedPost()



### Example

```ts
import {
  Configuration,
  DataApi,
} from '';
import type { DataSeedPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new DataApi();

  try {
    const data = await api.dataSeedPost();
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters

This endpoint does not need any parameter.

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

