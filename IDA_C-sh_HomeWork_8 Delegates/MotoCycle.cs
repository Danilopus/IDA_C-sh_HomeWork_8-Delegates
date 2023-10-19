using nmspRaceGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmspRaceGame
{
    internal class MotoCycle : Vechile
    {
        public MotoCycle()
        {
            MaxSpeed_ = 145;
            Weight_ = 200;
            EnginePower_ = 100;
            Textfield_ = "Motocycle";
        }
    }
}
