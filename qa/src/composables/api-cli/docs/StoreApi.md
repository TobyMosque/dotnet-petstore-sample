# StoreApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**storeInventoryGet**](StoreApi.md#storeinventoryget) | **GET** /store/inventory |  |
| [**storeOrderGet**](StoreApi.md#storeorderget) | **GET** /store/order |  |
| [**storeOrderOrderIdDelete**](StoreApi.md#storeorderorderiddelete) | **DELETE** /store/order/{orderId} |  |
| [**storeOrderOrderIdGet**](StoreApi.md#storeorderorderidget) | **GET** /store/order/{orderId} |  |
| [**storeOrderPost**](StoreApi.md#storeorderpost) | **POST** /store/order |  |



## storeInventoryGet

> { [key: string]: number; } storeInventoryGet()



### Example

```ts
import {
  Configuration,
  StoreApi,
} from '';
import type { StoreInventoryGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new StoreApi();

  try {
    const data = await api.storeInventoryGet();
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

**{ [key: string]: number; }**

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


## storeOrderGet

> Array&lt;OrderDto&gt; storeOrderGet()



### Example

```ts
import {
  Configuration,
  StoreApi,
} from '';
import type { StoreOrderGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new StoreApi();

  try {
    const data = await api.storeOrderGet();
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

[**Array&lt;OrderDto&gt;**](OrderDto.md)

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


## storeOrderOrderIdDelete

> storeOrderOrderIdDelete(orderId)



### Example

```ts
import {
  Configuration,
  StoreApi,
} from '';
import type { StoreOrderOrderIdDeleteRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new StoreApi();

  const body = {
    // string
    orderId: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies StoreOrderOrderIdDeleteRequest;

  try {
    const data = await api.storeOrderOrderIdDelete(body);
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
| **orderId** | `string` |  | [Defaults to `undefined`] |

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


## storeOrderOrderIdGet

> OrderDto storeOrderOrderIdGet(orderId)



### Example

```ts
import {
  Configuration,
  StoreApi,
} from '';
import type { StoreOrderOrderIdGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new StoreApi();

  const body = {
    // string
    orderId: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies StoreOrderOrderIdGetRequest;

  try {
    const data = await api.storeOrderOrderIdGet(body);
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
| **orderId** | `string` |  | [Defaults to `undefined`] |

### Return type

[**OrderDto**](OrderDto.md)

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


## storeOrderPost

> OrderDto storeOrderPost(placeOrderRequest)



### Example

```ts
import {
  Configuration,
  StoreApi,
} from '';
import type { StoreOrderPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const api = new StoreApi();

  const body = {
    // PlaceOrderRequest (optional)
    placeOrderRequest: ...,
  } satisfies StoreOrderPostRequest;

  try {
    const data = await api.storeOrderPost(body);
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
| **placeOrderRequest** | [PlaceOrderRequest](PlaceOrderRequest.md) |  | [Optional] |

### Return type

[**OrderDto**](OrderDto.md)

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

