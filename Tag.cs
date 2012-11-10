using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Recipe_Box
{
    public class Tag
    {
        public string Name {get; set;}
        public int Category { get; set; }

        public Tag(string name, int category)
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
        private Dictionary<string, int> categories { get; set; }

        public TagDatabase()
        {
            this.tags = new List<Tag>();
            this.categories = new Dictionary<string, int>();

            this.categories.Add("baking", 0);
            this.categories.Add("meat", 1);
            this.categories.Add("chinese", 2);

            this.tags.Add(new Tag("chocolate", categories["baking"]));
            this.tags.Add(new Tag("flour", categories["baking"]));
            this.tags.Add(new Tag("milk", categories["baking"]));
            this.tags.Add(new Tag("chicken", categories["meat"]));
            this.tags.Add(new Tag("steak", categories["meat"]));

        }

        public void AddTag(string name, string category)
        {
            this.tags.Add(new Tag(name, categories[category]));
        }

        public void AddCategory(string name)
        {
            this.categories.Add(name, this.categories.Count);
        }

        private List<Tag> FindByCategory(string category)
        {
            return this.tags.FindAll(
                delegate(Tag tag)
                {
                    return tag.Category == this.categories[category];
                }
            );
        }

        public ObservableCollection<Tag> TagCollectionFromCategory(string category)
        {
            ObservableCollection<Tag> tagsList = new ObservableCollection<Tag>();

            foreach (Tag tag in this.FindByCategory(category))
            {
                tagsList.Add(tag);
            }

            return tagsList;
        }

        public ObservableCollection<string> CategoriesCollection()
        {
            ObservableCollection<string> categories = new ObservableCollection<string>();

            foreach (KeyValuePair<string, int> kvp in this.categories)
            {
                categories.Add(kvp.Key);
            }

            return categories;
        }
    }
}
