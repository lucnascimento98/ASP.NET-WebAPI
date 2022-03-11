namespace ContosoPizza.Models.Enums
{
    public enum Claims
    {
        AddPizza = 0,
        EditPizza = 1,
        DeletePizza = 4,
        AddTopping = 5,
        EditTopping = 6,
        DeleteTopping = 9,
        AddUserAdmin = 10,
        EditAllUser = 11,
        GetUser = 12,
        GetAllUser = 13,
        DeleteAllUser = 14,
        ChangePassword = 15,
        AddRole = 16,
        EditRole = 17,
        GetRole = 18,
        GetAllRole = 19,
        DeleteRole = 20,
        AddClaimToRole = 21,
        RemoveClaimFromRole = 22,
        ListRoleClaims = 23,
        GetAllUsersOrders = 24,
        OrderPizza = 25
    }
}

/*
 * admin = 0, 1, 4, 5, 6, 9, 11, 10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24
 * 
 * user = 25
 */
