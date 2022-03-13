using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemToolTip : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI nameText;
    [SerializeField]private TextMeshProUGUI typeText;
    [SerializeField]private TextMeshProUGUI descriptionText;
    [SerializeField]private TextMeshProUGUI valueText;
    [SerializeField] private GameObject bottomPart;

    public void SetUpToolTip(ItemDetails itemDetails,SlotType slotType)
    {
        nameText.text = itemDetails.itemName;
        typeText.text = GetItemType(itemDetails.itemType);
        descriptionText.text=itemDetails.itemDescription.ToString();

        if (itemDetails.itemType==ItemType.Seed||itemDetails.itemType==ItemType.Commodity||itemDetails.itemType==ItemType.Furniture)
        {
            bottomPart.SetActive(true);
            var pice = itemDetails.itemPrice;
            if (slotType==SlotType.Bag)
            {
                pice = (int)(pice * itemDetails.sellPercentage);
            }
            valueText.text = pice.ToString();
        }
        else
        {
            bottomPart.SetActive(false);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    private string GetItemType(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.Seed => "����",
            ItemType.Commodity => "��Ʒ",
            ItemType.Furniture => "�Ҿ�",
            ItemType.BreakTool => "����",
            ItemType.ChopTool => "����",
            ItemType.CollectTool => "����",
            ItemType.HoeTool => "����",
            ItemType.ReapTool => "����",
            ItemType.WaterTool => "����",
            _ => "��"
        };
    }
}
