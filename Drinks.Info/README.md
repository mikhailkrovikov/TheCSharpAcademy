# Drinks.Info

A C# console app for browsing drinks from TheCocktailDB, with menus and tables styled using Spectre.Console.

## Features

- Browse categories and drinks.
- View ingredients, measurements, instructions, and images.
- Add and remove favourite drinks, saved in `favourites.json`.
- Navigate back between menus without restarting the app.

## Run

Requires .NET 10 SDK and an internet connection. Run from the project folder:

```bash
dotnet run
```

Use the arrow keys to navigate and Enter to select.

## Challenges

Displaying images clearly in the console was challenging: smaller images lost detail and quality, while larger ones took up too much space. The app uses smaller image versions and limits their display width.
