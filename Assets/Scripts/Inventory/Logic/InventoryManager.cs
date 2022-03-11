using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDataList_SO itemDataList_SO;
        [Header("背包数据")]
        public InventoryBag_SO PlayerBag;
        /// <summary>
        /// 通过ID获取物品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDetailsList.Find(i => i.itemID == ID);
        }

        /// <summary>
        /// 添加物品到Player背包
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestroy"></param>
        public void AddItem(Item item ,bool toDestroy=true)
        {
            //是否已经有这个物品
            var index =GetItemIndexInBag(item.itemID);
            AddItemAtIndex(item
                .itemID, index, 1);
            
            if (toDestroy)
            {
                Destroy(item.gameObject);
            }
        }

        /// <summary>
        /// 检查背包是否有空位
        /// </summary>
        /// <returns></returns>
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < PlayerBag.itemList.Count; i++)
            {
                if (PlayerBag.itemList[i].itemID > 0) ;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 通过物品ID找到背包已有物品
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>-1则没有这个物品否则返回序号</returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < PlayerBag.itemList.Count; i++)
            {
                if (PlayerBag.itemList[i].itemID==ID)
                {
                    return i;
                }
            }
                return -1;
        }

        /// <summary>
        /// 在指定背包序号位置添加物品
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        private void AddItemAtIndex(int ID, int index ,int amount)
        {
            if (index==-1&&CheckBagCapacity()) //背包中没有这个物品
            {
                var item =new InventoryItem {itemID = ID,itemAmount=amount};
                for (int i = 0; i < PlayerBag.itemList.Count; i++)
                {
                    if (PlayerBag.itemList[i].itemID == 0)
                    {
                        PlayerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            else //背包中有这个物品
            {
                int currentAmount = PlayerBag.itemList[index].itemAmount + amount;
                var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
                PlayerBag.itemList[index] = item;
            }
        }
    }
}