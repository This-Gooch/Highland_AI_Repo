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
        //CardList cl = new CardList("main");
        
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

                Libraries.instance.Save_Card_Local(act);
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

                Libraries.instance.Save_Card_Local(min);
                Libraries.instance.Save_Cards_To_File();
                break;
            case 2:
                Passive pas = new Passive();
<<<<<<< HEAD
                pas.id = NSGameplay.Cards.ECardKeys.TestCardThree;
=======
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
>>>>>>> 167b132f4f237d1bb4a8320eda5799aa2f99b0ab

                Libraries.instance.Save_Card_Local(pas);
                Libraries.instance.Save_Cards_To_File();
                break;
        }

    }
   
}
