using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    #region Editor references
    [SerializeField]
    Image _Portrait;
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
    #endregion

    #region Public members
    public int m_Health          { get; private set; }
    public int m_Defence         { get; set; }
    public int m_BaseDefence     { get; private set; }
    public int m_Attack          { get; private set; }
    public int m_Utility         { get; private set; }
    public bool m_Exhausted      { get; private set; }
    public int m_OwningPlayer    { get; private set; }
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
        if (m_OwningPlayer == 1)
        {
            EventHandler_Gameplay.OnPlayer1TurnBegin += this.OnTurnBegin;
        }
        else if (m_OwningPlayer == 2)
        {
            EventHandler_Gameplay.OnPlayer2TurnBegin += this.OnTurnBegin;
        }
        //Trigger new unit created.
        EventHandler_Gameplay.NewUnitCreated(this.gameObject, this.m_OwningPlayer);

        Register();
    }

    #region public Methods
    //When attacking an other unit must defend.
    public void Attack()
    {
        //TODO: implement attack for units.
    }
    //The flip side of an attack
    public void Defend()
    {
        //TODO: implement defend for units.
    }

    #endregion

    #region private Methods
    //Init on start.
    private void Initialize()
    {
        /*Debug init. Real value should come from file*/
        m_Health = 100;
        m_Attack = 10;
        m_BaseDefence = 5;
        m_Utility = 2;

        _Portrait.sprite = Resources.Load<Sprite>(m_PortraitImagePath) as Sprite;
        m_Exhausted = false;
        m_OwningPlayer = _Player;
        m_Defence = m_BaseDefence;
       
    }

    private void Register()
    {
        //TODO: register to networked battle manager instead.
        BattleManager.instance.RegisterUnit(this);
    }

    #endregion

    #region Events
    public void KillUnit()
    {
        //Trigger unit death
        EventHandler_Gameplay.OnUnitDead(this.gameObject, this.m_OwningPlayer);
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
        Debug.Log("OnTurnBegin:" + gameObject.name + ". From player " + m_OwningPlayer);
        m_Defence = m_BaseDefence;
    }

    #endregion


}
