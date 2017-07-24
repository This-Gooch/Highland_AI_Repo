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
        this.health = health;
        this.attack = attack;
        this.baseDefence = defence;
        this.defence = defence;
        this.utility = utility;
        this.name = name;
        this.exhausted = false;
    }
    /// <summary>
    /// Name is both the card's displayed Name AND id.
    /// </summary>
    public string name { get; set; }
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
    
    public string portraitPath { get; set; }
}
public class Unit : MonoBehaviour, ISelectable {

    #region Editor references
    
    [SerializeField]
    int _Player;
    
    #endregion

    #region Public members
    public UnitInfo info;
    //Card in hand for the unit.
    public List<Card> m_Hand = new List<Card>();

    public Deck m_Deck;
    #endregion

    #region Private members
    private float m_UnitHeight;

    private string m_PortraitImagePath = "Units/Portraits/";

    #endregion


    private void Awake()
    {
        Initialize();
        Register();
    }

    private void Start()
    {
        InitializeTransform();
    }

    #region public Methods
    /// <summary>
    /// At lvl 1
    /// </summary>
    public void UseAbilityOne()
    {
        UpdateUI();
    }
    /// <summary>
    /// At lvl 2
    /// </summary>
    public void UseAbilityTwo()
    {
        UpdateUI();
    }
    /// <summary>
    /// At lvl 3
    /// </summary>
    public void UseSpecial()
    {
        UpdateUI();
    }
    /// <summary>
    /// At lvl 4
    /// </summary>
    public void UseUltimate()
    {
        UpdateUI();
    }
    //Draw a card.
    public void Draw()
    {
        m_Deck.Draw_From_Deck();
    }
    #endregion

    #region private Methods
    //Init on start.
    private void Initialize()
    {
        /*Debug init. Real value should come from file*/
        //TODO: Load unit data from file
        info.health = 100;
        info.attack = 10;
        info.baseDefence = 5;
        info.utility = 2;
        info.name = "the_name_of_the_hero";
        ////////////////////

        //Set the height at which to display UI elements.
        //m_UnitHeight = 


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
    }

    public void OnMouseOver(PointerEventData eventData)
    {
        Debug.Log("OnMouseOver");
    }

    public void OnMouseClick(PointerEventData eventData)
    {
        Debug.Log("OnMouseClick");
    }

    #endregion


}
