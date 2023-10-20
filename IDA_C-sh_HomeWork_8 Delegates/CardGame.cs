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
        bool _game_in_progress = false;
        List<Card> _cards_stick = new List<Card>();
        List<CardPlayer> _players_list = new List<CardPlayer>();
        List<Card> _game_table = new List<Card>();
        enum StickType { thirty_six = 36, fifty_two = 52 }
        StickType _stickType;
        static public void exe() { Init(); }
        // CTOR -----------------------------------------
        CardGame()
        {
            _stickType = StickType.thirty_six;
            if (!Stick_prepare_random_order()) throw new Exception("stick problem");
            if (!PlayersInvite()) throw new Exception("player create problem");
            StartGame();
        }
        // METHODS --------------------------------------
        static void Init() { new CardGame(); }
        bool Stick_prepare_random_order()
        {
            List<KeyValuePair<CardRate, CardType>> _etalon_36_stick = new List<KeyValuePair<CardRate, CardType>>();
            for (CardRate i = CardRate.six; i <= CardRate.ace; i++)
                for (CardType ii = CardType.piki; ii <= CardType.diamonds; j++)
                    _etalon_36_stick.Add(new KeyValuePair<CardRate, CardType>(i, ii));


            //case: StickType.thirty_six
            //for (int i = 0; i < (int)StickType.thirty_six; i++)
            for (int i = _etalon_36_stick.Count; i > 0; i--)
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
        bool PlayersInvite(int players_quantity = 0)
        {
            if (players_quantity == 0) players_quantity = (int)ServiceFunction.Get_Random(2, 5);
            for (int i = 0; i < players_quantity; i++)
                _players_list.Add(new CardPlayer());
            return true;
        }
        void StartGame()
        {           
            CardsDistrubution();
            GameProgressHandler();
            _game_in_progress = true;
        }
        void CardsDistrubution()
        {
            int i = default;
            // Если карт не хватает на раздачу поровну, остаток остается в колоде
            while (i < (_cards_stick.Count - _players_list.Count + 1))
            {
                for (int ii = 0; ii < _players_list.Count; ii++)
                    _players_list[ii].Hand_.Add(_cards_stick[i++]);
            }
        }
        void GameProgressHandler()
        {
            CardPlayer Attacker, Defender;

            //while (_game_in_progress)
            turn++;
         
            
            for (int i = 0; i < _players_list.Count; i++)
            {
                Attacker = _players_list[i];
                Defender = _players_list[NextPlayer(i)];
                
                _game_table.Add(Attacker.Attack());
                _game_table.Add(Defender.Defend(_game_table));

                DecisionMaker(Attacker, Defender);

                if (EndofGameCheck()) WeHaveaWinner();

                if (!IsGameTableClear()) throw new Exception("game_table not empty");
            }

        }
        int NextPlayer(int i) { if (i < _players_list.Count -1 ) return i + 1; return 0; }
        void DecisionMaker(CardPlayer Attacker, CardPlayer Defender)
        {
            if (_game_table[0].Rate_ > _game_table[1].Rate_ ) 

            { Win(Attacker); }
            
            if (_game_table[0].Rate_ < _game_table[1].Rate_)
            { Win(Defender); }

            Draw(Attacker,  Defender);            

        }
        void Win(CardPlayer player)
        {
            foreach (Card card in _game_table) { player.Hand_.Add(card); _game_table.Remove(card); }
            // _game_table.Clear();
        }
/*        void DefenderWin(CardPlayer Defender)
        {
            foreach (Card card in _game_table) { Defender.Hand_.Add(card); _game_table.Remove(card); }
            // _game_table.Clear();
        }*/        
        void Draw(CardPlayer Attacker, CardPlayer Defender)
        {
            Attacker.Hand_.Add(_game_table[0]);
            Defender.Hand_.Add(_game_table[1]);
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
             if (player.Hand_.Count >= 36) return true;
           
            return false;
        }
        void WeHaveaWinner()
        {
            _game_in_progress = false;
        }





    }

}
