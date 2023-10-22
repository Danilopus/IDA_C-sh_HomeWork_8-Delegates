using IDA_C_sh_HomeWork;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmspRaceGame
{
    public enum GameSpeed { slow_x_2, real, fast_x_2, veryfast_x_5, veryveryfast_x_10, ultrafast_x_20 }
    internal class RaceGame
    {
        // PROPERTIES ------------------------------------
        protected List <Vechile> _vechileList = new List<Vechile>();
        //protected SortedDictionary <Vechile,double> _result_list = new SortedDictionary<Vechile, double>();
        protected Dictionary<Vechile, double> _result_list = new Dictionary<Vechile, double>();
        public double Distance_ { get; set; } = default; // km
        static bool _state = default;
        long _race_time = default; // sec
        static GameSpeed _speed;// = GameSpeed.ultrafast_x_20;

        // CTOR ------------------------------------------
        public RaceGame(GameSpeed speed = GameSpeed.veryfast_x_5) : this((int)ServiceFunction.Get_Random(3, 10), Math.Round(ServiceFunction.Get_Random(1, 5), 1), speed) {}
        //public RaceGame(GameSpeed speed): 
        RaceGame(int car_quantity, double distance, GameSpeed speed = GameSpeed.veryfast_x_5)
        {
            Distance_ = distance;
            _speed = speed;
            inGameTimer.Timeframe_length_ = inGameTimer.GetTimeFrameLength(); // Можно установить длину таймфрейма (длительность игровой секунды) в мс (1000 мс - время 1:1 с реальным)
            weHaveaWinner_event += weHaveaWinner_handler;
            check_result += CheckResult_handler;
            GetCarsforRace(car_quantity);
            StartRace();
        }
        //public static bool exe { get { Initialise(); return _state; } }
        public delegate void StartApp(GameSpeed speed = GameSpeed.veryfast_x_5);
        static public StartApp exe = Initialise;

        // METHODS ----------------------------------------
        static void Initialise(GameSpeed speed = GameSpeed.veryfast_x_5)
        {
            new RaceGame(speed);
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
            switch((int)ServiceFunction.Get_Random(0,3.99))
            { 
                case 0: return new MotoCycle();
                case 1: return new RaceCar();
                case 2: return new RaceTruck();
                case 3: return new RaceBus();

            }
            throw new Exception("problem with RandomVechile");

        }
        void StartRace()
        {
            _state = true;
            onStartAnounce();
            // вызов метода Start() всех учавствующих машин
            StartRace_dlgt start_dlgt = StartRace_dlgt_handler;
            foreach (Vechile vechile in _vechileList) { start_dlgt(vechile, Distance_); }
            // Передача управление гонкой специальному методу
            RaceHandler();
        }
        void onStartAnounce()
        {
            Console.Write("\nVechiles on start:\n" + String.Join("\n", _vechileList));
            Console.Write("\n\n!!!GET READY!!! (press any key when ready)\n");
            Console.ReadKey();
            Console.Write("\n\nRace will start in: ");
            for (int i = 5; i >= 0; i--) { Console.Write("\b " + i); Thread.Sleep(1000); }
            Console.Clear();
            Console.Write("\nRACE STARTED!!!\n\n");
        }
        void RequestStatus(int request_frequency = 1)
        {
            if (_race_time % request_frequency == 0)
            {
                Console.Clear();
                Console.Write("\nGame speed: {0}", _speed);
                Console.Write("\nRace distance: {0} km", Distance_);
                Console.Write("\nRace in progress: {0}\n", _state);
                Console.Write("\nRace time: {0} sec", _race_time);
                Console.Write("\nCurrent positions:\n");

                List<KeyValuePair<Vechile, double>> sorted_results = _result_list.OrderBy(d => d.Value).ToList();
                sorted_results.Reverse(); 
                int i = 0;
                foreach (var item in sorted_results)
                {
                    Console.ForegroundColor = item.Key.Color_;
                    Console.Write("\n{0}  {1}\t".PadRight(15), ++i, item.Key );
                    Console.Write("Distance: {0} km\n".PadLeft(15), Math.Round(item.Value, 3));
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        void RaceHandler()
        {
            Thread.Sleep(1500);
            while (_state)
            {
                // добавляем 1 игровыую секунду
                inGameTimer.Tic(this);
                // Обсчитываем перемещение машин за 1 игровую секунду
                RaceTicEvalute();
                // Выводим отчет
                if (_state) RequestStatus(1); // параметр  - на сколько тиков выводить отчет;
            }
        }
        class inGameTimer
        {
            static public int Timeframe_length_ { get; set; } = 100; // ms
            static public void Tic(RaceGame raceGame) { Thread.Sleep(Timeframe_length_); raceGame._race_time++; }
            static inGameTimer() { Timeframe_length_ = 100; }            
            public static int GetTimeFrameLength()
            {
                switch (_speed) 
                {
                    case GameSpeed.slow_x_2: return 2000; break;
                    case GameSpeed.real: return 1000; break;
                    case GameSpeed.fast_x_2: return 500; break;
                    case GameSpeed.veryfast_x_5: return 200; break;
                    case GameSpeed.veryveryfast_x_10: return 100; break;
                    case GameSpeed.ultrafast_x_20: return 50; break;
                    default: return 500;
                }
            }
        }
        void RaceTicEvalute()
        {    
             RequestStatus_dlgt request_dlgt = RequestStatus_dlgt_handler;
             foreach (Vechile vechile in _vechileList) { _result_list[vechile] = request_dlgt(vechile, _race_time); }

            if (check_result()) weHaveaWinner_event(GetWinner());
        }
        Vechile GetWinner()
        {
            return _result_list.MaxBy(d => d.Value).Key;            
        }

        // DELEGATES --------------------------------------
        delegate void StartRace_dlgt(Vechile vechile_obj, double distance);
        void StartRace_dlgt_handler (Vechile vechile_obj, double distance) { vechile_obj.Start(distance); }
        delegate double RequestStatus_dlgt(Vechile vechile_obj, long time_frame);
        double RequestStatus_dlgt_handler(Vechile vechile_obj, long time_frame) { return vechile_obj.CoveredDistance(time_frame); }
        delegate double RequestStatus_dlgt_2(long time_frame);
        //double RequestStatus_dlgt_2_handler(long time_frame) { return vechile_obj.CoveredDistance(time_frame); }
        delegate void weHaveaWinner(Vechile vechile);
        void weHaveaWinner_handler(Vechile vechile) 
        { 
            _state = false;
            RequestStatus();
            int blink_number = 0;
            Console.WriteLine();
            while (blink_number < 10) 
            {
            blink_number++;
            Console.Write("And the winner is {0}!!!\r", vechile);
            Thread.Sleep(300);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("And the winner is {0}!!!\r", vechile);
            Thread.Sleep(400);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("And the winner is {0}!!!\r", vechile);
            }
            Console.Write("And the winner is {0}!!!", vechile);
        }
        delegate bool CheckResult();
        //bool CheckResult_handler() { foreach (var result_record in _result_list) if (result_record.Value >= Distance_) return true; return false; }
        bool CheckResult_handler() { foreach (var vechile in _vechileList) if (vechile.CoveredDistance_ >= Distance_) return true; return false; }

        // EVENTS
        event weHaveaWinner weHaveaWinner_event;
        event CheckResult check_result;
    }
}
