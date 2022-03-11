using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryBag_SO", menuName = "Inventory/Bag")]
public class InventoryBag_SO : ScriptableObject
{
    public List<InventoryItem> itemList;
}
