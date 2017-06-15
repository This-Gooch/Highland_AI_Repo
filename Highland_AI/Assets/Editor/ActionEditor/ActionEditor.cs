using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class ActionEditor : EditorWindow {

    Object baseCardSource;
    GameObject editActionCard;
    [SerializeField]Object[] allActionCards;

    Sprite actionImage;

    string actionName;
    string actionDescription;

    int utilityCost;
    int utilityGain;
    int dmgImdOutput;
    int healImdOutput;

    string[] targetDmg = new string[] { "Self", "Target" };
    int targetIndexDmg = 0;

    string[] targetHeal = new string[] { "Self", "Target" };
    int targetIndexHeal = 0;

    bool hasImmidiate = false;
    bool isImdDmg = false;
    bool isImdHeal = false;
    bool hasTurnStart = false;
    bool hasTurnEnd = false;


    [MenuItem("Window/Action Card Editor")]


    public static void ShowWindow()
    {
        ActionEditor window = EditorWindow.GetWindow<ActionEditor>();
        Texture icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/ActionEditor/AEicon.png");
        GUIContent titleContent = new GUIContent("Action Editor", icon);
        window.titleContent = titleContent;
    }

    [SerializeField]string[] testArray = { "Boobs", "Butts", "Helen" };
    Vector2 mainScrollPos = Vector2.zero;
    private void OnGUI()
    {
        mainScrollPos = GUILayout.BeginScrollView(mainScrollPos);

        baseCardSource = AssetDatabase.LoadAssetAtPath<Object>("Assets/Resources/ActionCards/_ActionCardBase.prefab");
        baseCardSource = EditorGUILayout.ObjectField(new GUIContent("Base Action Card", "The root Action Card that the card will be built off of."), baseCardSource, typeof(Object), false, GUILayout.Width(500));
        EditorGUILayout.Space();

        actionImage = (Sprite)EditorGUILayout.ObjectField("Action Image", actionImage, typeof(Sprite), false);

        GUILayout.Label("Action Details", EditorStyles.boldLabel);
        actionName = EditorGUILayout.TextField("Action Name:", actionName, GUILayout.Width(500));
        actionDescription = EditorGUILayout.TextField("Description", actionDescription, GUILayout.Height(100), GUILayout.Width(500));
        EditorGUILayout.Space();

        utilityCost = EditorGUILayout.IntField(new GUIContent("Utility Cost:", "How much Utility does this Action take to activate?"), utilityCost, GUILayout.Width(250));
        utilityGain = EditorGUILayout.IntField(new GUIContent("Utility Gain:", "How much Utility does this Action give when discarded?"), utilityGain, GUILayout.Width(250));
        EditorGUILayout.Space();

        hasImmidiate = EditorGUILayout.BeginToggleGroup(new GUIContent("Has Immidiate Effect?", "Does this Action Card have an immidiate effect when played?"), hasImmidiate);
            isImdDmg = EditorGUILayout.BeginToggleGroup("Does Damage?", isImdDmg);
                targetIndexDmg = EditorGUILayout.Popup("Target or Self?", targetIndexDmg, targetDmg, GUILayout.Width(250));
                dmgImdOutput = EditorGUILayout.IntField("Damage Output:", dmgImdOutput, GUILayout.Width(250));
            EditorGUILayout.EndToggleGroup();
            isImdHeal = EditorGUILayout.BeginToggleGroup("Does Healing?", isImdHeal);
                targetIndexHeal = EditorGUILayout.Popup("Target or Self?", targetIndexHeal, targetHeal, GUILayout.Width(250));
                healImdOutput = EditorGUILayout.IntField("Healing Output", healImdOutput, GUILayout.Width(250));
            EditorGUILayout.EndToggleGroup();
        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.Space();

        if (GUILayout.Button("Build Action Card", GUILayout.Width(250)))
        {
            if (baseCardSource != null)
            {
                if (baseCardSource.name != actionName)
                {
                    if (actionName != "")
                    {
                        CheckExistance();
                    }
                    else { EditorUtility.DisplayDialog("No Name", "Give the Action Card a name", "OK"); }
                }
                else { EditorUtility.DisplayDialog("Bad Name", "Name cannot be ActionCardBase. Please Change.", "OK"); }
            }
            else
            {
                Debug.Log("Need Source Action Card before Action Editor can build.");
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Clear Fields", GUILayout.Width(250)))
        {
            ClearAllFields();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Load Selected Action", GUILayout.Width(250)))
        {
            LoadExistingAction();
        }

        GUILayout.Label("Action Action Cards:", EditorStyles.boldLabel);

        allActionCards = Resources.LoadAll("ActionCards");
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty cardsProperty = so.FindProperty("allActionCards");

        EditorGUILayout.PropertyField(cardsProperty, true, GUILayout.Width(500));

        so.ApplyModifiedProperties();
        
        //End of mainScroll
        GUILayout.EndScrollView();
    }


    bool yesWasClicked = false;
    Object checkTestObject;
    void CheckExistance()
    {
        checkTestObject = null;
        checkTestObject = AssetDatabase.LoadAssetAtPath("Assets/Resources/ActionCards/" + actionName + ".prefab", typeof(Object));
        if (checkTestObject == null)
        {
            CreateActionCard();
        }
        else
        {
            yesWasClicked = EditorUtility.DisplayDialog("That Action Card already exists!", "Do you want to replace the existing action with a new one?", "Yes", "No");
            if(yesWasClicked)
            {
                CreateActionCard();
                yesWasClicked = false;
            }
        }
    }

    void CreateActionCard()
    { 

        GameObject cloneBaseCard = PrefabUtility.CreatePrefab("Assets/Resources/ActionCards/" + actionName + ".prefab", baseCardSource as GameObject) as GameObject;
        cloneBaseCard.transform.GetChild(1).GetComponent<Text>().text = actionName;
        cloneBaseCard.transform.GetChild(4).GetComponent<Text>().text = actionDescription;
        cloneBaseCard.transform.GetChild(0).GetComponent<Image>().sprite = actionImage;
        cloneBaseCard.tag = "ParentAction";

            if (hasImmidiate)
            {
                Action_Immediate actImd = cloneBaseCard.AddComponent(typeof(Action_Immediate)) as Action_Immediate;

            actImd.utilityCost = utilityCost;
            actImd.utilityGain = utilityGain;
                if (isImdDmg)
                {
                    actImd.isDamage = true;
                    if (targetIndexDmg == 1)
                    {
                        actImd.needsTarget = true;
                    }
                    actImd.damageOutput = dmgImdOutput;
                }

                if (isImdHeal)
                {
                    actImd.isHeal = true;
                    if (targetIndexHeal == 1)
                    {
                        actImd.needsTarget = true;
                    }
                    actImd.healingOutput = healImdOutput;
                }
            }
    }

    void LoadExistingAction()
    {
        editActionCard = Selection.activeObject as GameObject;
        if (editActionCard != null)
        {
            if (editActionCard.CompareTag("ParentAction"))
            {
                ClearAllFields();
                actionName = editActionCard.transform.GetChild(1).GetComponent<Text>().text;
                actionDescription = editActionCard.transform.GetChild(4).GetComponent<Text>().text;
                actionImage = editActionCard.transform.GetChild(0).GetComponent<Image>().sprite;
                if (editActionCard.GetComponent<Action_Immediate>() != null)
                {
                    hasImmidiate = true;
                    utilityCost = editActionCard.GetComponent<Action_Immediate>().utilityCost;
                    utilityGain = editActionCard.GetComponent<Action_Immediate>().utilityGain;
                    if (editActionCard.GetComponent<Action_Immediate>().needsTarget == true)
                    {
                        targetIndexDmg = 1;
                        targetIndexHeal = 1;
                    }
                    if (editActionCard.GetComponent<Action_Immediate>().isDamage == true)
                    {
                        isImdDmg = true;
                        dmgImdOutput = editActionCard.GetComponent<Action_Immediate>().damageOutput;
                    }
                    if (editActionCard.GetComponent<Action_Immediate>().isHeal == true)
                    {
                        isImdHeal = true;
                        healImdOutput = editActionCard.GetComponent<Action_Immediate>().healingOutput;
                    }
                    editActionCard = null;
                }
            }
            else { EditorUtility.DisplayDialog("Not a Parent", "Please select a Parent to an Action Card", "OK"); }
            editActionCard = null;
        }
        else { EditorUtility.DisplayDialog("Nothing Selected", "Please select a Parent to an Action Card", "OK"); }
    }

    void ClearAllFields()
    {
        actionName = "";
        actionDescription = "";

        actionImage = null;

        utilityCost = 0;
        utilityGain = 0;
        dmgImdOutput = 0;
        healImdOutput = 0;

        targetIndexDmg = 0;
        targetIndexHeal = 0;

        hasImmidiate = false;
        isImdDmg = false;
        isImdHeal = false;
        hasTurnStart = false;
        hasTurnEnd = false;
    }
}
