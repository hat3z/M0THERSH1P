#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
    private ItemDatabase itemDatabaseList;

    bool showItems = false;
    private SerializedProperty itemsList;

    bool showShipParts = false;
    private SerializedProperty shipPartsList;

    Color original = new Color();
    Color green = new Color();
    Color red = new Color();
    public void OnEnable()
    {
        itemDatabaseList = target as ItemDatabase;
        itemsList = serializedObject.FindProperty("Items");
        shipPartsList = serializedObject.FindProperty("ShipParts");

        green = Color.green;
        red = Color.red;
        original = GUI.color;
    }

    public override void OnInspectorGUI()
    {
        EditorUtility.SetDirty(itemDatabaseList);
        serializedObject.Update();


        #region ITEMS property
        GUI.backgroundColor = green;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUI.backgroundColor = original;
        showItems = EditorGUILayout.BeginToggleGroup("Show Items List:", showItems);
        if(showItems)
        {

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Items", EditorStyles.boldLabel);
            EditorGUI.indentLevel = 1;
            for (int i = 0; i < itemsList.arraySize; i++)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);

                SerializedProperty item = itemsList.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(item, true);

                //if(itemDatabaseList.FoodDatabase[i].foodModelPath == string.Empty && Selection.activeGameObject != null)
                //{
                //    if (GUILayout.Button("Set Model Path!"))
                //    {
                //        itemDatabaseList.FoodDatabase[i].foodModelPath = Selection.activeGameObject.name;
                //    }
                //}


                GUI.backgroundColor = red;
                if (GUILayout.Button("Delete"))
                {
                    ShowDialogItem(i);
                }
                GUI.backgroundColor = original;
                GUILayout.EndVertical();
                EditorGUILayout.Space();

            }

            GUI.backgroundColor = green;
            if (GUILayout.Button("Add new Item to Database"))
            {
                itemDatabaseList.AddNewItemToDatabase();
            }
            GUI.backgroundColor = original;

            EditorGUILayout.Separator();
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndToggleGroup();
        GUI.backgroundColor = original;
        EditorGUILayout.EndVertical();
        #endregion

        #region SHIP PARTS property
        GUI.backgroundColor = green;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUI.backgroundColor = original;
        showShipParts = EditorGUILayout.BeginToggleGroup("Show Ship Parts List:", showShipParts);
        if (showShipParts)
        {

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Ship Parts:", EditorStyles.boldLabel);
            EditorGUI.indentLevel = 0;
            for (int i = 0; i < shipPartsList.arraySize; i++)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);

                SerializedProperty item = shipPartsList.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(item, true);

                //if(itemDatabaseList.FoodDatabase[i].foodModelPath == string.Empty && Selection.activeGameObject != null)
                //{
                //    if (GUILayout.Button("Set Model Path!"))
                //    {
                //        itemDatabaseList.FoodDatabase[i].foodModelPath = Selection.activeGameObject.name;
                //    }
                //}


                GUI.backgroundColor = red;
                if (GUILayout.Button("Delete"))
                {
                    ShowDialogPromptShipParts(i);
                }
                GUI.backgroundColor = original;
                GUILayout.EndVertical();
                EditorGUILayout.Space();

            }

            GUI.backgroundColor = green;
            if (GUILayout.Button("Add new Ship Part to Database"))
            {
                itemDatabaseList.AddNewShipPartToDatabase();
            }
            GUI.backgroundColor = original;

            EditorGUILayout.Separator();
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndToggleGroup();
        GUI.backgroundColor = original;
        EditorGUILayout.EndVertical();
        #endregion

        serializedObject.ApplyModifiedProperties();
        
    }

    

    void SetModelPath(string _pathToset)
    {
        _pathToset = AssetDatabase.GetAssetPath(Selection.activeObject);
    }

    public void ShowDialogItem(int _itemIndex)
    {
        bool option = EditorUtility.DisplayDialog("Are you sure?", "This will delete the selected item from FoodItems", "Ok", "Cancel");
        if (option)
        {
            itemDatabaseList.Items.RemoveAt(_itemIndex);
            EditorUtility.SetDirty(itemDatabaseList);
        }
    }

    public void ShowDialogPromptShipParts(int _itemIndex)
    {
        bool option = EditorUtility.DisplayDialog("Are you sure?", "This will delete the selected item from Ship Part", "Ok", "Cancel");
        if (option)
        {
            itemDatabaseList.ShipParts.RemoveAt(_itemIndex);
            EditorUtility.SetDirty(itemDatabaseList);
        }
    }

    //public void ShowDialogPromptAppliances(int _itemIndex)
    //{
    //    bool option = EditorUtility.DisplayDialog("Are you sure?", "This will delete the selected item from FoodIngredients", "Ok", "Cancel");
    //    if (option)
    //    {
    //        itemDatabaseList.Appliances.RemoveAt(_itemIndex);
    //        EditorUtility.SetDirty(itemDatabaseList);
    //    }
    //}



}
