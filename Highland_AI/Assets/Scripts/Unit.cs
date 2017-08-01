using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using NSGameplay;
using System;
using UnityEngine.EventSystems;



//XML root name in the file.
[XmlRoot("UnitList")]
// include type class Unit
[XmlInclude(typeof(UnitInfo))] 
//Class holding a list of units. Used for storing and loading purposes.
public class UnitList       
{

    [XmlElement("Listname")]
    public string Listname { get; set; }

    [XmlArray("List"), XmlArrayItem(ElementName = "Unit", Type = typeof(UnitInfo))]
    public List<UnitInfo> unitList = new List<UnitInfo>();


    // Constructor
    public UnitList() { }

    public UnitList(string name)
    {
        this.Listname = name;
    }

    public void Add(UnitInfo unit)
    {
        unitList.Add(unit);
    }
}
[System.Serializable]
[XmlRoot("Unit")]
public class UnitInfo
{
    //Default constructor
    public UnitInfo() { }
    //Main constructor
    public UnitInfo(int health, int defence, int attack, int utility, string name)
    {
        this.baseHealth = health;
        this.attack = attack;
        this.baseDefence = defence;
        this.defence = defence;
        this.utility = utility;
        this.name = name;
        this.exhausted = false;
        this.fieldPosition = 0;
    }
    /// <summary>
    /// Name is both the card's displayed Name AND id.
    /// </summary>
    public string name { get; set; }
    public int baseHealth { get; set; }
    public int health { get; set; }
    /// <summary>
    /// Current armor
    /// </summary>
    public int defence { get; set; }
    /// <summary>
    /// Level of armor to reset the unit to at then start of his turn.
    /// </summary>
    public int baseDefence { get; set; }
    public int attack { get; set; }
    /// <summary>
    /// Utility is gained by discarding cards and servers has mana.
    /// </summary>
    public int utility { get; set; }
    /// <summary>
    /// Level is used to determine what skills a unit can use.
    /// This is gained everytime a unit goes through exhaust. (Card could also maybe used.)
    /// </summary>
    public int level { get; set; }
    /// <summary>
    /// The unit Is unable to draw or play any skills/cards this turn.
    /// </summary>
    public bool exhausted { get; set; }
    public int owningPlayer { get; set; }
    /// <summary>
    /// The field order that this unit starts at.
    /// </summary>
    public int fieldPosition { get; set; }
    
    public string portraitPath { get; set; }
}
public class Unit : MonoBehaviour, ITargetable, IComparable<Unit>
{

    #region Editor references
    
    [SerializeField]
    int _Player;

    /// <summary>
    /// The targeting start/end location for this unit.
    /// </summary>
    [SerializeField]
    private Transform m_TargetingLocation;

    #endregion

    #region Public members
    public UnitInfo info;
    //Card in hand for the unit.
    public List<Card> m_Hand = new List<Card>();
    public Deck m_Deck;

    public bool m_IsMouseOver = false;
    public bool m_IsSelected = false;
    public bool m_UsingAbility = false;

    //For debug
    public LayerMask mask;

    #endregion

    #region Private members
    
    /// <summary>
    /// Abilities
    /// </summary>
    private Ability m_AbilityOne;
    private Ability m_AbilityTwo;
    private Ability m_AbilitySpecial;
    private Ability m_AbilityUltimate;

    

    #endregion

    #region Monobehaviour Functions
    private void Awake()
    {
        Initialize();
        Register();
    }

    private void Start()
    {
        InitializeTransform();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Select") && !EventSystem.current.IsPointerOverGameObject())
        {
            if (!m_IsMouseOver && m_IsSelected) 
            {
                Debug.Log("Deselecting On click");
                Deselect();
            }
            else if (m_IsMouseOver && !m_IsSelected)
            {
                if (TargetingTracer.instance.targeterEntity != null && (object)TargetingTracer.instance.targeterEntity != this)
                {
                    Debug.Log("This unit is getting targeted : " + gameObject.name);
                }
                else
                {
                    Debug.Log("Selecting : " + gameObject.name);
                    Select();
                }
                
            }
        }

        if (Input.GetButtonDown("Deselect") && m_IsSelected)
        {
            Deselect();
        }
    }

