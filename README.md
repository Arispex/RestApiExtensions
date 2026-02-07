# RestApiExtensions

一个用于 Terraria TShock 服务端的 REST API 扩展插件，提供：

- 玩家背包查询接口
- Boss 进度查询接口

当前插件版本：`2.0.0`  
目标框架：`.NET 9`


## 快速开始

1.	编译插件或在 Release 下载
2.	将 `RestApiExtensions.dll` 放入服务端 `ServerPlugins/`
3.	重启服务端
4.	给调用接口的账号授予权限：
•	rae.player.inventory
•	rae.world.progress


## 接口文档

### 1. 查询玩家背包

- 路径：`/plugins/restapiextensions/v2/player/inventory`
- 权限：`rae.player.inventory`
- 参数：`player`（必填，玩家名称）

示例：

```text
GET /plugins/restapiextensions/v2/player/inventory?player=Alice&token=你的Token
```

成功返回示例：

```json
{
  "status": "200",
  "response": [
    {
      "netID": 0,
      "prefix": 0,
      "stack": 0
    }
  ]
}
```

失败返回示例：

```json
{
  "status": "400",
  "error": "缺少必要参数 player"
}
```

```json
{
  "status": "400",
  "error": "玩家未找到"
}
```

### 2. 查询世界进度

- 路径：`/plugins/restapiextensions/v2/world/progress`
- 权限：`rae.world.progress`
- 参数：无

示例：

```text
GET /plugins/restapiextensions/v2/world/progress?token=你的Token
```

返回示例：

```json
{
  "status": "200",
  "response": {
    "史莱姆王": true,
    "克苏鲁之眼": true,
    "世界吞噬者 或 克苏鲁之脑": false,
    "蜂后": true,
    "骷髅王": true,
    "巨鹿": false,
    "肉山": true,
    "史莱姆皇后": false,
    "双子魔眼": false,
    "毁灭者": false,
    "机械骷髅王": false,
    "世纪之花": false,
    "石巨人": false,
    "朱鲨": false,
    "光女": false,
    "教徒": false,
    "月总": false,
    "太阳能柱": false,
    "星云柱": false,
    "涡柱": false,
    "星尘柱": false
  }
}
```


## 编译

在仓库根目录执行：

```bash
dotnet restore

dotnet build RestApiExtensions.sln
```
