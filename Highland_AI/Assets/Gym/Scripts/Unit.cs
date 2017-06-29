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
    #endregion

    #region Public members
    public int mHealth  { get; private set; }
    public int mDefence { get; private set; }
    public int mAttack  { get; private set; }
    public int mUtility { get; private set; }
    #endregion

    #region Private members
    private string mPortraitImagePath = "Units/Portraits/main";

    #endregion

    private void Awake()
    {
        _Portrait.sprite = Resources.Load<Sprite>(mPortraitImagePath) as Sprite;
    }

    private void Start()
    {
        //Register all eventhandlers on start.
        //This is mostly an example of usage at the momment.
        EventHandler_Gameplay.OnUnitSpawn += this.NewUnitCreated;
        EventHandler_Gameplay.OnUnitDestroyed += this.OnUnitDestroy;
        //Trigger new unit created.
        EventHandler_Gameplay.NewUnitCreated(this.gameObject);
    }


    #region Events
    public void KillUnit()
    {
        //Trigger unit death
        EventHandler_Gameplay.UnitDead(this.gameObject);
    }
    //Implementation of the event.
    private void OnUnitDestroy(GameObject unit)
    {
        //unregister all handlers if we remove the unit from the game.
        EventHandler_Gameplay.OnUnitSpawn -= this.NewUnitCreated;
        EventHandler_Gameplay.OnUnitDestroyed -= this.OnUnitDestroy;
    }
    //Implementation of the event.
    private void NewUnitCreated(GameObject unit)
    {
        console.log("New Unit created: " + gameObject.name);
    }

    #endregion


}
