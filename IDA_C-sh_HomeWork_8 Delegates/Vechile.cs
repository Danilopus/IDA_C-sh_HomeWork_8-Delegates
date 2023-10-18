using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDA_C_sh_HomeWork
{
    abstract internal class Vechile : IEnumerable<Vechile>
    {
        // PROPERTIES ------------------------------------
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
            _id = _id_counter++;
        }
        
        // METHODS ----------------------------------------
        virtual public void Start(double distance)
        {
            _is_started = true;

        }
        public double CoveredDistance(long timeframe_number)
        {
            double K_random = ServiceFunction.Get_Random(0.75, 1.00);
            CoveredDistance_ += MaxSpeed_ * K_random * ((1 - 1 / (timeframe_number + 1)) * EnginePower_ / Weight_);
            time_of_race++;
            return CoveredDistance_;
        }

        public IEnumerator<Vechile> GetEnumerator()
        {
            return ID_;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }




        // Надо добавить события
        //event finish;


    }
}
