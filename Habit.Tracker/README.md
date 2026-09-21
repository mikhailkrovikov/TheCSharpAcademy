# Habit tracker

Console based CRUD application to track your habit: date anf time and count of fency exercises.
Developed using C# and SQLite.


# Given Requirements:
- When the application starts, it should create a sqlite database, if one isn’t present.
- It should also create a table in the database, where the hours will be logged.
- You need to be able to insert, delete, update and view your records of habbit. 
- You should handle all possible errors so that the application never crashes 
- The application should only be terminated when the user enter E. 
- You can only interact with the database using raw SQL. You can’t use mappers such as Entity Framework

# Features

* SQLite database connection

	- The program uses a SQLite db connection to store and read information. 
	- If no database exists, or the correct table does not exist they will be created on program start.
 	
* CRUD DB functions
	- "C" - creating a new record with date/time entered in mm-DD-yyyy format and count
	- "R" - reading all data
	- "U" - update a record by Id, changing a time and count
	- "D" - delete record by Id

# Challenges
	
- SQLite by ADO.NET gives me a more experience with SQL. Its not LINQ-like EF Core and needs SQL knoledges. 
- Refactoring code. Good practise for me to follow DRY, SRP princips. Also add delegate-like architecture in DB module (I love delegates so much)
	
# Areas to Improve

- Refactoring is endlees process, but in this project I hope reviwers will point my troubles.
- Im not good enough in UI, so console and UX could be better
