# My test project for ShuttleX:rocket:
___

### :eyes: Below you can see endpoints for communicate with api/ws

- [Chat Application Api](#chat-application-api)
  - [Chat Endpoints](#chat-endpoints)
    - [Get Chats](#get-chats)
    - [Get Chat By Id](#get-chat-by-id)
    - [Get Users By Chat](#get-users-by-chat)
    - [Create Chat](#create-chat)
    - [Send Message](#send-message)
    - [Update Chat](#update-chat)
    - [Delete Chat](#delete-chat)
  - [User Endpoints](#user-endpoints)
    - [Get User By Id](#get-user-by-id)
    - [Create User](#create-user)
 - [Chat Application Web Soket](#chat-application-web-soket)
   - [Join chat](#join-chat)
___

### How works with chat application

  1. [Create user](#create-chat)
  2. [Create chat](#create-chat) or [Search](#get-chats) for join
  3. [Send](#send-message) and Receive message
___

## Chat Endpoints

### Get Chats

```js
    GET {{host}}/api/chats
```

#### Get Chats Reponse

```js
    200 OK
```

```js
    {
        "chatId": "9d03975c-9d56-413f-9350-f96c46b73550",
        "name": "Chat"
    },
    {
        "chatId": "4d03975c-9d56-413f-9350-f96c46b73554",
        "name": "Team"
    }
```

### Get Chats with Search

```js
    GET {{host}}/api/chats?search=Team
```

#### Get Chats with Search Reponse

```js
    200 OK
```

```js
    {
        "chatId": "4d03975c-9d56-413f-9350-f96c46b73554",
        "name": "Team"
    }
```
___

### Get Chat by Id

```js
    GET {{host}}/api/chats/9d03975c-9d56-413f-9350-f96c46b73550
```

#### Get Chat by Id Reponse

```js
    200 OK
```

```js
    {
        "chatId": "4d03975c-9d56-413f-9350-f96c46b73554",
        "name": "Team"
    }
```
___

### Get Users by Chat

```js
    GET {{host}}/api/chats/2c9a2647-2d12-473c-96cc-e431ad6063a3/users
```

#### Get Users by Chat Reponse

```js
    200 OK
```

```js
    {
        "userId": "5d970d85-6584-464f-ad57-9e15d6fe7ecb",
        "name": "Bob"
    },
    {
        "userId": "7w454c42-6584-464r-ad57-9e15d6fe7ecb",
        "name": "Alex"
    }
```
___

### Create Chat

```js
    POST {{host}}/api/chats
```

#### Create Chat Request

```js
    {
        "userId": "5d970d85-6584-464f-ad57-9e15d6fe7ecb",
        "name": "Programmers"
    }
```

#### Create Chat Response

```js
    201 Created
```

```js
    {
        "chatId": "9d03975c-9d56-413f-9350-f96c46b73550",
        "name": "Programmers"
    }
```
___

### Send Message

```js
    POST {{host}}/api/chats/9d03975c-9d56-413f-9350-f96c46b73550/message
```

#### Send Message Request

```js
    {
        "userId": "c8cb2ada-59f7-4cef-8c9d-2c43e3675f82",
        "message": "Hello, I am Bob"
    }
```

#### Send Message Response

```js
    200 OK
```
___

### Update Chat

```js
    PUT {{host}}/api/chats/9d03975c-9d56-413f-9350-f96c46b73550
```

#### Update Chat Request

```js
    {
        "userId": "c8cb2ada-59f7-4cef-8c9d-2c43e3675f82",
        "name": "Team of Programmers"
    }
```

#### Update Chat Response

```js
    200 OK
```
___

### Delete Chat

```js
    DELETE {{host}}/api/chats/9d03975c-9d56-413f-9350-f96c46b73550
```

#### Delete Chat Response

```js
    204 No Content
```
___

## User Endpoints

### Get User by Id

```js
    GET {{host}}/api/users/c8cb2ada-59f7-4cef-8c9d-2c43e3675f82
```

#### Get User by Id Reponse

```js
    200 OK
```

```js
    {
        "userId": "c8cb2ada-59f7-4cef-8c9d-2c43e3675f82",
        "name": "Bob"
    }
```
___

### Create User

```js
    POST {{host}}/api/users
```

#### Create User Request

```js
    {
        "name": "Jame"
    }
```

#### Create User Response

```js
    201 Created
```

```js
    {
        "userId": "c8cb2ada-59f7-4cef-8c9d-2c43e3675f82",
        "name": "Jame"
    }
```
___

## Chat Application Web Soket

### Join Chat

```js
    wss://{{host}}/chat
```

#### Join Chat Message

```js
    {
        "arguments": [
                "5d970d85-6584-464f-ad57-9e15d6fe7ecb","6081bea4-b8ee-4726-a08a-e92d620cb578"
            ],
        "target": "JoinChat",
        "type": 1
    }
```
