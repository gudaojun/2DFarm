using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public ItemToolTip itemToolTip;

        [SerializeField]private SlotUI[] playerSlot;
        [SerializeField] private GameObject bagUI;
        private bool bagOpen;
        public Image dragItem;
        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        }

        

        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
            
        }
        private void Start()
        {
            for (int i = 0; i < playerSlot.Length; i++)
            {
                playerSlot[i].slotIndex = i;
            }
            bagOpen = bagUI.activeInHierarchy;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                OpenBagUI();
            }
        }
        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.Player:
                    for (int i = 0; i < playerSlot.Length; i++)
                    {
                        if (list[i].itemAmount>0)
                        {
                            var item=InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlot[i].UpdateSlot(item, list[i].itemAmount);
                        }
                        else
                        {
                            playerSlot[i].UpdateEmptySlot();
                        }
                    }
                    break;
            }
        }

        public void OpenBagUI()
        {
            bagOpen = !bagOpen;
            bagUI.SetActive(bagOpen);
        }

        /// <summary>
        /// 更新slot高亮显示
        /// </summary>
        /// <param name="index"></param>
        public void UpdateSlotHigltLight(int index)
        {
            for (int i = 0; i < playerSlot.Length; i++)
            {
                var slot=playerSlot[i];
                if (slot.isSelected&&index== slot.slotIndex)
                {
                    slot.slotHigltLight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected=false;
                    slot.slotHigltLight.gameObject.SetActive(false);
                }
            }
        }
    }
}