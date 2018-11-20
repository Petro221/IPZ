using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Order
    {
        public int Id { get; set; }
        public decimal sum_price { get; set; }
        public System.DateTime order_date { get; set; }
        public bool eat_in { get; set; }
        public bool is_payed { get; set; }
        public string customer_first_name { get; set; }
        public string customer_last_name { get; set; }
        public int ticket_number { get; set; }
    }
}