    private void FixedUpdate()
    {

    }

    #endregion

    #region Player called Methods
    /// <summary>
    /// At lvl 1
    /// </summary>
    public void SelectAbilityOne()
    {
        Debug.Log("Selecting Ability One");
        if (m_AbilityOne.targetable)//The ability can be targeted.
        {
            m_IsSelected = true;
            m_UsingAbility = true;
            TargetingTracer.instance.Close();
            TargetingTracer.instance.SetOrigin(m_TargetingLocation, m_AbilityOne.targetableMask, m_AbilityOne, this);
        }
        else//The ability cannot be targeted.
        {
            UseAbilityOne(GetValidTargets(m_AbilityOne.targetLayer));
        }
       
    }
    private void UseAbilityOne(ITargetable[] target)
    {
        Debug.Log("Using Ability One");
        info.utility -= m_AbilityOne.Use(info.utility, target);
        ReferenceHolder.instance.UnitUI.Refresh(this);
    }
    /// <summary>
    /// At lvl 2
    /// </summary>
    public void SelectAbilityTwo()
    {
        Debug.Log("Selecting Ability Two");
        if (m_AbilityTwo.targetable)//The ability can be targeted.
        {
            m_IsSelected = true;
            m_UsingAbility = true;
            TargetingTracer.instance.Close();
            TargetingTracer.instance.SetOrigin(m_TargetingLocation, m_AbilityTwo.targetableMask, m_AbilityTwo, this);
        }
        else//The ability cannot be targeted.
        {
            UseAbilityTwo(GetValidTargets(m_AbilityTwo.targetLayer));
        }
    }
    private void UseAbilityTwo(ITargetable[] target)
    {
        Debug.Log("Using Ability One");
        info.utility -= m_AbilityTwo.Use(info.utility, target);
        ReferenceHolder.instance.UnitUI.Refresh(this);
    }
    /// <summary>
    /// At lvl 3
    /// </summary>
    public void SelectAbilitySpecial()
    {
        Debug.Log("Selecting Special Ability");
       
        if (m_AbilitySpecial.targetable)//The ability can be targeted.
        {
            m_IsSelected = true;
            m_UsingAbility = true;
            TargetingTracer.instance.Close();
            TargetingTracer.instance.SetOrigin(m_TargetingLocation, m_AbilitySpecial.targetableMask, m_AbilitySpecial, this);
        }
        else//The ability cannot be targeted.
        {
            UseSpecial(GetValidTargets(m_AbilitySpecial.targetLayer));
        }
    }
    private void UseSpecial(ITargetable[] target)
    {
        Debug.Log("Using special Ability ");
        info.utility -= m_AbilitySpecial.Use(info.utility, target);
        ReferenceHolder.instance.UnitUI.Refresh(this);
    }
    /// <summary>
    /// At lvl 4
    /// </summary>
    public void SelectAbilityUltimate()
    {
        Debug.Log("Selecting ultimate Ability");
        if (m_AbilityUltimate.targetable)//The ability can be targeted.
        {
            m_IsSelected = true;
            m_UsingAbility = true;
            TargetingTracer.instance.Close();
            TargetingTracer.instance.SetOrigin(m_TargetingLocation, m_AbilityUltimate.targetableMask, m_AbilityUltimate, this);
        }
        else//The ability cannot be targeted.
        {
            UseUltimate(GetValidTargets(m_AbilityUltimate.targetLayer));
        }
    }
    private void UseUltimate(ITargetable[] target)
    {
        Debug.Log("Using ultimate ability");
        info.utility -= m_AbilityUltimate.Use(info.utility, target);
        ReferenceHolder.instance.UnitUI.Refresh(this);
    }
   
    #endregion

