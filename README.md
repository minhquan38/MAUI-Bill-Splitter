# TripBudgeting

**TripBudgeting** is a mobile application built with .NET MAUI, designed to help users manage and track their travel expenses. The application allows users to set an initial budget, allocate funds to different categories (such as Transportation, Food and Drink, Accommodation, etc.), and keep track of remaining funds as expenses are logged.

## Key Features

- **Expense Management:**  
  Users can create, update, and delete expenses for each trip. The application tracks how much money has been spent in different categories and shows the remaining budget.

- **Trip Overview:**  
  A detailed overview of each trip, including the destination, start and end dates, and duration.

- **Image Support:**  
  Users can upload an image related to their trip, which is displayed prominently at the top of the trip details page.

- **Data Persistence:**  
  All data is stored locally using SQLite, ensuring that trip and expense information is retained between sessions.

## Technologies Used

- **.NET MAUI:**  
  The primary framework used to build the cross-platform mobile application.

- **Entity Framework Core:**  
  Used for data access and management, providing an abstraction layer over SQLite for easy database interactions.

- **SQLite:**  
  A lightweight, file-based database used for storing trip and expense data locally on the device.

- **Data Binding:**  
  Utilized for syncing the UI with the underlying data models, ensuring that changes in the data are reflected in the UI in real time.

- **CommunityToolkit.Mvvm:**  
  Used to implement the MVVM pattern, making it easier to manage the separation of concerns between the UI and the business logic.

## Small demo
  

https://github.com/user-attachments/assets/28398369-f3d4-4df3-a2a4-191cacca29b0



## What I Learned

Throughout the development of **TripBudgeting**, I gained valuable experience in:

- **Cross-Platform Development:**  
  Learning how to develop a single codebase that runs seamlessly on multiple platforms (iOS, Android).

- **MVVM Pattern:**  
  Implementing the Model-View-ViewModel pattern to separate concerns and create a more maintainable codebase.

- **Entity Framework Core with SQLite:**  
  Mastering database interactions using Entity Framework Core, including CRUD operations, relationships, and data migrations.

- **Data Binding:**  
  Understanding and applying data binding techniques to ensure that the UI remains in sync with the data models.

- **Debugging and Error Handling:**  
  Identifying and resolving common issues such as tracking multiple instances of the same entity in Entity Framework Core.

## Next Version

In the next version of **TripBudgeting**, I plan to introduce the following features and improvements:

- **Cloud Synchronization:**  
  Implementing cloud-based storage and synchronization, allowing users to back up their trips and expenses and access them across multiple devices.

- **User Authentication:**  
  Adding user accounts and authentication to provide personalized experiences and secure data access.

- **Enhanced UI/UX:**  
  Improving the user interface and user experience with more polished design elements, animations, and a more intuitive layout.

- **Advanced Analytics:**  
  Introducing features like expense forecasting and trend analysis to help users better manage their budgets.
## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

Make sure you have the following installed on your local machine:
- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui)
- [SQLite](Nuget)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Installation

1. **Clone the repository:**

   Open your terminal and run the following command:

   ```sh
   git clone https://github.com/minhquan38/MAUI-Bill-Splitter.git
