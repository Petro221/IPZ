using System;

namespace Model
{
    [Serializable]
    public class BankCreditCard
    {
        public int Id { get; set; }
        public string credit_card_number { get; set; }
        public string credit_card_name { get; set; }
        public string credit_card_date { get; set; }
    }
}
