# AlphaTravel
Alpha travel and tourism project showcases CQRS and clean architecture of RESTful Web Api.

### Version

#### Swagger UI
![](Swagger%20UI%20-%20Customer%20Web%20Api.PNG)

##### GET all customers
###### Request Url
` https://localhost:44351/api/v1/customers `
###### Response Body
```javascript
{
  "href": "https://localhost:44351/api/v1/customers",
  "method": "GET",
  "rel": [
    "collection"
  ],
  "offset": 0,
  "limit": 25,
  "total": 5,
  "first": {
    "href": "https://localhost:44351/api/v1/customers",
    "method": "GET",
    "rel": [
      "collection"
    ]
  },
  "data": [
    {
      "href": null,
      "method": "GET",
      "id": 1,
      "firstname": "John",
      "surname": "Richard",
      "password": "password123",
      "email": "test@test.com",
      "created_on": "2019-03-18T12:49:44Z",
      "created_by": "SeedDataService",
      "modified_on": "0001-01-01T00:00:00Z"
    },.......
```
![](Get%20All%20Customers.PNG)

##### GET all customers with pagging, offset and limit

###### Request Url
` https://localhost:44351/api/v1/customers?Offset=0&Limit=50 `
###### Response Body
```javascript
{
  "href": "https://localhost:44351/api/v1/customers",
  "method": "GET",
  "rel": [
    "collection"
  ],
  "offset": 0,
  "limit": 50,
  "total": 5,
  "first": {
    "href": "https://localhost:44351/api/v1/customers",
    "method": "GET",
    "rel": [
      "collection"
    ]
  },
  "data": [
    {
      "href": null,
      "method": "GET",
      "id": 1,
      "firstname": "John",
      "surname": "Richard",
      "password": "password123",
      "email": "test@test.com",
      "created_on": "2019-03-18T12:49:44Z",
      "created_by": "SeedDataService",
      "modified_on": "0001-01-01T00:00:00Z"
    },......
 ```
 
![](Get%20All%20Customers%20with%20pagging%202.PNG)

##### GET a single customer

##### POST a customer
![](Post%20Customer.png)

##### DELETE a customer
![](Delete%20Customer.png)

##### PUT a customer
![](Put%20Customer.png)
