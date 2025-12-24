# Shopping App — Project Documentation

_This document was created using your provided assignment PDF as a reference and adapted into a full project documentation set for the Shopping App (Clean Architecture + React frontend)._ 

---

## 1. Overview
This Shopping App is a full-stack assignment built with a Clean Architecture backend (.NET) and React frontend. The app supports product browsing, product detail pages, user authentication, a persistent cart, checkout (orders), and admin CRUD for products. The documentation matches the style and coverage of the reference PDF but is tailored to the project scaffold in this repo.

---

## 2. High-level architecture
- **Frontend**: React (Vite), React Router, Redux Toolkit, Axios, Tailwind (optional)
- **Backend**: ASP.NET Web API (.NET 7/8) using Clean Architecture projects:
  - `ShoppingApp.Domain` — entities, enums, exceptions
  - `ShoppingApp.Application` — interfaces, DTOs, services, validators
  - `ShoppingApp.Infrastructure` — EF Core DbContext, repositories, external services
  - `ShoppingApp.Api` — controllers, middleware, startup
- **Database**: PostgreSQL (production) / SQL Server or SQLite for dev
- **Auth**: JWT-based auth (Auth0 optional) with refresh tokens
- **Testing**: xUnit for backend, Jest + React Testing Library for frontend
![useronboardingflow_nshppoingcartapp](https://github.com/user-attachments/assets/e7169603-88f4-401e-9523-d36c8b4722e4)
- **Product Browsing and Selection Diagram**
![prooductbrowsingandselection](https://github.com/user-attachments/assets/6fe33fcf-2c68-4ac1-932b-87d6034e9150)

---

## 3. Project structure & file list
The projects and key files are listed below (grouped by project). Each file includes a short purpose.

### `ShoppingApp.Domain`
- `Entities/User.cs` — user entity
- `Entities/Product.cs`
- `Entities/Cart.cs` — cart container (UserId, Items)
- `Entities/CartItem.cs`
- `Entities/Order.cs`
- `Entities/OrderItem.cs`
- `Enums/OrderStatus.cs`
- `ValueObjects/Money.cs` (optional)
- `Exceptions/DomainException.cs`

### `ShoppingApp.Application`
- `Interfaces/IProductRepository.cs`
- `Interfaces/IUserRepository.cs`
- `Interfaces/ICartRepository.cs`
- `Interfaces/IOrderRepository.cs`
- `Services/ProductService.cs` — business logic for products
- `Services/CartService.cs` — orchestrates add/remove/update items
- `Services/OrderService.cs` — checkout flow
- `DTOs/*` — DTOs for input/output (ProductDto, CreateOrderRequest, Auth models)
- `Validators/*` — FluentValidation classes
- `Mappers/` — AutoMapper profiles (or manual mapping)

### `ShoppingApp.Infrastructure`
- `Persistence/AppDbContext.cs`
- `Configurations/ProductConfiguration.cs`
- `Repositories/ProductRepository.cs`
- `Repositories/CartRepository.cs`
- `Repositories/OrderRepository.cs`
- `Auth/JwtTokenService.cs`
- `Seed/DbSeeder.cs`
- `Extensions/ServiceCollectionExtensions.cs`

### `ShoppingApp.Api`
- `Program.cs` / `Startup.cs`
- `Controllers/AuthController.cs`
- `Controllers/ProductsController.cs`
- `Controllers/CartController.cs`
- `Controllers/OrdersController.cs`
- `Middleware/ExceptionMiddleware.cs`
- `Helpers/CurrentUser.cs`
- `appsettings.json`

### Frontend `frontend/`
- `src/main.tsx`, `src/App.tsx`
- `src/api/apiClient.ts` — axios instance
- `src/features/products/productsSlice.ts` and components
- `src/features/cart/cartSlice.ts` and components
- `src/features/auth/authSlice.ts` and pages
- `src/components/Header.tsx`, `Footer.tsx`, `Spinner.tsx`

---

## 4. Database (ER diagram & schema)

### Entities & relationships (summary)
- **User** (1) — (M) **Cart** (or 1 cart per user)
- **Cart** (1) — (M) **CartItem**
- **Product** (1) — (M) **CartItem**
- **User** (1) — (M) **Order**
- **Order** (1) — (M) **OrderItem**
- **Product** (1) — (M) **OrderItem**

---

## 5. API design (endpoints, request/response shapes)

### Auth
- `POST /api/auth/register` — `{ email, password, name }` → `201 Created` with minimal user DTO
- `POST /api/auth/login` — `{ email, password }` → `{ accessToken, refreshToken, expiresIn, user }`
- `POST /api/auth/refresh` — `{ refreshToken }` → new tokens

### Products
- `GET /api/products` — query: `page`, `size`, `search`, `minPrice`, `maxPrice` → paged result
- `GET /api/products/{id}` → product details
- `POST /api/products` — admin only, create product
- `PUT /api/products/{id}` — admin only, update product
- `DELETE /api/products/{id}` — admin only

### Cart
- `GET /api/cart` — returns current user's cart with items
- `POST /api/cart/items` — `{ productId, quantity }` → adds or updates item
- `PUT /api/cart/items/{cartItemId}` — `{ quantity }` → update
- `DELETE /api/cart/items/{cartItemId}` — remove item
- `POST /api/cart/clear` — clear cart

### Orders
- `POST /api/orders` — create order (checkout) — validates stock, creates order, clears cart
- `GET /api/orders` — current user's orders (admin can view all)
- `GET /api/orders/{id}` — order details

---

## 6. Repository interfaces (methods summary)
This project follows the interface set created previously. Example: `ICartRepository` — methods include `GetCartByUserIdAsync`, `GetOrCreateCartByUserIdAsync`, `AddItemToCartAsync`, `UpdateCartItemAsync`, `DeleteCartItemAsync`, `ClearCartAsync`, etc.

(Full interface files are part of the repo under `ShoppingApp.Application/Interfaces`.)

---

## 7. Authentication & security
- JWT access tokens (short-lived) + refresh tokens (longer)
- Passwords hashed (BCrypt/Identity)
- All APIs served over HTTPS in production
- Role-based authorization: `Admin` vs `User` claims
- Secrets in environment variables or secret manager (not in source)

---

## 8. Frontend structure & important components
- **Header**: shows links + cart count + login status
- **ProductList**: paginated list, server-side paging
- **ProductDetail**: product info + Add to Cart
- **CartPage**: list items, quantity updater, checkout
- **Checkout**: order summary + place order
- **Admin ProductForm**: create/edit products

State management: Redux Toolkit slices for `auth`, `products`, `cart`, `orders`.

---

## 9. Development setup (local) — commands & env

### Prereqs
- .NET SDK 7/8
- Node 18+
- PostgreSQL (or Docker)
- dotnet-ef tool (`dotnet tool install --global dotnet-ef`)

### Backend (run locally)
```bash
# from repo root
cd src/ShoppingApp.Api
# configure connection string in appsettings.Development.json or env var
dotnet ef database update --project ../ShoppingApp.Infrastructure/ --startup-project .
dotnet run
```

### Frontend (run locally)
```bash
cd frontend
npm install
npm run dev
# open http://localhost:5173 (or port reported by Vite)
```

### Environment variables (example `.env`)
```
# Backend
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=Host=localhost;Database=shop;Username=postgres;Password=postgres
Jwt__Issuer=ShoppingApp
Jwt__Audience=ShoppingAppClients
Jwt__Secret=YOUR_SECRET_HERE
Jwt__ExpiresInMinutes=15
Jwt__RefreshTokenExpiresDays=30

# Frontend (Vite)
VITE_API_URL=http://localhost:5000/api
```

---

## 10. Testing strategy
- **Unit tests**: xUnit for Application services; mock repositories with Moq
- **Integration tests**: `WebApplicationFactory<T>` + `UseInMemoryDatabase` for controller tests
- **Frontend tests**: Jest + React Testing Library for key pages (ProductList, Cart)
- **E2E** (optional): Cypress for checkout flow

Run backend tests:
```bash
dotnet test ./tests/ShoppingApp.Tests
```

Run frontend tests:
```bash
cd frontend
npm test
```

---

## 11. Demo script & deliverables
**Demo script** (5–7 minutes):
1. Show architecture and ER diagram (30s)
2. Register + Login as user (30s)
3. Browse products, open product detail (30s)
4. Add items to cart and open Cart page (60s)
5. Checkout to create order (60s)
6. Log in as Admin and create a product (30s)
7. Show code snippet: `CartService.AddItemToCartAsync` and repository implementation (60s)

**Deliverables:** Source code on GitHub, README, ER diagram, API docs (Swagger), unit tests, demo slides/video

---

## 12. Timeline & task breakdown (sample 1-week sprint)
- Day 1: Scaffold backend projects, domain, initial migration
- Day 2: Products API + frontend product list
- Day 3: Cart backend + frontend cart UI
- Day 4: Auth + protect endpoints
- Day 5: Orders / checkout
- Day 6: Tests & polish
- Day 7: Demo prep

---

### Final notes
- This doc mirrors the structure and detail level of your reference PDF but is specific to the project scaffold we discussed earlier. Use this as the canonical project documentation, commit it to the `docs/` folder, and keep it up to date with architectural decisions and API changes.



