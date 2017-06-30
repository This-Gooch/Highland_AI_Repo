using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Event delegate relating to Gameplay.
/// Implementation is done on the registered entity.
/// </summary>
public class EventHandler_Gameplay : MonoBehaviour {
    
    //Delegate 
    public delegate void UnitEventHandler(GameObject unit, int player);
    //OnUnitSpawn event. Register objects to implement function On unit spawn.
    public static event UnitEventHandler OnUnitSpawn;
    //OnUnitDestroyed event. Register objects to implement functions On unit destroyed.
    public static event UnitEventHandler OnUnitDestroyed;
    //OnPlayer1 turn begin event.
    public static event UnitEventHandler OnPlayer1TurnBegin;
    //OnPlayer2 turn begin event.
    public static event UnitEventHandler OnPlayer2TurnBegin;

    //Calls the delegate
    public static void NewUnitCreated(GameObject unit, int player)
    {
        if (OnUnitSpawn != null)
        {
            OnUnitSpawn(unit, player);
        }
    }
    //Calls the delegate
    public static void UnitDead(GameObject unit, int player)
    {
        if (OnUnitDestroyed != null)
        {
            OnUnitDestroyed(unit, player);
        }
    }
    public static void OnTurnBegin(GameObject unit, int player)
    {
        if (player == 1)
        {
            if (OnPlayer1TurnBegin != null)
            {
                OnPlayer2TurnBegin(unit, player);
            }           
        }
        else if (player == 2)
        {
            if (OnPlayer2TurnBegin != null)
            {
                OnPlayer1TurnBegin(unit, player);
            }            
        }
        else
        {
            Debug.Log("ERROR: Expected player 1 or 2 instead of " + player + " in OnTurnBegin.");
        }
    }

}
