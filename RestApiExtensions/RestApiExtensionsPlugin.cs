using System.Reflection;
using Rests;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace RestApiExtensions;

[ApiVersion(2, 1)]
public sealed class RestApiExtensionsPlugin(Main game) : TerrariaPlugin(game)
{
    private const string RoutePrefix = "/plugins/restapiextensions/v2";

    public override string Author => "千亦";

    public override string Description => "REST API Extensions";

    public override string Name => "REST API Extensions";

    public override Version Version => new(2, 0, 0);

    public override void Initialize()
    {
        TShock.RestApi.Register(new SecureRestCommand($"{RoutePrefix}/player/inventory", Inventory, "rae.player.inventory"));
        TShock.RestApi.Register(new SecureRestCommand($"{RoutePrefix}/world/progress", Progress, "rae.world.progress"));
    }

    private static object Inventory(RestRequestArgs args) // 查看玩家背包
    {
        var playerName = args.Request?.Parameters?["player"];
        if (string.IsNullOrWhiteSpace(playerName))
        {
            return Error("400", "缺少必要参数 player");
        }

        if (!PlayerInventoryService.TryGetInventory(playerName, out var inventory, out var error))
        {
            return Error("400", error ?? "玩家未找到");
        }

        return new RestObject()
        {
            {
                "response",
                inventory
            }
        };
    }

    private static object Progress(RestRequestArgs args) // 获取进度详情
    {
        var progress = new Dictionary<string, bool>()
        {
            {"史莱姆王", NPC.downedSlimeKing}, //史莱姆王
            {"克苏鲁之眼", NPC.downedBoss1}, //克苏鲁之眼
            {"世界吞噬者 或 克苏鲁之脑", NPC.downedBoss2}, //世界吞噬者 或 克苏鲁之脑
            {"蜂后", NPC.downedQueenBee}, //蜂后
            {"骷髅王", NPC.downedBoss3}, //骷髅王
            {"巨鹿", NPC.downedDeerclops}, //巨鹿
            {"肉山", Main.hardMode}, //肉山
            {"史莱姆皇后", NPC.downedQueenSlime}, //史莱姆皇后
            {"双子魔眼", NPC.downedMechBoss2}, //双子魔眼
            {"毁灭者", NPC.downedMechBoss1}, //毁灭者
            {"机械骷髅王", NPC.downedMechBoss3}, //机械骷髅王
            {"世纪之花", NPC.downedPlantBoss}, //世纪之花
            {"石巨人", NPC.downedGolemBoss}, //石巨人
            {"朱鲨", NPC.downedFishron}, // 朱鲨
            {"光女", NPC.downedEmpressOfLight}, //光女
            {"教徒", NPC.downedAncientCultist}, //教徒
            {"月总", NPC.downedMoonlord}, //月总
            {"太阳能柱", NPC.downedTowerSolar}, //太阳能柱
            {"星云柱", NPC.downedTowerNebula}, //星云柱
            {"涡柱", NPC.downedTowerVortex}, //涡柱
            {"星尘柱", NPC.downedTowerStardust}, //星尘柱
        };
        return new RestObject()
        {
            {
                "response",
                 progress
            }
        };
    }

    private static RestObject Error(string code, string message)
        => new(code)
        {
            {
                "error",
                message
            }
        };
}
