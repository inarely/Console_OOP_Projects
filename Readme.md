## 1) Library Management Console Application (C#)

## Description

This project is a console-based Library Management System written in C#.  
It allows users to manage a collection of books with basic functionality such as adding, removing, saving, and loading data from a file.

The application demonstrates fundamental Object-Oriented Programming (OOP) principles, working with collections, file input/output, and user interaction through the console.

## Features

- Add new books to the library
- Display all stored books
- Remove books by index
- Set and track the current book
- Mark books as read or unread
- Save data to a text file
- Load data from a text file
- Automatic loading on startup
- Automatic saving on exit

## Project Structure

The project consists of the following main components:

### Book Class
Represents a book entity with:
- Title
- Author
- Year of publication
- Read status

Includes validation and formatting logic.

### Library Class
Manages a collection of books using `List<Book>` and provides methods to:
- Add books
- Remove books
- Access books by index
- Display all books

### File Handling
Data is stored in a `.txt` file using a custom format:

"Title|Author|Year|IsRead"

Each book is saved on a separate line.

### Program Class
Contains:
- Main menu logic
- User input handling
- File save/load methods
- Validation utilities

---

## 2) Product Catalog App

## Description

Product Catalog App is a console-based application written in C# that demonstrates the fundamentals of Object-Oriented Programming (OOP), collections, and file-based persistence using JSON.

The application allows a user to manage a simple product storage:
- add products,
- remove products,
- update product quantities,
- save and load data from a JSON file.

The project is intended as an educational example and focuses on understanding program structure, data flow, and safe work with files.

---

## Key Concepts Used

- Object-Oriented Programming (classes, encapsulation)
- `Dictionary<TKey, TValue>` for data storage
- Console-based menu navigation
- JSON serialization and deserialization (`System.Text.Json`)
- File system operations (`System.IO`)
- Input validation
- Separation of concerns (menu logic, storage logic, file handling)

---

## Project Structure

- **Product**
  - Represents a single product.
  - Stores name, price, amount, storage data, and unique identifier.
  - Uses validation to ensure correct input values.

- **Storage**
  - Manages a collection of products using `Dictionary<int, Product>`.
  - Provides methods to add, remove, increase, and decrease product quantities.
  - Exposes data as a read-only collection to protect internal state.

- **SavingInFile**
  - Responsible for saving and loading the storage to and from a JSON file.
  - Ensures safe loading even if the file does not exist or is empty.

- **Menu**
  - Provides a console interface for user interaction.
  - Handles user input and menu navigation.

- **Methods**
  - Contains helper methods for input reading, validation, and output formatting.

---

## Data Persistence

Product data is stored in a JSON file located in the `data` directory at the project root.

- Saving always **overwrites** the existing file.
- Loading replaces the current storage state with data from the file.
- The application safely handles missing or empty files.

Example file name: data/storage.json


## Requirements

- .NET (modern .NET version, e.g. .NET 8 / .NET 9 / .NET 10)
- Console environment
- No external libraries required

---

## How to Run

## Notes about downloading from GitHub correctly
If you want everything to open and build properly:
 - Prefer **git clone** instead of copying single files manually
 - Make sure you download/clone the **full repository**, not just the .sln
 - Do not delete project folders like Properties/, .csproj, etc.

## Educational Purpose

This project was created for learning purposes to practice:
- working with collections,
- understanding JSON serialization,
- managing application state,
- building structured console applications.

---

## License
This project is provided for educational purposes without any warranty. Use and modify freely.
