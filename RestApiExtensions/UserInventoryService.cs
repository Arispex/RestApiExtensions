using TShockAPI;

namespace RestApiExtensions;

public static class UserInventoryService
{
    public static bool TryGetInventory(string user, out NetItem[] inventory, out string? error)
    {
        inventory = Array.Empty<NetItem>();
        error = null;

        if (string.IsNullOrWhiteSpace(user))
        {
            error = "用户不能为空";
            return false;
        }

        var account = TShock.UserAccounts.GetUserAccountByName(user);
        if (account == null)
        {
            error = "用户未找到";
            return false;
        }

        var data = TShock.CharacterDB.GetPlayerData(null, account.ID);
        if (data == null)
        {
            error = "未找到用户数据";
            return false;
        }

        inventory = data.inventory ?? Array.Empty<NetItem>();
        return true;
    }
}
