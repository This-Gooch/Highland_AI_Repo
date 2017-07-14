using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using NSGameplay;
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
    public UnitInfo(int health, int defence, int attack, int utility, string name, string portraitPath)
    {
        this.health = health;
        this.attack = attack;
        this.baseDefence = defence;
        this.defence = defence;
        this.utility = utility;
        this.name = name;
        this.portraitPath = portraitPath;
        exhausted = false;
    }

    public string id { get; set; }
    public int health { get; set; }
    public int defence { get; set; }
    public int baseDefence { get; set; }
    public int attack { get; set; }
    public int utility { get; set; }
    public bool exhausted { get; set; }
    public int owningPlayer { get; set; }
    public string name { get; set; }
    public string portraitPath { get; set; }
}
public class Unit : MonoBehaviour {

    #region Editor references
    [SerializeField]
    Image _Portrait;
    [SerializeField]
    Text _Name;
    [SerializeField]
    Text _Health;
    [SerializeField]
    Text _Defence;
    [SerializeField]
    Text _Attack;
    [SerializeField]
    Text _Utility;
    [SerializeField]
    int _Player;
    [SerializeField]
    GameObject _CardSlot1;
    [SerializeField]
    GameObject _CardSlot2;
    [SerializeField]
    GameObject _CardSlot3;
    #endregion

    #region Public members
    public UnitInfo info;
    //Card in hand for the unit.
    public List<Card> m_Hand = new List<Card>();

    public Deck m_Deck;
    #endregion

    #region Private members
    private string m_PortraitImagePath = "Units/Portraits/main";

    #endregion


    private void Awake()
    {
        Initialize();              
    }

    private void Start()
    {
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

        Register();
    }

    #region public Methods
    //When attacking an other unit must defend.
    public void Attack()
    {
        //TODO: implement attack for units.

        UpdateUI();
    }
    //The flip side of an attack
    public void Defend()
    {
        //TODO: implement defend for units.

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
        info.name = "The Name";
        ////////////////////

        _Portrait.sprite = Resources.Load<Sprite>(m_PortraitImagePath) as Sprite;
        _Name.text = info.name;
        info.exhausted = false;
        info.owningPlayer = _Player;
        info.defence = info.baseDefence;
        UpdateUI();
    }

    private void Register()
    {
        //TODO: register to networked battle manager instead.
        BattleManager.instance.RegisterUnit(this);
    }

    private void UpdateUI()
    {
        _Health.text = info.health.ToString();
        _Defence.text = info.defence.ToString();
        _Attack.text = info.attack.ToString();
        _Utility.text = info.utility.ToString();
        
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

    #endregion


}
