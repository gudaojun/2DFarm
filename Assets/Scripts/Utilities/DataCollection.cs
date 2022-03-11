using UnityEngine;

[System.Serializable]
//��Ʒ��Ϣ��
public class ItemDetails
{
    public int itemID;
    public string itemName;

    public ItemType itemType;
    public Sprite itemIcon;
    public Sprite iteamOnWroldSprite;
    public string itemDescription;

    public int itemUseRadius;

    //�Ƿ��ܱ�ʰȡ
    public bool canPickedup;

    public bool canDropped;

    public bool canCarried;
    //�۸�
    public int itemPrice;
    //����ʱ���۱���
    [Range(0,1)]
    public float sellPercentage;
}