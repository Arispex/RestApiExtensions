using System.Reflection;
using Rests;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace RestApiExtensions;

[ApiVersion(2, 1)]
public sealed class RestApiExtensionsPlugin(Main game) : TerrariaPlugin(game)
{
    public override string Author => "千亦";

    public override string Description => "REST API Extensions";

    public override string Name => "REST API Extensions";

    public override Version Version => new(2, 0, 0);

    public override void Initialize()
    {
        TShock.RestApi.Register(new SecureRestCommand($"/v2/users/inventory", Inventory, "rae.users.inventory"));
        TShock.RestApi.Register(new SecureRestCommand($"/v2/world/progress", Progress, "rae.world.progress"));
    }

    private static object Inventory(RestRequestArgs args)
    {
        var user = args.Request?.Parameters?["user"];
        if (string.IsNullOrWhiteSpace(user))
        {
            return Error("400", "缺少必要参数 user");
        }

        if (!UserInventoryService.TryGetInventory(user, out var inventory, out var error))
        {
            return Error("400", error ?? "用户未找到");
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
            {"史莱姆王", NPC.downedSlimeKing},
            {"克苏鲁之眼", NPC.downedBoss1},
            {"世界吞噬者 或 克苏鲁之脑", NPC.downedBoss2},
            {"蜂王", NPC.downedQueenBee},
            {"骷髅王", NPC.downedBoss3},
            {"独眼巨鹿", NPC.downedDeerclops},
            {"血肉墙", Main.hardMode},
            {"史莱姆皇后", NPC.downedQueenSlime},
            {"双子魔眼", NPC.downedMechBoss2},
            {"毁灭者", NPC.downedMechBoss1},
            {"机械骷髅王", NPC.downedMechBoss3},
            {"世纪之花", NPC.downedPlantBoss},
            {"石巨人", NPC.downedGolemBoss},
            {"猪龙鱼公爵", NPC.downedFishron},
            {"光之女皇", NPC.downedEmpressOfLight},
            {"拜月教邪教徒", NPC.downedAncientCultist},
            {"日耀柱", NPC.downedTowerSolar},
            {"星云柱", NPC.downedTowerNebula},
            {"星旋柱", NPC.downedTowerVortex},
            {"星尘柱", NPC.downedTowerStardust},
            {"月亮领主", NPC.downedMoonlord},
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
