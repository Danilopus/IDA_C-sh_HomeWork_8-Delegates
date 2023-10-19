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

    }
}
