using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("��Ʒ����")]
        public ItemDataList_SO itemDataList_SO;
        [Header("��������")]
        public InventoryBag_SO PlayerBag;
        /// <summary>
        /// ͨ��ID��ȡ��Ʒ��Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDetailsList.Find(i => i.itemID == ID);
        }

        /// <summary>
        /// �����Ʒ��Player����
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestroy"></param>
        public void AddItem(Item item ,bool toDestroy=true)
        {
            //�Ƿ��Ѿ��������Ʒ
            var index =GetItemIndexInBag(item.itemID);
            AddItemAtIndex(item
                .itemID, index, 1);
            
            if (toDestroy)
            {
                Destroy(item.gameObject);
            }
        }

        /// <summary>
        /// ��鱳���Ƿ��п�λ
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
        /// ͨ����ƷID�ҵ�����������Ʒ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>-1��û�������Ʒ���򷵻����</returns>
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
        /// ��ָ���������λ�������Ʒ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        private void AddItemAtIndex(int ID, int index ,int amount)
        {
            if (index==-1&&CheckBagCapacity()) //������û�������Ʒ
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
            else //�������������Ʒ
            {
                int currentAmount = PlayerBag.itemList[index].itemAmount + amount;
                var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
                PlayerBag.itemList[index] = item;
            }
        }
    }
}