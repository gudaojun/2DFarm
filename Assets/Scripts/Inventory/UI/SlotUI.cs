using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
namespace Inventory
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("������ȡ")]
        [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI amountText;
        public Image slotHigltLight;
        [SerializeField] private Button button;

        [Header("��������")]
        [SerializeField] public SlotType slotType;

        public bool isSelected;
        public int slotIndex;
        public ItemDetails itemDetails;
        public int itemAmount;

        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();
        private void Start()
        {
            isSelected = false;
            if (itemDetails.itemID == 0)
            {
                UpdateEmptySlot();
            }
        }
        /// <summary>
        /// ���¸��Ӻ���Ϣ
        /// </summary>
        /// <param name="item">������Ϣ</param>
        /// <param name="amount">����</param>
        public void UpdateSlot(ItemDetails item, int amount)
        {
            itemDetails = item;
            slotImage.enabled = true;
            slotImage.sprite = item.itemIcon;
            itemAmount = amount;
            amountText.text = amount.ToString();
            button.interactable = true;
        }
        /// <summary>
        /// ����slotΪ��
        /// </summary>
        public void UpdateEmptySlot()
        {
            if (isSelected)
            {
                isSelected = false;
            }
            slotImage.enabled = false;
            amountText.text = string.Empty;
            button.interactable = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemAmount == 0) return;
            isSelected = !isSelected;

            inventoryUI.UpdateSlotHigltLight(slotIndex);

            if (slotType==SlotType.Bag)
            {
                //֪ͨ��Ʒ��ѡ��״̬
                EventHandler.CallItemSelectedEvent(itemDetails, isSelected);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemAmount != 0)
            {
                inventoryUI.dragItem.enabled = true;
                inventoryUI.dragItem.sprite = slotImage.sprite;
                inventoryUI.dragItem.SetNativeSize();

                isSelected = true;
                inventoryUI.UpdateSlotHigltLight(slotIndex);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.dragItem.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.dragItem.enabled = false;
            //  Debug.Log(eventData.pointerCurrentRaycast.gameObject);
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
                    return;
                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                int targetIndex = targetSlot.slotIndex;

                if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
                {
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                }
                inventoryUI.UpdateSlotHigltLight(-1);
            }
            //else //�Ե�����
            //{
            //    if (itemDetails.canDropped)
            //    {
            //        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            //        EventHandler.CallInstantiateItemInScene(itemDetails.itemID, pos);
            //    }
            //}
        }
    }
}