    #region Initialization
    //Init on start.
    private void Initialize()
    {
        /*Debug init. Real value should come from file*/
        //TODO: Load unit data from file
        info.health = 100;
        info.attack = 10;
        info.baseDefence = 5;
        info.utility = 10;
        info.name = "the_name_of_the_hero";
        info.portraitPath = "healer";
        info.fieldPosition = int.Parse(gameObject.name);

        Effect[] effects1 = new Effect[3];
        effects1[1] = new Effect(EEffect.attack, 5);
        effects1[0] = new Effect(EEffect.modify_armor, -2);
        effects1[2] = new Effect(EEffect.modify_health, -2);

        

        m_AbilityOne = new Ability(effects1, 1, 5, 1, true, mask);

        Effect[] effects2 = new Effect[1];
        effects2[0] = new Effect(EEffect.modify_health, -15);

        m_AbilityTwo = new Ability(effects2, 2, 5, 1, false, mask, TargetLayer.all_ennemies);
        Effect[] effects3 = new Effect[1];
        effects3[0] = new Effect(EEffect.modify_health, 10);
        m_AbilitySpecial = new Ability(effects3, 3, 5, 1, false, mask, TargetLayer.adjacent);
        Effect[] effects4 = new Effect[1];
        effects4[0] = new Effect(EEffect.modify_armor, 10);
        m_AbilityUltimate = new Ability(effects4, 4, 5, 1, false, mask, TargetLayer.all_allies);

        ////////////////////



        info.exhausted = false;
        info.owningPlayer = _Player;
        info.defence = info.baseDefence;

        
        

        UpdateUI();
    }
    //Set the unit's starting position on the battlefield.
    private void InitializeTransform()
    {        
        Transform t = BattleManager.instance.GetFieldPosition(this);
        transform.position = t.position;
        transform.rotation = t.rotation;
    }

    private void Register()
    {
        //TODO: register to networked battle manager instead.
        BattleManager.instance.RegisterUnit(this);

        //Register all eventhandlers on start.
        //This is mostly an example of usage at the momment.
        EventHandler_Gameplay.OnUnitSpawn += this.NewUnitCreated;
        EventHandler_Gameplay.OnUnitDestroyed += this.OnUnitDestroy;
        if (info.owningPlayer == 1)
        {
            EventHandler_Gameplay.OnPlayer1TurnBegin += this.OnTurnBegin;
        }
        else if (info.owningPlayer == 2)
        {
            EventHandler_Gameplay.OnPlayer2TurnBegin += this.OnTurnBegin;
        }
        //Trigger new unit created.
        EventHandler_Gameplay.NewUnitCreated(this.gameObject, this.info.owningPlayer);
    }

    private void UpdateUI()
    {
            
    }


    #endregion

    #region Events
    public void KillUnit()
    {
        //Trigger unit death
        EventHandler_Gameplay.OnUnitDead(this.gameObject, this.info.owningPlayer);
    }
    //Implementation of the event.
    private void OnUnitDestroy(GameObject unit, int player)
    {
        //unregister all handlers if we remove the unit from the game.
        EventHandler_Gameplay.OnUnitSpawn -= this.NewUnitCreated;
        EventHandler_Gameplay.OnUnitDestroyed -= this.OnUnitDestroy;
    }
    //Implementation of the event.
    private void NewUnitCreated(GameObject unit, int player)
    {
        //Debug.Log("New Unit created: " + gameObject.name + " for player " + player);
    }
    //Reset the unit's stats at the start of turn.
    private void OnTurnBegin(GameObject unit, int player)
    {
        Debug.Log("OnTurnBegin:" + gameObject.name + ". From player " + info.owningPlayer);
        info.defence = info.baseDefence;
        m_AbilityOne.OnTurnBegin();
        m_AbilityTwo.OnTurnBegin();
        m_AbilitySpecial.OnTurnBegin();
        m_AbilityUltimate.OnTurnBegin();
    }

    public void OnMouseEnter()
    {
  
        m_IsMouseOver = true;
    }

    public void OnMouseExit()
    {
        m_IsMouseOver = false;
    }
    #endregion

    #region GamePlay Methods
    //Draw a card.
    public void Draw()
    {
        m_Deck.Draw_From_Deck();
    }
    


    public void Select()
    {
        Debug.Log("Unit is selected : " + gameObject.name);
        //On select show the unit's UI
        m_IsSelected = true;        
        ReferenceHolder.instance.UnitUI.SetActive(true, this);
    }

    public void Deselect()
    {
        Debug.Log("Unit is deselected : " + gameObject.name);
        //hide the unit's UI on deselect
        m_IsSelected = false;
        m_UsingAbility = false;
        TargetingTracer.instance.Close();
        ReferenceHolder.instance.UnitUI.SetActive(false);
    }

