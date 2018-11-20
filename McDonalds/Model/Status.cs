using System;

namespace Model
{
    [Serializable]
    public enum Status
    {
        LOGIN,
        REGISTRATION,
        MAIN_DISHES,
        CATEGORY_DISHES,
        CREATE_ORDERED_FOOD,
        CREATE_ORDER,
        REMOVE_FOOD_FROM_CATEGORY_DISHES,
        CHECK_CREDIT_CARD
    }
}
