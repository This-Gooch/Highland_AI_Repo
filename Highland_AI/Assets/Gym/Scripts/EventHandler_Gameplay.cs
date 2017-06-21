using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Event delegate relating to Gameplay.
/// Implementation is done on the registered entity.
/// </summary>
public class EventHandler_Gameplay : MonoBehaviour {
    
    //Delegate 
    public delegate void UnitEventHandler(GameObject unit);
    //OnUnitSpawn event. Register objects to implement function On unit spawn.
    public static event UnitEventHandler OnUnitSpawn;
    //OnUnitDestroyed event. Register objects to implement functions On unit destroyed.
    public static event UnitEventHandler OnUnitDestroyed;

    //Calls the delegate
    public static void NewUnitCreated(GameObject unit)
    {
        if (OnUnitSpawn != null)
        {
            OnUnitSpawn(unit);
        }
    }
    //Calls the delegate
    public static void UnitDead(GameObject unit)
    {
        if (OnUnitDestroyed != null)
        {
            OnUnitDestroyed(unit);
        }
    }
}
