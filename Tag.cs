using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe_Box
{
    public class Tag
    {
        public string Name {get; set;}
        public string Category { get; set; }

        public Tag(string name, string category)
        {
            this.Name = name;
            this.Category = category;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class TagDatabase
    {
        private List<Tag> tags { get; set; }

        public TagDatabase()
        {
            this.tags = new List<Tag>();

            this.tags.Add(new Tag("chocolate", "baking"));
            this.tags.Add(new Tag("flour", "baking"));
            this.tags.Add(new Tag("milk", "baking"));
            this.tags.Add(new Tag("chicken", "meat"));
            this.tags.Add(new Tag("steak", "meat"));
        }

        public List<Tag> FindByCategory(string category)
        {
            return tags.FindAll(
                delegate(Tag tag)
                {
                    return tag.Category == category;
                }
            );
        }
    }
}
