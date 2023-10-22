using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmspCardGame
{
    internal class CardGame
    {

        // PROPERTY ------------------------------------
        int turn = default;
        int subturn = default;
        CardPlayer _winner;
        bool _game_in_progress = false;
        List<Card> _cards_stick = new List<Card>();
        List<CardPlayer> _players_list = new List<CardPlayer>();
        List<Card> _game_table = new List<Card>();
        DateTime _start = DateTime.Now;
        enum StickType { thirty_six = 36, fifty_two = 52 }
        StickType _stickType;
        static public void exe() { Init(); }
        // CTOR -----------------------------------------
        CardGame()
        {
            _stickType = StickType.thirty_six;
            if(!GetEtalonStick()) throw new Exception("stick problem");
            if (!Shuffle()) throw new Exception("shuffle problem");
            if (!PlayersInvite()) throw new Exception("player create problem");
            EndOfGame_event = EndofGameCheck;
            StartGame();
        }
        // METHODS --------------------------------------
        static void Init() { new CardGame(); }
        bool GetEtalonStick()
        {
            for (CardRate i = CardRate.six; i <= CardRate.ace; i++)
                for (CardType ii = CardType.piki; ii <= CardType.diamonds; ii++)
                    _cards_stick.Add(new Card(i, ii));
            return true;
        }
        bool Stick_prepare_random_order()
        {
            List<KeyValuePair<CardRate, CardType>> _etalon_36_stick = new List<KeyValuePair<CardRate, CardType>>();
            
            for (CardRate i = CardRate.six; i <= CardRate.ace; i++)
                for (CardType ii = CardType.piki; ii <= CardType.diamonds; ii++)
                    _etalon_36_stick.Add(new KeyValuePair<CardRate, CardType>(i, ii));


            //case: StickType.thirty_six
            //for (int i = 0; i < (int)StickType.thirty_six; i++)
            for (int i = _etalon_36_stick.Count-1; i >= 0; i--)
            {
                int k = (int)ServiceFunction.Get_Random(0, i);
                KeyValuePair<CardRate, CardType> temp = _etalon_36_stick[k];
                _etalon_36_stick[k] = _etalon_36_stick[i];
                _etalon_36_stick[i] = temp;
            }

            foreach (var item in _etalon_36_stick)
                _cards_stick.Add(new Card(item));

            return true;
        }
        bool Shuffle()
        {
            for (int i = _cards_stick.Count - 1; i >= 0; i--)
            {
                int k = (int)ServiceFunction.Get_Random(0, i);
                var temp = _cards_stick[k];
                _cards_stick[k] = _cards_stick[i];
                _cards_stick[i] = temp;
            }
            return true; 
        }
        bool PlayersInvite(int players_quantity = 0)
        {
            if (players_quantity == 0) players_quantity = (int)ServiceFunction.Get_Random(2, 7.99);
            for (int i = 0; i < players_quantity; i++)
                _players_list.Add(new CardPlayer());
            return true;
        }
        void StartGame()
        {
            _game_in_progress = true;
            CardsDistrubution();
            GameProgressHandler();
        }
        void CardsDistrubution()
        {
            int i = default;
            List<Card> _cards_to_remove = new List<Card>();
            // Если карт не хватает на раздачу поровну, остаток остается в колоде
            while (i < (_cards_stick.Count / _players_list.Count) * _players_list.Count)
            {
                for (int ii = 0; ii < _players_list.Count; ii++)
                {
                    _cards_to_remove.Add(_cards_stick[i]);
                    _players_list[ii].Hand_.Add(_cards_stick[i++]);                }
            }
            // убираем из колоды выданные карты
            foreach (var card in _cards_to_remove) { _cards_stick.Remove(card); }
        }
        void GameProgressHandler()
        {
            CardPlayer Attacker, Defender;

            while (_game_in_progress)
            {
                turn++;
                for (int i = 0; i < _players_list.Count; i++)
                {
                    subturn++;

                    Attacker = _players_list[i];
                    if (Attacker.Hand_.Count == 0) continue;

                    int ii = i;
                    Defender = _players_list[NextPlayer(ii)];
                    while (Defender.Hand_.Count == 0) 
                        {                            
                            ii = NextPlayer(ii);
                            Defender = _players_list[ii];
                        } 

                    _game_table.Add(Attacker.makeMove());
                    _game_table.Add(Defender.makeMove());

                    DecisionMaker(Attacker, Defender);
                    if (subturn % _players_list.Count == 0) GameStatus();
                    Thread.Sleep(25);
                    if (EndOfGame_event()) WeHaveaWinner();

                    if (!IsGameTableClear()) throw new Exception("game_table not empty");
                }
            }
        } 
        int NextPlayer(int i) { if (i < _players_list.Count-1 ) return i + 1; return 0; }
        void DecisionMaker(CardPlayer Attacker, CardPlayer Defender)
        {
            if (_game_table[0].Rate_ == CardRate.six && _game_table[1].Rate_ == CardRate.ace) { Win(Attacker); }
            else if (_game_table[0].Rate_ == CardRate.ace && _game_table[1].Rate_ == CardRate.six) { Win(Defender); }
            else if (_game_table[0].Rate_ > _game_table[1].Rate_ )   { Win(Attacker); }            
            else if (_game_table[0].Rate_ < _game_table[1].Rate_)    { Win(Defender); }
            else if (_game_table[0].Rate_ == _game_table[1].Rate_)   Draw(Attacker,  Defender);  
        }
        void Win(CardPlayer player)
        {
            // Добавляем выигранные карты в низ колоды
            foreach (Card card in _game_table) 
                player.Hand_.Insert(0, card);

            _game_table.Clear();
        } 
        void Draw(CardPlayer Attacker, CardPlayer Defender)
        {
            // после ничьей убираем карты в низ колоды
            Attacker.Hand_.Insert(0, _game_table[0]);
            Defender.Hand_.Insert(0, _game_table[1]);
            _game_table.Clear();
        }
        bool IsGameTableClear()
        {
            if (_game_table.Count > 0) { return false; }
            return true;
        }
        bool EndofGameCheck()
        {
            foreach (CardPlayer player in _players_list)
                if (player.Hand_.Count >= ((int)_stickType / _players_list.Count) * _players_list.Count)
                { _winner = player; return true; }        
            return false;
        }
        void WeHaveaWinner()
        {
            _game_in_progress = false;
            GameStatus();
            Console.WriteLine($"\nGame end\nWinner is {_winner}!\n" +
                "Game time {0} sec", Math.Round((DateTime.Now - _start).TotalSeconds, 3));
        }
        void GameStatus()
        {
            Console.Clear();
            Console.WriteLine("Turn {0} Subturn {1}\n", turn, subturn);            
            for (int i = 0; i < _players_list.Count; i++)
                Console.WriteLine($"{i+1}  {_players_list[i]}".PadRight(17) +
                    $"\tcards: {_players_list[i].Hand_.Count}".PadLeft(7));
        }

        // EVENTs ------------------------------------
        delegate bool endofgamecheck_dlgt();
        event endofgamecheck_dlgt EndOfGame_event;

    }

}
