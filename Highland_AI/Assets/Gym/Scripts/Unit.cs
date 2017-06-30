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
    public int mHealth          { get; private set; }
    public int mDefence         { get; set; }
    public int mBaseDefence     { get; private set; }
    public int mAttack          { get; private set; }
    public int mUtility         { get; private set; }
    public bool mExhausted      { get; private set; }
    public int mOwningPlayer    { get; private set; }
    #endregion

    #region Private members
    private string mPortraitImagePath = "Units/Portraits/main";
    
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
        if (mOwningPlayer == 1)
        {
            EventHandler_Gameplay.OnPlayer1TurnBegin += this.OnTurnBegin;
        }
        else if (mOwningPlayer == 2)
        {
            EventHandler_Gameplay.OnPlayer2TurnBegin += this.OnTurnBegin;
        }
        //Trigger new unit created.
        EventHandler_Gameplay.NewUnitCreated(this.gameObject, this.mOwningPlayer);
    }

    #region public Methods

    

    #endregion

    #region private Methods
    //Init on start.
    private void Initialize()
    {
        _Portrait.sprite = Resources.Load<Sprite>(mPortraitImagePath) as Sprite;
        mExhausted = false;
        mOwningPlayer = _Player;
    }


    #endregion

    #region Events
    public void KillUnit()
    {
        //Trigger unit death
        EventHandler_Gameplay.UnitDead(this.gameObject, this.mOwningPlayer);
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
        console.log("New Unit created: " + gameObject.name + " for player " + player);
    }
    //Reset the unit's stats at the start of turn.
    private void OnTurnBegin(GameObject unit, int player)
    {
        console.log("OnTurnBegin:");
        mDefence = mBaseDefence;
    }

    #endregion


}
