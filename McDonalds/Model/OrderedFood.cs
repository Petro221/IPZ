using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class OrderedFood
    {
        public int Id { get; set; }
        public int order_id { get; set; }
        public int category_id { get; set; }
        public int number { get; set; }
    }
}
