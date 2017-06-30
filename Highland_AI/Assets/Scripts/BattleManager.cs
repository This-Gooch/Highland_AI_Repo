﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSGameplay.StateMachine;

public class BattleManager : MonoBehaviour {

    #region Public Member
    //Lists of player's units
    public List<Unit> P1_Units = new List<Unit>();
    public List<Unit> P2_Units = new List<Unit>();
    #endregion

    #region Private member

   // private int P1_TeamSize;
    //private int P2_TeamSize;

    #endregion


    //Checks to make sure all units are dead for win condition.
    int checkAlliesLethal;
    int checkOpponentsLethal;

    public bool selectingTarget;
    public GameObject activeAction;

    public GameObject deckLoc;
    public GameObject discardLoc;

    public List<Action_TurnStart> act_Start = new List<Action_TurnStart>();
    public List<Action_Immediate> act_Immediate = new List<Action_Immediate>();
    public List<Action_TurnEnd> act_End = new List<Action_TurnEnd>();

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

   /* public enum State
    {
        ExhaustToActive,
        TurnStart,
        DrawAction,
        Execute,
        ActiveToExhaust,
        EndTurn,
        Win,
        Lose,
    }*/

    public State state;

    private void Start()
    {
        Initialize();
        //P1_TeamSize = P1_Units.Count;
        //P2_TeamSize = P2_Units.Count;
        NextState();
    }



    #region Private Methods
    //Initialization.
    private void Initialize()
    {
        state = State.P1_Draw;
    }
    //Skip to the next State.
    private void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
    #endregion

    #region States

    /// <summary>
    /// Player 1
    /// </summary>

    //Exhaust Phase player 1
    IEnumerator P1_ExhaustState()
    {
        Debug.Log("P1_ExhaustState: Enter");
        while (state == State.P1_Exhaust)
        {
            CheckExhaustStatus(1);
            yield return null;
        }
        Debug.Log("P1_ExhaustState: Exit");
        NextState();
    }

    //Draw Phase Player 1
    IEnumerator P1_DrawState()
    {
        Debug.Log("P1_DrawState: Enter");
        while (state == State.P1_Draw)
        {
            //TODO: Call drawing function Here
            yield return null;
        }
        Debug.Log("P1_DrawState: Exit");
        NextState();
    }

    //Player 1 play phase. (Play cards, attack, defend, etc..)
    IEnumerator P1_PlayPhaseState()
    {
        Debug.Log("P1_PlayPhaseState: Enter");
        while (state == State.P1_PlayPhase)
        {            
            //TODO: Listen for cards being play. -> go to sub phase of PlayPhaseAction
            yield return null;
        }
        Debug.Log("P1_PlayPhaseState: Exit");
        NextState();
    }
    //Sub Phase while action cards are played.
    IEnumerator P1_PlayPhase_ActionState()
    {
        Debug.Log("P1_PlayPhase_ActionState: Enter");
        while (state == State.P1_PlayPhase_Action)
        {            
            yield return null;
        }
        Debug.Log("P1_PlayPhase_ActionState: Exit");
        NextState();
    }
    //End of player 1's turn.
    IEnumerator P1_EndTurnState()
    {
        Debug.Log("P1_EndTurnState: Enter");
        while (state == State.P1_EndTurn)
        {
            yield return null;
        }
        Debug.Log("P1_EndTurnState: Exit");
        NextState();
    }
    //Activation phase?
    IEnumerator P1_ActivateState()
    {
        Debug.Log("P1_ActivateState: Enter");
        while (state == State.P1_Activate)
        {
            //TODO: figure out what happens here.
            yield return null;
        }
        Debug.Log("P1_ActivateState: Exit");
        NextState();
    }

    /// <summary>
    /// Player 2
    /// </summary>

    //Exhaust Phase player 2
    IEnumerator P2_ExhaustState()
    {
        Debug.Log("P2_ExhaustState: Enter");
        while (state == State.P2_Exhaust)
        {
            CheckExhaustStatus(2);
            yield return null;
        }
        Debug.Log("P2_ExhaustState: Exit");
        NextState();
    }

    //Draw Phase Player 2
    IEnumerator P2_DrawState()
    {
        Debug.Log("P2_DrawState: Enter");
        while (state == State.P2_Draw)
        {
            //TODO: Call drawing function Here
            yield return null;
        }
        Debug.Log("P2_DrawState: Exit");
        NextState();
    }

