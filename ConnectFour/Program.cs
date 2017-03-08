using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Hello there! To whoever reviews this, I just want to say that I have attempted 
/// to polsih this code to the best of my ability, making it as scalable and resiliant
/// as possible. I would very much love to move forward with this recruitment process
/// and work at your company, and will approach any task given to me with the same
/// enthusiastic and methodical approach as I took with this one. Thank you very
/// much for your time.
/// 
/// Kind regards,
/// Jack Rebetzke
/// </summary>
namespace ConnectFour
{
    class Program
    {
        static void Main(string[] args)
        {
            MainLoop();
        }

        /// <summary>
        /// Plays a game, then asks if the user wants to play again or exit.
        /// </summary>
        static void MainLoop()
        {
            bool runGame = true;
            while (runGame)
            {
                IGame game = new Game();
                while (GameLoop(game)) { }
                Console.WriteLine("Press X to exit, or any other key to play again: ");
                ConsoleKeyInfo playAgain = Console.ReadKey();
                if (playAgain.KeyChar == 'X' || playAgain.KeyChar == 'x') runGame = false;
            }
        }

        /// <summary>
        /// Attempts to setup the game until successful, otherwise executes a turn until the game ends.
        /// </summary>
        /// <param name="game">The game object</param>
        /// <returns>Returns true when the game is over, otherwise false.</returns>
        static bool GameLoop(IGame game)
        {
            if (!game.isSetup)
            {
                game.setup();
            } else
            {
                game.executeTurn();
                if (game.gameOver) return false;
            }
            return true;
        }


    }
}
