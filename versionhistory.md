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
   - Note that this modal is created purely in Blazor instead of utilizing Bootstrap's modal. This is due to Blazor's nature

**Version 1: 09/14/2021**

This update:
- Makes a modal be closed by clicking outside of it, instead of only being able to click the Close button
- Allows a user to add a game to the SQL database
