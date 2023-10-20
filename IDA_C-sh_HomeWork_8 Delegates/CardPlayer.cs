using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmspCardGame
{
    internal class CardPlayer
    {
        public string Name_ { get; set; } = "default name";
        public List<Card> Hand_ { get; set; } = new List<Card>();
        public CardPlayer() { }
        public Card Attack()
        {
            return AggressiveTactic();
        }
        public Card Defend (List<Card> game_table)
        {

        }
        Card AggressiveTactic()
        {
            return Hand_.Max();
        }
        Card ConservativeTactic()
        {
            return Hand_.Min();
        }
        Card BalancedTactic() 
        {
            int BalancedRate = ((int)Hand_.Min().Rate_ + (int)Hand_.Max().Rate_) / 2;



            return Hand_.In();
        }
    }
}