    public Vector3 GetTargetLocation()
    {
        return m_TargetingLocation.position;
    }

    /// <summary>
    /// Returns all the valid targets base on the layer that is targeted.
    /// the fieldPosition value of unit.info is used to determine this.
    /// The positions on the field are has follow from left to right. (When a unit dies all units will have their fieldPosition recalculated.)
    /// 1 - 2 - 3 - 4
    /// </summary>
    /// <param name="targetedLayer"></param>
    /// <returns></returns>
    public ITargetable[] GetValidTargets(TargetLayer targetedLayer)
    {
        List<ITargetable> targets = new List<ITargetable>();
        
        switch (targetedLayer)
        {
            case TargetLayer.none:
                Debug.Log("No Targets. Ability must be invalid.");
                break;
            case TargetLayer.player_selected:
                break;
            case TargetLayer.self:
                targets.Add(this);
                break;
            case TargetLayer.all:
                foreach (ITargetable t in BattleManager.instance.P1_Units)
                {
                    targets.Add(t);
                }
                foreach (ITargetable t in BattleManager.instance.P2_Units)
                {
                    targets.Add(t);
                }
                break;
            case TargetLayer.all_ennemies:
                if (info.owningPlayer == 1)
                {
                    foreach (ITargetable t in BattleManager.instance.P2_Units)
                    {
                        targets.Add(t);
                    }
                }
                else if(info.owningPlayer == 2)
                {
                    foreach (ITargetable t in BattleManager.instance.P1_Units)
                    {
                        targets.Add(t);
                    }
                }
                break;
            case TargetLayer.all_allies:
                if (info.owningPlayer == 1)
                {
                    foreach (ITargetable t in BattleManager.instance.P1_Units)
                    {
                        targets.Add(t);
                    }
                }
                else if (info.owningPlayer == 2)
                {
                    foreach (ITargetable t in BattleManager.instance.P2_Units)
                    {
                        targets.Add(t);
                    }
                }
                break;
            case TargetLayer.adjacent:
                if (info.owningPlayer == 1)
                {
                    int numberofUnits = BattleManager.instance.P1_Units.Count;
                    switch (numberofUnits)
                    {
                        case 1:
                            Debug.LogError("Error trying to target adjancent but not allies are around.");
                            break;
                        case 2:
                            //Only need to add the other unit.
                            foreach (Unit t in BattleManager.instance.P1_Units)
                            {
                                if (t != this)
                                {
                                    targets.Add(t);
                                }                                
                            }
                            break;
                        case 3:
                        case 4:
                            foreach (Unit t in BattleManager.instance.P1_Units)
                            {
                                if (t != this)
                                {
                                    //If the unit is no further than 1 in field position. Add().
                                    if (Mathf.Abs( t.info.fieldPosition - this.info.fieldPosition) == 1)
                                    {
                                        targets.Add(t);
                                    }
                                    
                                }
                            }  
                            break;
                    }
                }
                else if (info.owningPlayer == 2)
                {
                    int numberofUnits = BattleManager.instance.P2_Units.Count;
                    switch (numberofUnits)
                    {
                        case 1:
                            Debug.LogError("Error trying to target adjancent but not allies are around.");
                            break;
                        case 2:
                            //Only need to add the other unit.
                            foreach (Unit t in BattleManager.instance.P2_Units)
                            {
                                if (t != this)
                                {
                                    targets.Add(t);
                                }
                            }
                            break;
                        case 3:
                        case 4:
                            foreach (Unit t in BattleManager.instance.P2_Units)
                            {
                                if (t != this)
                                {
                                    //If the unit is no further than 1 in field position. Add().
                                    if (Mathf.Abs(t.info.fieldPosition - this.info.fieldPosition) == 1)
                                    {
                                        targets.Add(t);
                                    }

                                }
                            }
                            break;
                    }
                }
                break;
            case TargetLayer.first_left:
                if (info.owningPlayer == 1)
                {
                    foreach (Unit t in BattleManager.instance.P1_Units)
                    {
                        if (t.info.fieldPosition - this.info.fieldPosition == 1)
                        {
                            targets.Add(t);
                            break;
                        }                        
                    }
                }
                else if (info.owningPlayer == 2)
                {
                    foreach (Unit t in BattleManager.instance.P2_Units)
                    {
                        if (t.info.fieldPosition - this.info.fieldPosition == 1)
                        {
                            targets.Add(t);
                            break;
                        }
                    }
                }
                break;
            case TargetLayer.first_right:
                if (info.owningPlayer == 1)
                {
                    foreach (Unit t in BattleManager.instance.P1_Units)
                    {
                        if (t.info.fieldPosition - this.info.fieldPosition == -1)
                        {
                            targets.Add(t);
                            break;
                        }
                    }
                }
                else if (info.owningPlayer == 2)
                {
                    foreach (Unit t in BattleManager.instance.P2_Units)
                    {
                        if (t.info.fieldPosition - this.info.fieldPosition == -1)
                        {
                            targets.Add(t);
                            break;
                        }
                    }
                }
                break;
            case TargetLayer.all_left:
                if (info.owningPlayer == 1)
                {
                    foreach (Unit t in BattleManager.instance.P1_Units)
                    {
                        if (t.info.fieldPosition - this.info.fieldPosition <= -1)
                        {
                            targets.Add(t);
                        }
                    }
                }
                else if (info.owningPlayer == 2)
                {
                    foreach (Unit t in BattleManager.instance.P2_Units)
                    {
                        if (t.info.fieldPosition - this.info.fieldPosition <= -1)
                        {
                            targets.Add(t);
                        }
                    }
                }
                break;
            case TargetLayer.all_right:
                if (info.owningPlayer == 1)
                {
                    foreach (Unit t in BattleManager.instance.P1_Units)
                    {
                        if (t.info.fieldPosition - this.info.fieldPosition >= 1)
                        {
                            targets.Add(t);
                        }
                    }
                }
                else if (info.owningPlayer == 2)
                {
                    foreach (Unit t in BattleManager.instance.P2_Units)
                    {
                        if (t.info.fieldPosition - this.info.fieldPosition >= 1)
                        {
                            targets.Add(t);
                        }
                    }
                }
                break;
            case TargetLayer.front://Not sure this will be used
                break;
            default:
                break;
        }

        return targets.ToArray();
    }

