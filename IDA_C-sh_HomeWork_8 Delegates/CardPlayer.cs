using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmspCardGame
{
    internal class CardPlayer
    {
        // PROPERTIES -------------------------------------------
        int _id = 0; 
        static public int ID_ { get; set; } = default;
        string[] PlayerNames = new string[] { "Serj", "Coward", "Expierenced", "Fool", "Nicolas", "Elleanora", "Andronio", "Justin" };
        public string Name_ { get; set; } = "default name";
        public List<Card> Hand_ { get; set; } = new List<Card>();
        // C-TOR --------------------------------------------------
        public CardPlayer() { Name_ = PlayerNames[(int)ServiceFunction.Get_Random(0, PlayerNames.Length)]; _id = ID_++; }
        // METHOD -------------------------------------------------
        public override string ToString()
        {
            return Name_ + _id;
        }
        public Card makeMove()
        {
            if (Hand_.Count == 0) throw new Exception("Hand is empty");
            // Кладет верхнюю карту из своей колоды на игровой стол
            
            Card tmp = Hand_[Hand_.Count - 1];
            // удаляем карту из руки
            Hand_.Remove(Hand_[Hand_.Count - 1]);
            return tmp;
        }
        public Card Defend (List<Card> game_table)
        {
            if (Hand_.Count == 0) throw new Exception("Hand is empty");
            // Кладет верхнюю карту из своей колоды на игровой стол
            Card tmp = Hand_[Hand_.Count - 1];
            // удаляем карту из руки
            Hand_.Remove(Hand_[Hand_.Count - 1]);
            return tmp;

            /*         Hand_.Sort();
                     foreach (Card card in Hand_)
                     { 
                         if (card.Rate_ > game_table[0].Rate_)
                         {
                             var temp = card; 
                             Hand_.Remove(card);
                             return temp;
                         }
                     }
                     return Hand_.Min();*/
        }
        Card AggressiveTactic()
        {
            return Hand_.Max();
        }
        Card ConservativeTactic()
        {
            return Hand_.Min();
        }

    }
}