    //Player 2 play phase. (Play cards, attack, defend, etc..)
    IEnumerator P2_PlayPhaseState()
    {
        Debug.Log("P2_PlayPhaseState: Enter");
        while (state == State.P2_PlayPhase)
        {
            //TODO: Listen for cards being play. -> go to sub phase of PlayPhaseAction
            yield return null;
        }
        Debug.Log("P2_PlayPhaseState: Exit");
        NextState();
    }
    //Sub Phase while action cards are played.
    IEnumerator P2_PlayPhase_ActionState()
    {
        Debug.Log("P2_PlayPhase_ActionState: Enter");
        while (state == State.P2_PlayPhase_Action)
        {
            yield return null;
        }
        Debug.Log("P2_PlayPhase_ActionState: Exit");
        NextState();
    }
    //End of player 2's turn.
    IEnumerator P2_EndTurnState()
    {
        Debug.Log("P2_EndTurnState: Enter");
        while (state == State.P2_EndTurn)
        {
            yield return null;
        }
        Debug.Log("P2_EndTurnState: Exit");
        NextState();
    }
    //Activation phase?
    IEnumerator P2_ActivateState()
    {
        Debug.Log("P1_ActivateState: Enter");
        while (state == State.P2_Activate)
        {
            //TODO: figure out what happens here.
            yield return null;
        }
        Debug.Log("P2_ActivateState: Exit");
        NextState();
    }

    /// <summary>
    /// GameOver
    /// </summary>
    /// 
    IEnumerator GameOver_P1_WinState()
    {
        Debug.Log("GameOver_P1_WinState: Enter");
        while (state == State.GameOver_P1_Win)
        {
            //TODO: end Game.
            yield return null;
        }
        Debug.Log("GameOver_P1_WinState: Exit");
    }
    IEnumerator GameOver_P2_WinState()
    {
        Debug.Log("GameOver_P2_WinState: Enter");
        while (state == State.GameOver_P2_Win)
        {
            //TODO: end Game.
            yield return null;
        }
        Debug.Log("GameOver_P2_WinState: Exit");
    }
    IEnumerator GameOver_DrawState()
    {
        Debug.Log("GameOver_DrawState: Enter");
        while (state == State.GameOver_Draw)
        {
            //TODO: end Game.
            yield return null;
        }
        Debug.Log("GameOver_DrawState: Exit");
    }

    #endregion

    #region Events Called
    void CheckExhaustStatus(int player)
    {
        if (player == 1) // Player 1
        {
            foreach (Unit unit in P1_Units)
            {
                if (unit.GetComponent<UnitStats>().exhaustStateCount > 0)
                {
                    Debug.Log("Reduce Exhaust Count");
                    unit.GetComponent<UnitStats>().exhaustStateCount--;
                }
            }
            state = State.P1_Draw;
        }        
        else // Player 2
        {
            foreach (Unit unit in P2_Units)
            {
                if (unit.GetComponent<UnitStats>().exhaustStateCount > 0)
                {
                    Debug.Log("Reduce Exhaust Count");
                    unit.GetComponent<UnitStats>().exhaustStateCount--;
                }
            }
            state = State.P2_Draw;
        }
        ResetToBaseStats();
       
    }

    void ResetToBaseStats()
    {
        foreach (Unit unit in P1_Units)
        {
            if(unit.GetComponent<UnitStats>().defense < unit.GetComponent<UnitStats>().baseDefense)
            {
                unit.GetComponent<UnitStats>().defense = unit.GetComponent<UnitStats>().baseDefense;
            }
        }
        foreach (Unit unit in P2_Units)
        {
            if (unit.GetComponent<UnitStats>().defense < unit.GetComponent<UnitStats>().baseDefense)
            {
                unit.GetComponent<UnitStats>().defense = unit.GetComponent<UnitStats>().baseDefense;
            }
        }
    }
    void TriggerActionDraws()
    {
        foreach (Unit unit in P1_Units)
        {
            if (unit.GetComponent<UnitStats>().exhaustStateCount == 0)
                unit.GetComponent<ActionManager>().DrawActions();
        }
        //state = State.Execute;
    }


    public void FinishExecuteState()
    {
        //state = State.ActiveToExhaust;
    }

    void CheckActiveToExhaust()
    {
        foreach (Unit unit in P1_Units)
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
        //state = State.EndTurn;
    }
    //Call everytime a unit dies.
    public void CheckLethal()
    {
        bool p1_Dead = true;
        bool p2_Dead = true;
        foreach (Unit unit in P1_Units)
        {
            if (unit.mHealth > 0)
            {
                p1_Dead = false;

            }
        }       

        foreach (Unit unit in P2_Units)
        {
            if (unit.mHealth > 0)
            {
                p2_Dead = false;
            }
        }
       
    }
    #endregion





 
    
}
