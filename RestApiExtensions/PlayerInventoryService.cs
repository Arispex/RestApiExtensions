using TShockAPI;

namespace RestApiExtensions;

public static class PlayerInventoryService
{
    public static bool TryGetInventory(string playerName, out NetItem[] inventory, out string? error)
    {
        inventory = Array.Empty<NetItem>();
        error = null;

        if (string.IsNullOrWhiteSpace(playerName))
        {
            error = "玩家名称不能为空";
            return false;
        }

        var account = TShock.UserAccounts.GetUserAccountByName(playerName);
        if (account == null)
        {
            error = "玩家未找到";
            return false;
        }

        var data = TShock.CharacterDB.GetPlayerData(null, account.ID);
        if (data == null)
        {
            error = "未找到玩家数据";
            return false;
        }

        inventory = data.inventory ?? Array.Empty<NetItem>();
        return true;
    }
}
