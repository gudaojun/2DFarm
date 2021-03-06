using UnityEngine;

[System.Serializable]
//物品信息类
public class ItemDetails
{
    public int itemID;
    public string itemName;

    public ItemType itemType;
    public Sprite itemIcon;
    public Sprite itemOnWorldSprite;
    public string itemDescription;

    public int itemUseRadius;

    //是否能被拾取
    public bool canPickedup;

    public bool canDropped;

    public bool canCarried;
    //价格
    public int itemPrice;
    //出售时打折比率
    [Range(0,1)]
    public float sellPercentage;
}

[System.Serializable]
public struct InventoryItem
{
    public int itemID;
    public int itemAmount;
}

[System.Serializable]
public class AnimatorType
{
    public PartType partType;
    public PartName partName;
    public AnimatorOverrideController overrideController;
}