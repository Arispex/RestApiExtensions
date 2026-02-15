using TShockAPI;

namespace RestApiExtensions;

public static class UserInventoryService
{
    public static bool TryGetInventory(string user, out NetItem[] inventory, out string? error)
    {
        inventory = Array.Empty<NetItem>();
        error = null;

        if (!UserDataService.TryGetPlayerData(user, out var data, out error))
        {
            return false;
        }

        inventory = data.inventory ?? Array.Empty<NetItem>();
        return true;
    }
}
