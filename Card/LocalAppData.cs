using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe_Box
{
    public class LocalAppData
    {
        public int CardCount;
        public List<CardObj> AllCardData;
        public LocalAppData ()
        {
            CardCount = 0;
            AllCardData = new List<CardObj>();
        }

        public void RemoveCard(int index)
        {
            this.AllCardData.RemoveAt(index);
            this.CardCount -= 1;

        }

        public void AddCard(CardObj Card)
        {
            this.AllCardData.Add(Card);
            this.CardCount += 1;
        }
    }
}
