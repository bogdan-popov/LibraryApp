# Backend для библиотеки на .NET

Это backend-сервис для управления пользователями, книгами и абонементами в библиотеке.

## Технологический стек

*   **.NET 9**
*   **ASP.NET Core Web API**
*   **Entity Framework Core 9**
*   **PostgreSQL**
*   **Архитектура:** 3-слойная (API -> Business Logic -> Data Access)
*   **Паттерны:** Repository, Unit of Work

## Краткая документация по API

### Users (Пользователи)

*   **`GET /api/users`**
    *   Получает список всех пользователей.

*   **`GET /api/users/{id}`**
    *   Получает детальную информацию о пользователе по его `id`, включая список взятых книг.

*   **`POST /api/users`**
    *   Создает нового пользователя.
    *   Тело запроса: `{ "firstName": "string", "lastName": "string" }`

*   **`PUT /api/users/{id}`**
    *   Обновляет имя и фамилию пользователя.
    *   Тело запроса: `{ "firstName": "string", "lastName": "string" }`

*   **`DELETE /api/users/{id}`**
    *   Удаляет пользователя.

*   **`POST /api/users/{id}/subscription`**
    *   Оформляет пользователю абонемент на 30 дней.

### Books (Книги)

*   **`POST /api/books`**
    *   Добавляет новую книгу в библиотеку.
    *   Тело запроса: `{ "title": "string", "author": "string" }`

*   **`POST /api/books/{bookId}/borrow/{userId}`**
    *   Выдает книгу (`bookId`) пользователю (`userId`).

*   **`POST /api/books/{bookId}/return`**
    *   Возвращает книгу (`bookId`) в библиотеку.