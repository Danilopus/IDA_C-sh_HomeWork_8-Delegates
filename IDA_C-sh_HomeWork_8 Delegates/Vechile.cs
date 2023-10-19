using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmspRaceGame
{
    //abstract internal class Vechile : IEnumerable<Vechile>
    abstract internal class Vechile
    {
        // PROPERTIES ------------------------------------
        public ConsoleColor Color_ { set; get; } = ConsoleColor.White;
        public double MaxSpeed_ { set; get; } = default; // km/h
        public double CoveredDistance_ { set; get; } = default; // kilometers
        long time_of_race = default; // sec
        public double Weight_ { get; set; } = default; // kg
        public double EnginePower_ { set; get; } = default; // hp
        public string? Textfield_ { set; get; } = default;
        static int _id_counter = default;
        int _id = default;
        public int ID_ {  get { return _id; } }
        bool _is_started = false;

        // CTOR ------------------------------------------
        public Vechile()
        {
            Color_ = (ConsoleColor)(int)ServiceFunction.Get_Random(1, 15);
            _id = _id_counter++;
        }
        
        // METHODS ----------------------------------------
        public override string ToString()
        {
            return Textfield_ + $" id[{ID_}]";
        }
        virtual public void Start(double distance)
        {
            _is_started = true;
            Console.ForegroundColor = Color_;
            Console.WriteLine(this + " started! \n");
            Console.ForegroundColor = ConsoleColor.White;

        }
        public double CoveredDistance(long timeframe_number)
        {
            double Koef_random = ServiceFunction.Get_Random(0.75, 1.00);
            double Koef_kmph_to_mps = 1.0 / 3600; // кэффициент перевода км/ч в км/с
            double Inertia_factor = (Weight_ / EnginePower_) * 10; // фактор инерции: как долго разгоняется автомобиль
            CoveredDistance_ += Koef_random * (MaxSpeed_* Koef_kmph_to_mps) * (1 - Inertia_factor / (timeframe_number + Inertia_factor));
            time_of_race++;
            return CoveredDistance_;
        }

       /* public IEnumerator<Vechile> GetEnumerator()
        {
            return ID_;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }*/




        // Надо добавить события
        //event finish;


    }
}
