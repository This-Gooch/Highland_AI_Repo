using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {


    private void Awake()
    {
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

    void Update()
    {

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
