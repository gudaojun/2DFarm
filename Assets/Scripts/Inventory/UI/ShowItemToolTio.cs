using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Inventory {
    [RequireComponent(typeof(SlotUI))]
    public class ShowItemToolTio : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        private SlotUI slotUI;
        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        private void Awake()
        {
            slotUI = GetComponent<SlotUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (slotUI.itemAmount!=0)
            {
                inventoryUI.itemToolTip.gameObject.SetActive(true);
                inventoryUI.itemToolTip.SetUpToolTip(slotUI.itemDetails, slotUI.slotType);
                inventoryUI.itemToolTip.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
                inventoryUI.itemToolTip.transform.position = transform.position + Vector3.up * 30;
            }
            else
                inventoryUI.itemToolTip.gameObject.SetActive(false);

        }

        public void OnPointerExit(PointerEventData eventData)
        {            
                inventoryUI.itemToolTip.gameObject.SetActive(false);
        }
    }
}