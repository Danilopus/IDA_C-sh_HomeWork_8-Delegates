using nmspRaceGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDA_C_sh_HomeWork
{
    internal class RaceBus : Vechile
    {
        public RaceBus()
        {
            MaxSpeed_ = 180;
            Weight_ = 9e3;
            EnginePower_ = 2.5e3;
            Textfield_ = "RaceBus";
        }
    }
}
