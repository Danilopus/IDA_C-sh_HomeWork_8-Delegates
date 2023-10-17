using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDA_C_sh_HomeWork
{
    internal class RaceCar : Vechile
    {
        public RaceCar() 
        {
            MaxSpeed_ = 150;
            Weight_ = 2000;
            EnginePower_ = 400;
            Textfield_ = "general RaceCar prototype";
        }
        public override void Start(double distance)
        {
            throw new NotImplementedException();
        }
    }
}
