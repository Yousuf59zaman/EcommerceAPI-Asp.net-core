# EcommerceAPI

## Overview
`EcommerceAPI` is a robust backend system for managing an e-commerce platform using ASP.NET Core and Entity Framework Core with SQL Server. It provides endpoints for user authentication, product management, and order processing. Below is a detailed guide to the API functionalities for both users and admins.

## Features

### User Functionalities

#### Registration
- **Endpoint**: `POST /api/users/register`
- **Description**: Allows new users to register by providing username, email, and password.

#### Login
- **Endpoint**: `POST /api/users/login`
- **Description**: Authenticates users by their email and password.

#### View User Profile
- **Endpoint**: `GET /api/users/{userId}`
- **Description**: Retrieves the profile information of a logged-in user.

#### Update Profile
- **Endpoint**: `PUT /api/users/{userId}`
- **Description**: Allows users to update their profile information after logging in.

#### View Products
- **Endpoint**: `GET /api/products`
- **Description**: Displays all available products.

#### View Product Details
- **Endpoint**: `GET /api/products/{productId}`
- **Description**: Provides detailed information about a specific product.

#### Add to Cart
- **Endpoint**: `POST /api/cart`
- **Description**: Allows users to add products to their cart.

#### Place Order
- **Endpoint**: `POST /api/orders/placeorder`
- **Description**: Enables users to place an order. The order along with the order details are saved in one transaction.

#### View All Orders
- **Endpoint**: `GET /api/orders/user/{userId}`
- **Description**: Allows users to view all their orders.

### Admin Functionalities

#### View Products
- **Endpoint**: `GET /api/products`
- **Description**: Allows the admin to view all listed products.

#### Add Product
- **Endpoint**: `POST /api/products`
- **Description**: Enables the admin to add new products to the store.

#### Update Product
- **Endpoint**: `PUT /api/products/{productId}`
- **Description**: Allows the admin to update details of an existing product.

#### Delete Product
- **Endpoint**: `DELETE /api/products/{productId}`
- **Description**: Enables the admin to remove a product from the store.

#### Manage Orders
- **Endpoint**: `PUT /api/orders/updatestatus/{orderId}`
- **Description**: Allows the admin to update the status of an order (e.g., from "Pending" to "Completed").

#### View Users
- **Endpoint**: `GET /api/users`
- **Description**: Allows the admin to view all registered users.

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- SQL Server Management Studio (SSMS)

## Setup and Installation
1. Clone the repository.
2. Ensure you have [.NET 5.0 SDK](https://dotnet.microsoft.com/download) and [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) installed.
3. Update the `appsettings.json` with your SQL Server connection string.
4. Run `dotnet ef database update` to create and seed the database.
5. Start the application with `dotnet run`.

## Contributions
Contributions are welcome. Please fork the repository and submit a pull request with your updates.

