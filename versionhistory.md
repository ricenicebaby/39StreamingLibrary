# Version history

**Version 0: 09/06/2021**

This was the base/prototype. In this version, the website could do was:
- Populate models from their corresponding table in the SQL database
  - Game model was populated by existing games in the Game table
      - Game information consists of the game name and a url for the game cover
  - Genre model was popuplated by existing genres in the Genre table
      - Genre information consists of the genre name
  - GameGenre model was populated by existing GameGenre table
      - GameGenre information connects a game and a genre
- List the genres and games
- Create a modal upon clicking a game, that then binds to data of the game and displays it

**Version 1: 09/14/2021**

This update:
- Makes a modal be closed by clicking outside of it, instead of only being able to click the Close button
- Allows a user to add a game to the SQL database

**Version 2: 09/22/2021**

This update: 
- Provides the remaining CRUD actions (update/edit and delete) to be performed. Currently anybody can do it
   - The user can edit a game name or game cover.
- Implemented ability to filter games by genre
In the database:
- Dropped an unnecessary ID string column from every table; this string was a string version of the uniqueidentifier ID that was used initially
- Added an additional table connected to Game, which will hold video URL's for the respective game

**Version 3: 09/30/2021**

This update:
- Provides further CRUD capabilities by allowing the user to add a genre to a game
- Allows the user to move the modal

**Version 4: 10/07/2021**

This update:
- Provided default values for adding a game and genre, making the site a bit cleaner and instructions straightfoward.
- Implements further CRUD capabilities by allowing the user to add a genre (in general, not just to a game).
- Implements further CRUD capabilities by allowing the user to add YouTube videos to a specific game.
- Changed the interface design of filtering genres/tags. Previously genres were confined in 
  uniformly-sized boxes, even if the genres had more characters; this resulted in a crammed look.
- In the SQL database, there is now another table that connects to the primary Game table: the GameVideo table.   
- The GameVideo table consists of a GameId and a VideoUrl. 

**Future actions**
- Implement authorization so only authenticated and authorized users can CREATE, UPDATE, and DELETE. (!important) (1st priority)
- Allow the user to: edit genres, remove genres to a game, provide YouTube links for a provided game
