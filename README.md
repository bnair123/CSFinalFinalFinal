# Stock Viewer Application

This application allows users to view and manage their stock portfolio.

## Project Overview

This application uses WPF and .NET which allows for individual usar creation for whom you can save stocks to their profile.

The application has the following main components:

- `User`: Represents a user of the application.
- `Stock`: Represents a stock in the user's portfolio.
- `Controller`: Handles user login and stock details retrieval.
- `UserManager`: Manages user creation and login.
- `StockSymbolManager`: Fetches stock symbols.
- `StockPriceManager`: Fetches real-time stock prices.
- `StockDictionary`: Manages the user's stock portfolio.
- `MainWindow`: Represents the login screen of the application.
- `StockView`: Represents the main screen of the application where the user can view and manage their stock portfolio.

## Running the Application

To run the application just run it in VS should not be too hard.

## How It Works

1. The user is presented with a login screen where they can enter their username and password.
2. If the user enters valid credentials, the login screen is hidden and the main screen of the application (`StockView`) is shown.
3. On the main screen, the user can see their current stock portfolio, add new stocks to their portfolio, and remove stocks from their portfolio.
4. The application fetches real-time stock prices using an API.



