using TShockAPI;

namespace RestApiExtensions;

public static class UserDataService
{
    public static bool TryGetPlayerData(string user, out PlayerData data, out string? error)
    {
        data = null!;
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

        data = TShock.CharacterDB.GetPlayerData(null, account.ID);
        if (data == null)
        {
            error = "未找到用户数据";
            return false;
        }

        return true;
    }
}
