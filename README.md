# _Pierre's Sweet and Savory Treats_
### An independent project for Epicodus

#### _A program that will help Pierre organize his goodies._

#### By Brent Hubbard

## Technologies Used

* _C#_
* _.NET 5.0.102_
* _ASP.NET Core Mvc 4.8_
* _ASP.NET Identity_
* _MySQL_
* _Razor_
* _Entity Framework Core_
* _Git_
*_Identity_
## Description

_This application creates a database to hold treats and flavors with a many-to-many relationship. One can register for an account and log in to create, update, or delete flavors. All users including guests can see all of Pierre's treats and their corresponding flavors._

## Setup/Installation Requirements

* You can clone the repository to your desktop from https://github.com/Brenthubbard/PierreTreats.Solution.git
* Navigate to the PierreTreats.Solution directory
* Add a file called appsettings.json. In that file, you will need to add the following code:
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=brent_hubbard;uid=root;pwd=_password_;"
  }
}
* (The 'password' is only necessary if you have a password for your MySQL)
* Save
### Setting up the database
#### Using SQL
1. Open a SQL command line in a terminal. Copy and paste the contents of brent_hubbard.sql and hit enter.
2. Quit out of SQL with Ctrl + c
#### OR Using .NET
1. In the terminal, navigate to the PierreTreats.Solution directory
2. Enter "dotnet ef database update"
### Running the program
* From the Treats directory, enter "dotnet restore"
* Next, enter "dotnet build"
* Enter "dotnet run" in the terminal and hit enter to start a local host. 
* Ctrl + click the link that populates in the terminal to view the application in the webpage. It is probably http://localhost:5000/
* Use Ctrl + C in the terminal to quit the host and close out of the window in the browser.

## Known Bugs


## License

[MIT](https://opensource.org/licenses/MIT)

Copyright © 2021 Brent Hubbard

All Rights Reserved

## Contact Information
Hubbardbrent@hotmail.com