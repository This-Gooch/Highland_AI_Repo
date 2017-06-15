using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    public List<GameObject> Allies = new List<GameObject>();
    int allyTeamSize;
    public List<GameObject> Opponents =  new List<GameObject>();
    int opponentTeamSize;


    public bool selectingTarget;
    public GameObject activeAction;

    public GameObject deckLoc;
    public GameObject discardLoc;

    public List<Action_TurnStart> act_Start = new List<Action_TurnStart>();
    public List<Action_Immediate> act_Immediate = new List<Action_Immediate>();
    public List<Action_TurnEnd> act_End = new List<Action_TurnEnd>();
    //public GameObject[] exicutionOrder;

    /* The purpose of this script is to manage the battle phases. May use a state machine for this.
     * Battle Start
     * -> Set Exhausted Units to Active
     * -> Trigger Turn Start abilities
     * -> Draw Phase
     * -> Execute (Use abilities)
     * -> Trigger End Turn abilities
     * -> Check if Exhausted State should be active on Units
     * -> End Turn
     * This will proabably change over time. 
    */

    public enum State
    {
        ExhaustToActive,
        TurnStart,
        DrawAction,
        Execute,
        ActiveToExhaust,
        EndTurn,
        Win,
        Lose,
    }

    public State state;

    private void Start()
    {
        allyTeamSize = Allies.Count;
        opponentTeamSize = Opponents.Count;
        NextState();
    } 

    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName, 
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
    //Check the Exxhaust Status of unit. First Phase of a turn.
    IEnumerator ExhaustToActiveState()
    {
        Debug.Log("ExhaustToActiveState: Enter");
        while (state == State.ExhaustToActive)
        {
            CheckExhaustStatus();
            yield return null;
        }
        Debug.Log("ExhaustToActiveState: Exit");
        CheckLethal();
        NextState();
    }

    void CheckExhaustStatus()
    {
        foreach (GameObject unit in Allies)
        {
            if(unit.GetComponent<UnitStats>().exhaustStateCount > 0)
            {
                Debug.Log("Reduce Exhaust Count");
                unit.GetComponent<UnitStats>().exhaustStateCount--;
            }
        }
        ResetToBaseStats();
        state = State.TurnStart;
    }

    void ResetToBaseStats()
    {
        foreach (GameObject unit in Allies)
        {
            if(unit.GetComponent<UnitStats>().defense < unit.GetComponent<UnitStats>().baseDefense)
            {
                unit.GetComponent<UnitStats>().defense = unit.GetComponent<UnitStats>().baseDefense;
            }
        }
        foreach (GameObject unit in Opponents)
        {
            if (unit.GetComponent<UnitStats>().defense < unit.GetComponent<UnitStats>().baseDefense)
            {
                unit.GetComponent<UnitStats>().defense = unit.GetComponent<UnitStats>().baseDefense;
            }
        }
    }
    //Use this phase for any recurring Turn Start effects.
    IEnumerator TurnStartState()
    {
        Debug.Log("TurnStartState: Enter");
        while (state == State.TurnStart)
        {
            state = State.DrawAction;
            yield return null;
        }
        Debug.Log("TurnStartState: Exit");
        CheckLethal();
        NextState();
    }
    //ALl the Drawing Action happens at this phase.
    IEnumerator DrawActionState()
    {
        Debug.Log("DrawActionState: Enter");
        while (state == State.DrawAction)
        {
            TriggerActionDraws();
            yield return null;
        }
        Debug.Log("DrawActionState: Exit");
        CheckLethal();
        NextState();
    }

    void TriggerActionDraws()
    {
        foreach(GameObject unit in Allies)
        {
            if(unit.GetComponent<UnitStats>().exhaustStateCount == 0)
            unit.GetComponent<ActionManager>().DrawActions();
        }
        state = State.Execute;
    }
    //This is the players turn state. This will also be where we call the enemy AI decicions.
	IEnumerator ExecuteState()
    {
        Debug.Log("ExecuteState: Enter");
        while (state == State.Execute)
        {
            yield return null;
        }
        Debug.Log("ExecuteState: Exit");
        CheckLethal();
        NextState();
    }

    public void FinishExecuteState()
    {
        state = State.ActiveToExhaust;
    }
/*
    public void ExecuteActions()
    {
        Debug.Log("Execute Activated");
        foreach (Action_Immediate act in act_Immediate)
        {
            act.GetComponent<Action_Immediate>().ExecuteAction();
        }
        state = State.CheckLethal;
    }
*/
/*
    IEnumerator CheckLethalState()
    {
        Debug.Log("CheckLethalState: Enter");
        while (state == State.CheckLethal)
        {
            yield return null;
        }
        Debug.Log("CheckLethalState: Exit");
        NextState();
    }
    
    void CheckUnitHealth()
    {
        foreach (GameObject unit in Allies)
        {
            if (unit.GetComponent<UnitStats>().health <= 0)
            {
                Debug.Log(unit.name + " is dead");
            }
            state = State.ActiveToExhaust;
        }
    }
    */
    //This is when we check the deck and hand to see if they are empty, then set the unit to an exhaust state.
    IEnumerator ActiveToExhaustState()
    {
        Debug.Log("ActiveToExhaustState: Enter");
        while (state == State.ActiveToExhaust)
        {
            CheckActiveToExhaust();
            yield return null;
        }
        Debug.Log("ActiveToExhaustState: Exit");
        CheckLethal();
        NextState();
    }

    void CheckActiveToExhaust()
    {
        foreach (GameObject unit in Allies)
        {
            if (unit.GetComponent<ActionManager>().stackDeck.Count == 0 
                && unit.GetComponent<ActionManager>().inHand.Count == 0
                && unit.GetComponent<UnitStats>().exhaustStateCount == 0)
            {
                Debug.Log("Setting " + unit.name + " to Exhaust State");
                unit.GetComponent<UnitStats>().exhaustStateCount += 2;
                unit.GetComponent<ActionManager>().RestockStack();
            }
        }
        state = State.EndTurn;
    }

    //Any End Turn effects are called here.
    IEnumerator EndTurnState()
    {
        Debug.Log("EndTurnState: Enter");
        while (state == State.EndTurn)
        {
            state = State.ExhaustToActive;
            yield return null;
        }
        Debug.Log("EndTurnState: Exit");
        CheckLethal();
        NextState();
    }

    IEnumerator WinState()
    {
        while(state == State.Win)
        {
            yield return null;
        }
    }

    IEnumerator LoseState()
    {
        while (state == State.Lose)
        {
            yield return null;
        }
    }

    //Checks to make sure all units are dead for win condition.
    int checkAlliesLethal;
    int checkOpponentsLethal;
    public void CheckLethal()
    {
        checkAlliesLethal = 0;
        checkOpponentsLethal = 0;

        foreach(GameObject unit in Allies)
        {
            if(unit.GetComponent<UnitStats>().health <= 0)
            {
                checkAlliesLethal++;
            }
        }
        if(checkAlliesLethal == allyTeamSize)
        {
            Debug.Log("You lose");
            state = State.Lose;
        }

        foreach (GameObject unit in Opponents)
        {
            if (unit.GetComponent<UnitStats>().health <= 0)
            {
                checkOpponentsLethal++;
            }
        }
        if (checkOpponentsLethal == opponentTeamSize)
        {
            Debug.Log("You Win");
            state = State.Win;
        }
    }
}
