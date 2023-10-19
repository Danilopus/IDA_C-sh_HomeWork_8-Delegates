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
        List<Card> _cards_stick = new List<Card>();
        List<CardPlayer> _players_list = new List<CardPlayer>();
        enum StickType { thirty_six = 36, fifty_two = 52}
        StickType _stickType;
        static public void exe(){ Init(); }        
        // CTOR -----------------------------------------
        CardGame() 
        {
            _stickType = StickType.thirty_six;
            if(!Stick_prepare_random_order()) throw new Exception("stick problem");
            if (!PlayersInvite()) throw new Exception("player create problem");
            StartGame();
        }
        // METHODS --------------------------------------
        static void Init(){new CardGame();}
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
            if (players_quantity == 0) players_quantity = (int) ServiceFunction.Get_Random(2, 5);
            for (int i = 0; i < players_quantity; i++)
                _players_list.Add(new CardPlayer());
            return true; 
        }
        void StartGame()
        {
            //for (int i = 0; i < _cards_stick.Count; i++) 

            int i = default;
            while (i < (_cards_stick.Count - _players_list.Count + 1))
            { 
            for (int ii = 0; ii < _players_list.Count; ii++)                    
                _players_list[ii].Hand_.Add(_cards_stick[i++]);
            }

        }

    }
}
