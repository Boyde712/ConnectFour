using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    interface IGame
    {
        bool isSetup { get; }
        bool gameOver { get; }
        void setup();
        void executeTurn();
    }
}
