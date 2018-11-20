using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class SendingObject
    {
        public User user { get; set; }
        public List<Model.OrderedFood> orderedFood { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Status status { get; set; }
        public int mainDishId { get; set; }
        public decimal sumPrice { get; set; }
        public bool eatIn { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int categoryDishId { get; set; }
        public int number { get; set; }
        public int orderId { get; set; }
        public string cardNumber { get; set; }
        public string cardName { get; set; }
        public string cardDate { get; set; }
        public int ticketNumber { get; set; }
    }
}