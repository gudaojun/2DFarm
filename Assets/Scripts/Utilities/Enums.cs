public enum ItemType
{
    /// <summary>
    /// 种子
    /// </summary>
    Seed,
    /// <summary>
    /// 商品
    /// </summary>
    Commodity,
    /// <summary>
    /// 家具
    /// </summary>
    Furniture,
    /// <summary>
    /// 锄头
    /// </summary>
    HoeTool,
    /// <summary>
    /// 砍树工具
    /// </summary>
    ChopTool,
    /// <summary>
    /// 砸石头工具
    /// </summary>
    BreakTool,
    /// <summary>
    /// 割草工具
    /// </summary>
    ReapTool,
    /// <summary>
    /// 浇水工具
    /// </summary>
    WaterTool,
    /// <summary>
    /// 菜篮子
    /// </summary>
    CollectTool,
    /// <summary>
    /// 杂草（可以被收割）
    /// </summary>
    ReapableScenery
}

public enum SlotType
{
    Bag,Box,Shop
}

public enum InventoryLocation
{
    Player,BOx
}

public enum PartType 
{
    None,Carry,Hoe,Break
}
public enum PartName
{
    Hair,Body,Arm, Tool
}