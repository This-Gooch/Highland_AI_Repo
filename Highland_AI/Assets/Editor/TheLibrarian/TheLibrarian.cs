using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class TheLibrarian : EditorWindow {

    Object baseCardSource;
    GameObject editActionCard;
    [SerializeField]Object[] allActionCards;

    Sprite actionImage;

    string cardName;
    string cardDescription;
    string cardDescTitle;

    int cardTypeInt = 0;
    string[] typeOptions = new string[] { "Action", "Minion", "Passive" };

    int utilityCost;
    int utilityGain;

    int damageOut;
    int damageIn;
    int healOut;
    int healIn;
    int armorUpOut;
    int armorUpIn;
    int armorDownOut;
    int armorDownIn;

    int minionAttack;
    int minionBaseDefense;
    int minionHealth;

    int passiveTurnCount;
    bool playerTurnStart;
    bool playerTurnEnd;
    bool enemyTurnStart;
    bool enemyTurnEnd;

    string[] targetDmg = new string[] { "Self", "Target" };
    int targetIndexDmg = 0;

    string[] targetHeal = new string[] { "Self", "Target" };
    int targetIndexHeal = 0;

    bool hasImmidiate = false;
    bool isImdDmg = false;
    bool isImdHeal = false;
    bool hasTurnStart = false;
    bool hasTurnEnd = false;


    [MenuItem("Window/The Librarian")]


    public static void ShowWindow()
    {
        TheLibrarian window = EditorWindow.GetWindow<TheLibrarian>();
        Texture icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/TheLibrarian/AEicon.png");
        GUIContent titleContent = new GUIContent("The Librarian", icon);
        window.titleContent = titleContent;
    }

    Vector2 mainScrollPos = Vector2.zero;
    private void OnGUI()
    {
        mainScrollPos = GUILayout.BeginScrollView(mainScrollPos);

        GUILayout.Label("Card Details", EditorStyles.boldLabel);
        cardName = EditorGUILayout.TextField("Card Name:", cardName, GUILayout.Width(500));
        cardDescTitle = EditorGUILayout.TextField("Tooltip Title:", cardDescTitle, GUILayout.Width(500));
        cardDescription = EditorGUILayout.TextField("Description", cardDescription, GUILayout.Height(100), GUILayout.Width(500));
        EditorStyles.textField.wordWrap = true;
        EditorGUILayout.Space();

        utilityCost = EditorGUILayout.IntField(new GUIContent("Utility Cost:", "How much Utility does this Action take to activate?"), utilityCost, GUILayout.Width(250));
        //utilityGain = EditorGUILayout.IntField(new GUIContent("Utility Gain:", "How much Utility does this Action give when discarded?"), utilityGain, GUILayout.Width(250));
        EditorGUILayout.Space();

        cardTypeInt = EditorGUILayout.Popup(cardTypeInt, typeOptions, GUILayout.Width(250));

        if (cardTypeInt == 0)
        {
            damageOut = EditorGUILayout.IntField(new GUIContent("Damage Out: ", "How much damage does this card do?"), damageOut, GUILayout.Width(250));
            damageIn = EditorGUILayout.IntField(new GUIContent("Damage In: ", "How much damage does this card do to the user?"), damageIn, GUILayout.Width(250));

            healOut = EditorGUILayout.IntField(new GUIContent("Healing Out: ", "How much healing does this card do?"), healOut, GUILayout.Width(250));
            healIn = EditorGUILayout.IntField(new GUIContent("Healing In: ", "How much healing does this card do to the user?"), healIn, GUILayout.Width(250));

            armorUpOut = EditorGUILayout.IntField(new GUIContent("Armor Up Out: ", "How much armour does this card give?"), armorUpOut, GUILayout.Width(250));
            armorUpIn = EditorGUILayout.IntField(new GUIContent("Armor Up In: ", "How much armor does this card give to the user?"), armorUpIn, GUILayout.Width(250));
            armorDownOut = EditorGUILayout.IntField(new GUIContent("Armor Down Out: ", "How much armour does this card damage?"), armorDownOut, GUILayout.Width(250));
            armorDownIn = EditorGUILayout.IntField(new GUIContent("Armor Down In: ", "How much armor does this card damage to the user?"), armorDownIn, GUILayout.Width(250));
        }

        if(cardTypeInt == 1)
        {
            minionAttack = EditorGUILayout.IntField(new GUIContent("Minion Attack: ", "How much attack does the minion have?"), minionAttack, GUILayout.Width(250));
            minionBaseDefense = EditorGUILayout.IntField(new GUIContent("Minion Base Defense: ", "How much base defense does the minion have?"), minionBaseDefense, GUILayout.Width(250));
            minionHealth = EditorGUILayout.IntField(new GUIContent("Minion Health: ", "How much health does the minion have?"), minionHealth, GUILayout.Width(250));
        }

        if(cardTypeInt == 2)
        {
            passiveTurnCount = EditorGUILayout.IntField(new GUIContent("Turn Count: ", "How many turns does this card last for?"), passiveTurnCount, GUILayout.Width(250));
            playerTurnStart = EditorGUILayout.Toggle(new GUIContent("Trigger Player Turn Start: ", "Does this effect trigger at player Turn Start?"), playerTurnStart, GUILayout.Width(250));
            playerTurnEnd = EditorGUILayout.Toggle(new GUIContent("Trigger Player Turn End: ", "Does this effect trigger at player Turn End?"), playerTurnEnd, GUILayout.Width(250));
            enemyTurnStart = EditorGUILayout.Toggle(new GUIContent("Trigger Enemy Turn Start: ", "Does this effect trigger at enemy Turn Start?"), enemyTurnStart, GUILayout.Width(250));
            enemyTurnEnd = EditorGUILayout.Toggle(new GUIContent("Trigger Enemy Turn End: ", "Does this effect trigger at enemy Turn End?"), enemyTurnEnd, GUILayout.Width(250));

            damageOut = EditorGUILayout.IntField(new GUIContent("Damage Out: ", "How much damage does this card do?"), damageOut, GUILayout.Width(250));
            damageIn = EditorGUILayout.IntField(new GUIContent("Damage In: ", "How much damage does this card do to the user?"), damageIn, GUILayout.Width(250));

            healOut = EditorGUILayout.IntField(new GUIContent("Healing Out: ", "How much healing does this card do?"), healOut, GUILayout.Width(250));
            healIn = EditorGUILayout.IntField(new GUIContent("Healing In: ", "How much healing does this card do to the user?"), healIn, GUILayout.Width(250));

            armorUpOut = EditorGUILayout.IntField(new GUIContent("Armor Up Out: ", "How much armour does this card give?"), armorUpOut, GUILayout.Width(250));
            armorUpIn = EditorGUILayout.IntField(new GUIContent("Armor Up In: ", "How much armor does this card give to the user?"), armorUpIn, GUILayout.Width(250));
            armorDownOut = EditorGUILayout.IntField(new GUIContent("Armor Down Out: ", "How much armour does this card damage?"), armorDownOut, GUILayout.Width(250));
            armorDownIn = EditorGUILayout.IntField(new GUIContent("Armor Down In: ", "How much armor does this card damage to the user?"), armorDownIn, GUILayout.Width(250));
        }



        /*
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
                if (baseCardSource.name != cardName)
                {
                    if (cardName != "")
                    {
                       // CheckExistance();
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
            //ClearAllFields();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Load Selected Action", GUILayout.Width(250)))
        {
            //LoadExistingAction();
        }

        GUILayout.Label("Action Action Cards:", EditorStyles.boldLabel);

        allActionCards = Resources.LoadAll("ActionCards");
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty cardsProperty = so.FindProperty("allActionCards");

        EditorGUILayout.PropertyField(cardsProperty, true, GUILayout.Width(500));

        so.ApplyModifiedProperties();
        */
        if (GUILayout.Button("Create Card", GUILayout.Width(250)))
        {
            CreatCard();
        }


            //End of mainScroll
            GUILayout.EndScrollView();
    }

    void CreatCard()
    {
        XMLDataSerializer.LoadCards("Assets/Data/Cards.xml");

        switch (cardTypeInt)
        {
            case 0:
                Action act = new Action();
                act.id = "Test card One";
                act.type = NSGameplay.Cards.ECardType.Instant;
                act.cost = utilityCost;
                act.name = cardName;
                act.tooltip = new NSGameplay.Cards.Tooltip();
                act.tooltip.description = cardDescription;
                act.tooltip.title = cardDescTitle;
                act.tooltip.image_Path = "path of an image";
                act.damageOut = damageOut;
                act.damageIn = damageIn;
                act.healOut = healOut;
                act.healIn = healIn;
                act.armorUpIn = armorUpIn;
                act.armorUpOut = armorUpOut;
                act.armorDownIn = armorDownIn;
                act.armorDownOut = armorDownOut;

                //Saves the card into the library
                Libraries.instance.Save_Card_Local(act);
                //Saves the entire library to file
                Libraries.instance.Save_Cards_To_File();
                break;
            case 1:
                Minion min = new Minion();
                min.id = "Test Card two";
                min.type = NSGameplay.Cards.ECardType.Minion;
                min.cost = 2;
                min.name = cardName;
                min.tooltip = new NSGameplay.Cards.Tooltip();
                min.tooltip.description = cardDescription;
                min.tooltip.title = cardDescTitle;
                min.tooltip.image_Path = "path of an image";
                min.attack = minionAttack;
                min.baseDefence = minionBaseDefense;
                min.defence = minionBaseDefense;
                min.health = minionHealth;

                //Saves the card into the library
                Libraries.instance.Save_Card_Local(min);
                //Saves the entire library to file
                Libraries.instance.Save_Cards_To_File();
                break;
            case 2:
                Passive pas = new Passive();
                pas.id ="Test Card three";
                break;
        }

    }
    /*
    bool yesWasClicked = false;
    Object checkTestObject;
    void CheckExistance()
    {
        checkTestObject = null;
        checkTestObject = AssetDatabase.LoadAssetAtPath("Assets/Resources/ActionCards/" + cardName + ".prefab", typeof(Object));
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
    */
}
