using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;

public class ItemEditor : EditorWindow
{
    private ItemDataList_SO dataBase;
    /// <summary>
    /// ��Ʒ�����б�
    /// </summary>
    private List<ItemDetails> itemList = new List<ItemDetails>();
    private VisualTreeAsset itemRowTemplate;
    //�Ҳ���Ϣ
    private ScrollView itemDetailsSection;
    private ListView itemListView;
    private ItemDetails activeItem;
    //Ĭ��ͼƬ
    private Sprite defaultIcon;
    private VisualElement IconPreview;
    [MenuItem("ItemTool/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        //VisualElement label = new Label("Hello World! From C#");
        //root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Build/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);        
        //�õ�ģ������
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Build/ItemRowTemplate.uxml");

        //�õ�Ĭ��IconͼƬ
        defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Res/M Studio/Art/Items/Icons/icon_M.png");

        //������ֵ
        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        itemDetailsSection = root.Q<ScrollView>("ItemDetails");
        IconPreview = itemDetailsSection.Q<VisualElement>("Icon");

        //��ð���
        root.Q<Button>("AddButton").clicked += OnAddItemClicked;
        root.Q<Button>("DeleteButton").clicked += OnDeleteClicked ;

        //��������
        LoadDataBase();

        //����ListView
        GenerateListView();
    }

    private void OnDeleteClicked()
    {
        itemList.Remove(activeItem);
        itemListView.Rebuild();
        itemDetailsSection.visible = false;
    }

    private void OnAddItemClicked()
    {
        ItemDetails newItem =new ItemDetails();
        newItem.itemName = "New Item";
        newItem.itemID = 1000 + itemList.Count+1;
        itemList.Add(newItem);
        itemListView.Rebuild();
    }

    /// <summary>
    /// ���ر���Item�б�SO
    /// </summary>
    private void LoadDataBase()
    {
        var dataArray = AssetDatabase.FindAssets("ItemDataList_SO");

        if (dataArray.Length > 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            dataBase = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDataList_SO)) as ItemDataList_SO;
        }
        itemList=dataBase.itemDetailsList;
        EditorUtility.SetDirty(dataBase);
    }

    /// <summary>
    /// ����List��ͼ
    /// </summary>
    private void GenerateListView()
    {
        //����List��ͼ
        Func<VisualElement> makeItem = () => itemRowTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (e, i) =>
          {
              if (i<itemList.Count)
              {
                  if (itemList[i].itemIcon!=null)                  
                      e.Q<VisualElement>("Icon").style.backgroundImage = itemList[i].itemIcon.texture;
                  e.Q<Label>("Name").text = itemList[i] == null ? "No Item" : itemList[i].itemName;
              }
          };
        itemListView.itemsSource= itemList;
        itemListView.makeItem = makeItem;
        itemListView.bindItem= bindItem;

        itemListView.onSelectionChange += OnListSelectionChange;

        //�Ҳ���岻�ɼ�
        itemDetailsSection.visible = false;
    }

    private void OnListSelectionChange(IEnumerable<object> selectedItem)
    {
        activeItem = (ItemDetails)selectedItem.First();
        GetItemDetails();
        itemDetailsSection.visible = true;
    }

    private void GetItemDetails()
    {
        itemDetailsSection.MarkDirtyRepaint();
        //ID
        itemDetailsSection.Q<IntegerField>("ItemID").value=activeItem.itemID;
        itemDetailsSection.Q<IntegerField>("ItemID").RegisterValueChangedCallback(evt => 
        { 
            activeItem.itemID=evt.newValue;
        });
        //Name
        itemDetailsSection.Q<TextField>("ItemName").value = activeItem.itemName;
        itemDetailsSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt => 
        {
            activeItem.itemName=evt.newValue;
            itemListView.Rebuild();
        });
        //Icon
        IconPreview.style.backgroundImage = activeItem.itemIcon == null ? defaultIcon.texture : activeItem.itemIcon.texture;
        itemDetailsSection.Q<ObjectField>("ItemIcon").value = activeItem.itemIcon;
        itemDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt => 
        {
            Sprite newIcon =evt.newValue as Sprite;
            activeItem.itemIcon = newIcon;
            
            IconPreview.style.backgroundImage = newIcon==null? defaultIcon.texture:newIcon.texture;
            itemListView.Rebuild();
        });
        //�������б����İ�
        itemDetailsSection.Q<ObjectField>("ItemSprite").value = activeItem.itemOnWorldSprite;
        itemDetailsSection.Q<ObjectField>("ItemSprite").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemOnWorldSprite = (Sprite)evt.newValue;
        });

        itemDetailsSection.Q<EnumField>("ItemType").Init(activeItem.itemType);
        itemDetailsSection.Q<EnumField>("ItemType").value = activeItem.itemType;
        itemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemType = (ItemType)evt.newValue;
        });

        itemDetailsSection.Q<TextField>("Description").value = activeItem.itemDescription;
        itemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemDescription = evt.newValue;
        });

        itemDetailsSection.Q<IntegerField>("ItemUseRadius").value = activeItem.itemUseRadius;
        itemDetailsSection.Q<IntegerField>("ItemUseRadius").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemUseRadius = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanPickedup").value = activeItem.canPickedup;
        itemDetailsSection.Q<Toggle>("CanPickedup").RegisterValueChangedCallback(evt =>
        {
            activeItem.canPickedup = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanDropped").value = activeItem.canDropped;
        itemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            activeItem.canDropped = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanCarried").value = activeItem.canCarried;
        itemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            activeItem.canCarried = evt.newValue;
        });

        itemDetailsSection.Q<IntegerField>("Price").value = activeItem.itemPrice;
        itemDetailsSection.Q<IntegerField>("Price").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemPrice = evt.newValue;
        });

        itemDetailsSection.Q<Slider>("SellPercentage").value = activeItem.sellPercentage;
        itemDetailsSection.Q<Slider>("SellPercentage").RegisterValueChangedCallback(evt =>
        {
            activeItem.sellPercentage = evt.newValue;
        });
    }
}