    public void ReceiveEffects(Effect[] args)
    {
        Debug.Log("Unit" + gameObject.name + " receiving effects.");
        for (int currentEffect = 0; currentEffect < args.Length; currentEffect++)
        {
            ExecuteEffect(args[currentEffect]);
        }
    }

    private void ExecuteEffect(Effect effect)
    {
        Debug.Log("Effect received: " + effect.type + " of value " + effect.value);
        switch (effect.type)
        {
            case EEffect.none:
                Debug.Log("This Does no effect");
                break;
            case EEffect.attack:
                ReceiveAttack(effect.value);
                break;
            case EEffect.modify_health:
                ModifyHealth(effect.value);
                break;
            case EEffect.modify_armor:
                ModifyArmor(effect.value);
                break;
            case EEffect.modify_utility:
                break;
            case EEffect.modify_card_drawned:
                break;
            case EEffect.permanent_modify_armor:
                break;
            case EEffect.permanent_modify_health:
                break;
            case EEffect.permanent_modify_card_drawned:
                break;
            default:
                break;
        }
    }

    private void ReceiveAttack(int value)
    {
        info.defence -= value;
        if (info.defence < 0)
        {
            info.health += info.defence;
            info.defence = 0;
            if (info.health <= 0)
            {
                KillUnit();
            }
        }

    }
    private void ModifyArmor(int value)
    {
        info.defence += value;
        if (info.defence < 0)
        {
            info.defence = 0;
        }
    }
    private void ModifyHealth(int value)
    {
        info.health += value;
        if (info.health <= 0)
        {
            KillUnit();
        }
    }

    #endregion

    #region Utility
    /// <summary>
    /// CompareTo for sort() on list.
    /// </summary>
    /// <param name="u"></param>
    /// <returns></returns>
    public int CompareTo(Unit u)
    {
        // A null value means that this object is greater.
        if (u == null)
        {
            return 1;
        }
        else
        {
            return this.info.fieldPosition.CompareTo(u.info.fieldPosition);
        }
            
    }
    #endregion

}
