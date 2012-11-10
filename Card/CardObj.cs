using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe_Box
{
    public class CardObj
    {
        private List<string> ingredient_list;
        private List<string> instruction_list;
        private List<string> tags_list;

        public CardObj()
        {
            ingredient_list = new List<string>();
            instruction_list = new List<string>();
            tags_list = new List<string>();
        }

        public List<string> IngredientList
        {
            get { return this.ingredient_list; }
            set { this.ingredient_list = value; }

        }

        public List<string> InstructionList
        {
            get { return this.instruction_list; }
            set { this.instruction_list = value; }

        }

        public List<string> TagsList
        {
            get { return this.tags_list; }
            set { this.tags_list = value; }

        }
    }
}
