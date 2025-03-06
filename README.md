
This is a C#-based console application for managing contacts and related country information. The application follows a modular architecture, allowing for easy management and scalability. Below is an overview of the architecture and components:

Architecture Overview
The architecture is based on a simple and efficient layered design, with clear separation of concerns:

Presentation Layer (UI):

This layer is responsible for interacting with the user.
It provides a console interface for performing CRUD operations (Create, Read, Update, Delete) on contacts and country data.
The user inputs commands, and the application calls the relevant methods to interact with the business logic layer.
Business Logic Layer (BLL):

This layer contains the core business logic, such as creating, updating, deleting, and finding contacts.
It interacts with the data access layer to fetch and save data, handling the validation and transformations needed for these operations.
This separation ensures that the logic for manipulating data is independent of user interactions.
Data Access Layer (DAL):

The data access layer is responsible for interacting with the database or storage system.
It includes methods for accessing and modifying contact and country data.
Data retrieval is done via DataTable and operations are abstracted to simplify database interactions.
Models:

Contact and Country models are used to represent the main entities in the system.
These models contain properties like FirstName, LastName, Phone, Email, and others, and are used throughout the application for data manipulation.
Data Flow
User Interaction:

The user interacts with the console interface to select actions such as adding, updating, or deleting contacts or countries.
Business Logic Execution:

The input is passed to the business logic layer, which validates the input and performs the necessary operations by calling methods from the data access layer.
Data Persistence:

The data access layer handles saving and retrieving data from the database or storage, ensuring that the changes are persisted for future use.
