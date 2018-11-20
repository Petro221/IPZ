using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class ResponseObject
    {
        public Status status { get; set; }
        public User user { get; set; }
        public List<Model.MainDish> MainDishes { get; set; }
        public List<CategoryDish> CategoryDishes { get; set; }
        public Order Order { get; set; }
        public int ticketNumber { get; set; }
    }
}
