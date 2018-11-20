using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class CategoryDish
    {
        public int Id { get; set; }
        public string name { get; set; }
        public Nullable<int> main_dish_id { get; set; }
        public int number { get; set; }
        public decimal price { get; set; }
    }
}
