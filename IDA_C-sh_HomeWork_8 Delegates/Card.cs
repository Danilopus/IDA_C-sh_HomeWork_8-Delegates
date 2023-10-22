using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmspCardGame
{
    enum CardRate { six, seven, eight, nine, ten, boy, lady, king, ace }
    enum CardType { piki, spades, hearts, diamonds }
    internal class Card
    {
        public CardRate Rate_ { set; get; }
        public CardType _type { set; get; }
        public Card(KeyValuePair<CardRate, CardType> arg)
        {
            Rate_ = arg.Key;
            _type = arg.Value;
        }
        public Card(CardRate rate, CardType type)
        {
            Rate_ = rate;
            _type = type;
        }


    }
}
