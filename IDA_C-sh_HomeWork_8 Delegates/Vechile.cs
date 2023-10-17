using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDA_C_sh_HomeWork
{
    abstract internal class Vechile
    {
        public double MaxSpeed_ { set; get; } = default;
        public double CoveredDistance_ { set; get; } = default;
        public double Weight_ { get; set; } = default;
        public double EnginePower_ { set; get; } = default;
        static int _id_counter = default;
        int _id = default;
                public int ID_ {  get { return _id; } }
        virtual public void Start(double distance)
        {
            double K_random = ServiceFunction.Get_Random(0.75, 1.00);
            CoveredDistance_ += MaxSpeed_ * K_random * (1-1/time)*EnginePower_ / Weight_);
        }

        public Vechile()
        {
            _id = _id_counter++;
        }
        public string? Textfield_ { set; get; } = default;




        // Надо добавить события



    }
}
