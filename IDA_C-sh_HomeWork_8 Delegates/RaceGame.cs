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
        // PROPERTIES ------------------------------------
        protected List <Vechile> _vechileList = new List<Vechile>();
        protected SortedDictionary <Vechile,double> _result_list = new SortedDictionary<Vechile, double>();
        public double Distance_ { get; set; } = default; // km
        static bool _state = default;
        long _race_time = default; // sec

        // CTOR ------------------------------------------
        public RaceGame() : this((int)ServiceFunction.Get_Random(3, 10), ServiceFunction.Get_Random(10, 100)) { } // km
        RaceGame(int car_quantity, double distance)
        {
            Distance_ = distance;
            weHaveaWinner_event += weHaveaWinner_handler;
            check_result += CheckResult_handler;
            GetCarsforRace(car_quantity);
            StartRace();
        }  
        //public static bool exe { get { Initialise(); return _state; } }
        public delegate void StartApp();
        static public StartApp exe = Initialise;

        // METHODS ----------------------------------------
        static void Initialise()
        {
            new RaceGame();
        }
        void GetCarsforRace(int car_quantity)
        {
            for (int i = 0; i < car_quantity; i++)
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
            StartRace_dlgt start_dlgt = StartRace_dlgt_handler;
            foreach (Vechile vechile in _vechileList) { start_dlgt(vechile, Distance_); }
            RaceHandler();
        }
        void RequestStatus(int request_frequency)
        {
            if (_race_time % request_frequency == 0)
            {
                Console.WriteLine("\nRace time: {0} sec\n", _race_time);
                int i = 0;
                foreach (var item in _result_list) Console.WriteLine("\n{0}\t{1}\tDistance: {2}\n", ++i, item.Key.Textfield_, item.Value);
            }
        }
        void RaceHandler()
        {            
            inGameTimer.Timeframe_length_ = 500; // Можно установить длину таймфрейма в мс (1000 мс - время 1:1 с реальным)
            while (_state)
            {
                inGameTimer.Tic(this);
                RaceTicEvalute();
                RequestStatus(10); // параметр  - на сколько тиков выводить отчет;
            }
        }
        class inGameTimer
        {
            static public int Timeframe_length_ { get; set; } = 100; // ms
            static public void Tic(RaceGame raceGame) { Thread.Sleep(Timeframe_length_); raceGame._race_time++; }
            static inGameTimer() { Timeframe_length_ = 100; }
        }
        void RaceTicEvalute()
        {
           /* RequestStatus_dlgt_2 request_dlgt_2 = null;
            foreach (Vechile vechile in _vechileList)
                request_dlgt_2 += vechile.CoveredDistance;
            if (request_dlgt_2 != null) request_dlgt_2(_race_time);
            else throw new Exception("NULL at vechile_list");*/


             RequestStatus_dlgt request_dlgt = RequestStatus_dlgt_handler;
             foreach (Vechile vechile in _vechileList) { _result_list[vechile] = request_dlgt(vechile, _race_time); }

            if (check_result()) weHaveaWinner_event(GetWinner());
        }
        Vechile GetWinner()
        {
            foreach (Vechile vechile in _vechileList) { if (vechile.CoveredDistance_ >= Distance_)  { return vechile; } }
            return null;
        }

        // DELEGATES --------------------------------------
        delegate void StartRace_dlgt(Vechile vechile_obj, double distance);
        void StartRace_dlgt_handler (Vechile vechile_obj, double distance) { vechile_obj.Start(distance); }
        delegate double RequestStatus_dlgt(Vechile vechile_obj, long time_frame);
        double RequestStatus_dlgt_handler(Vechile vechile_obj, long time_frame) { return vechile_obj.CoveredDistance(time_frame); }
        delegate double RequestStatus_dlgt_2(long time_frame);
        //double RequestStatus_dlgt_2_handler(long time_frame) { return vechile_obj.CoveredDistance(time_frame); }
        delegate void weHaveaWinner(Vechile vechile);
        void weHaveaWinner_handler(Vechile vechile) { _state = false; Console.WriteLine("\nAnd the winner is {0}\n", vechile.Textfield_); }
        delegate bool CheckResult();
        bool CheckResult_handler() { foreach (var result_record in _result_list) if (result_record.Value >= Distance_) return true; return false; }

        // EVENTS
        event weHaveaWinner weHaveaWinner_event;
        event CheckResult check_result;
    }
}
