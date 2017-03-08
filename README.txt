How to play:
1. Click on the shortcut in the top level folder
2. You will be prompted for board dimensions. A non-numerical or negative
response will be deemed invalid and you will be asked again. While techically 
the only limit to the dimensions of the board is the range of an unsigned int
(0 to 4,294,967,295), input values over 50 will result in the top of your board
being clipped off by the command line buffer. The buffer size can be changed
in the properties menu for the command line itself.
3. The first player(always Red) will be prompted to make a move. Input the number
representing the column you wish to enter a token into. Columns are numbered 1
through the max width of your board. Non-numerical and negative responses wil not
be accepted. After a move is successfully made, the second player will be prompted 
to make a move.
4. When a win condition is achieved, the winner and how they won will be printed to
the console. At this point you will be prompted to press x(lower or uppercase will 
work) to exit, or any other key to play again.

Have fun!