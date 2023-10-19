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
        CardRate _rate;
        CardType _type;
        public Card(KeyValuePair<CardRate, CardType> arg)
        {
            _rate = arg.Key;
            _type = arg.Value;
        }

    }
}
