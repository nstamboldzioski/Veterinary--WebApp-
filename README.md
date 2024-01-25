                                                Veterinary WebApp

This is the final project from the Full Stack .Net Academy. We are going to build a .NET MVC Web Application for a Veterinary Client which will allow for the tracking of Pets and Owners and Vaccines. 
As part of this project, we will have an implementation of the following concepts.
-	Caching mechanism on a controller level.
-	Modeling and creating a DB with Entity Framework Code First Database
-	Scaffolding CRUD functionalities with Views
-	Authentication and Authorization with individual Accounts and roles
-	Drawing a database diagram model
-	Following clean code, intuitive naming and separation of concerns practices
-	Adding a Unit Test Project and writing unit tests for model logic.
-	Following of the 4 basic OOP Principles
These are the requirements for the MVC Web App project.
(Client Note)
I want an application that will help me track which pet belongs to which owner and what vaccines the pet has taken.
For Each Owner I would like to have the following data: Name, Surname, Age and for each Pet I would like to have Name and Age, Vaccines he had taken and Owner. For Each Vaccine I only want to have the name.
I want my owner age limit to be above 18 - 100 years as well. I want my pet age to be from 0 to 50 years.
Additionally, I would like to link the Owners and Pets, one owner should be able to have more Pets, and one Pet should have one Owner. And relation the Pets and Vaccines in a way that one Pet can have multiple Vaccines and one vaccine can have multiple Pets.
I would like to be able to Create, Read, Update and Delete an Owner, Pet and a Vaccine. Also, when I Create and Edit a Pet, I want to be able to link it to an Owner and link it to a Vaccine.
I should be able to see the Pets for a specific owner from the Owner’s details view and see the Vaccines for a Specific Pet from the Pet’s details view.
In the index View of the Pet Table I should be able to see the name and surname for the owner in the owner column. Also I should be able to see the full name of the owner in the Details and Delete View of the Pet.
The Pet is the central entity in this project so I should also be able to add one owner and multiple vaccines when I create a new Pet, and I should also be allowed to edit the vaccines and owner of a pet on the edit view.
I would also want to have Authentication and Authorization for my Web App, I would like to have individual user accounts and two Roles for my System, an Admin and a Regular User.
I don’t want my Regular Users to be able to Delete and Edit Owners Pets and Vaccines, I only want them to Create and View their Details.
I want my application to also be fast and precise. (Caching and Testing)

(Dev Note)
We can split this project in a few parts:
-	Creating Authorization and Authentication with individual accounts and roles.
-	Database Diagram creation, model creation and Creating the DB with Entity Framework Core
-	Scaffolding the Views for the Models
-	Implementing caching mechanisms on the index endpoints for the entities with absolute and sliding expiration, also cache key removal when updating entities.
-	Creating a Test project and writing Unit tests

Note: Owner model should contain GetFullName() function which will return name + surname to be used in the Pet index, Details and Delete View.

