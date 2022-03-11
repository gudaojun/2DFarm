using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;

public class ItemEditor : EditorWindow
{
    private ItemDataList_SO dataBase;
    /// <summary>
    /// 物品详情列表
    /// </summary>
    private List<ItemDetails> itemList = new List<ItemDetails>();
    private VisualTreeAsset itemRowTemplate;
    private ListView itemListView;
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

        //拿到模板数据
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Build/ItemRowTemplate.uxml");

        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        //加载数据
        LoadDataBase();

        //生成ListView
        GenerateListView();
    }
    /// <summary>
    /// 加载本地Item列表SO
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
    /// 生成List视图
    /// </summary>
    private void GenerateListView()
    {
        //创建List视图
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
    }
}