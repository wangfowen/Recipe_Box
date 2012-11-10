using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe_Box.Card
{
    public class CardSearch
    {
        public static List<MyUserControl1> SearchFor(LocalAppData AppData, string SearchTerm)
        {
            string SearchForTerm = SearchTerm.ToUpper();
            List<MyUserControl1> Result = new List<MyUserControl1>();
            foreach (CardObj Card in AppData.AllCardData)
            {
                foreach (string CardTag in Card.TagsList)
                {
                    string CardTagString = CardTag.ToUpper();

                    if (CardTagString.Contains(SearchForTerm))
                    {
                        MyUserControl1 tmp_control = new MyUserControl1(600, 400, Card);
                        tmp_control.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                        tmp_control.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                        Result.Add(tmp_control);
                    }
                }
            }

            return Result;
        }
    }
}
