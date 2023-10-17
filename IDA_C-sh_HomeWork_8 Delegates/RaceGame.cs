using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDA_C_sh_HomeWork
{
    internal class RaceGame
    {
        protected List <Vechile> _vechileList;
        public double Distance_ { get; set; } = default;
        public RaceGame(): this((int)ServiceFunction.Get_Random(3,10), ServiceFunction.Get_Random(10,100)) { }
        static bool _state = default;
        public static bool exe
        {
            get { Initialise(); return _state; }
            set { _state = value; }
        }
        static void Initialise()
        {
            new RaceGame();
        }
        RaceGame(int car_quantity, double distance)
        {
            Distance_ = distance;
            GetCarsforRace(car_quantity);
            StartRace();
        }
        void GetCarsforRace(int car_quantity)
        {
            for (int i = 0;  i < car_quantity; i++)
            {
                _vechileList.Add(GetRandomVechile());
            }
        }
        Vechile GetRandomVechile()
        {
            return new RaceCar();
        }
        void StartRace()
        {
            _state = true;

            // заменить на  event
            try
            {
                // заменить на делегата            
                foreach (var vechile in _vechileList) { vechile.Start(Distance_); }
            
            }
            catch(Exception ex)
            { _state = false; }
        }


    }
}
