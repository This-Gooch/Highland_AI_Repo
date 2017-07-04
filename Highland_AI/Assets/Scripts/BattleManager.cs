using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSGameplay.StateMachine;
using System;

public class BattleManager : MonoBehaviour {

    public static BattleManager instance;

    #region Public Member
    //Lists of player's units
    public List<Unit> P1_Units = new List<Unit>();
    public List<Unit> P2_Units = new List<Unit>();
    #endregion

    #region Private member
    

    #endregion


    public bool selectingTarget;
    public GameObject activeAction;

    public GameObject deckLoc;
    public GameObject discardLoc;

    //List of passives currently active.
    public List<Passive> m_ActivePassives = new List<Passive>();


    public State state { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Initialize();
        NextState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state++;
            NextState();
        }
    }

    #region Private Methods
    //Initialization.
    private void Initialize()
    {
        EventHandler_Gameplay.OnUnitDestroyed += this.OnUnitDestroy;
        state = State.P1_Exhaust;
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
    //Call everytime a unit dies.
    private void CheckLethal()
    {
        bool p1_Dead = true;
        bool p2_Dead = true;
        foreach (Unit unit in P1_Units)
        {
            if (unit.info.health > 0)
            {
                p1_Dead = false;
            }
        }

        foreach (Unit unit in P2_Units)
        {
            if (unit.info.health > 0)
            {
                p2_Dead = false;
            }
        }
        if (p1_Dead && p2_Dead)
        {
            state = State.GameOver_Draw;
            NextState();
        }
        else if (p1_Dead)
        {
            state = State.GameOver_P2_Win;
            NextState();
        }
        else if (p2_Dead)
        {
            state = State.GameOver_P1_Win;
            NextState();
        }
    }
    #endregion

    #region public Methods
    //Register a Unit with the manager.
    public bool RegisterUnit(Unit u)
    {
        if (u != null)
        {
            if (u.info.owningPlayer == 1)
            {
                P1_Units.Add(u);
                return true;
            }
            else if (u.info.owningPlayer == 2)
            {
                P2_Units.Add(u);
                return true;
            }
            else
            {
                Debug.LogError("Error registering unit to BattleManager. Owning player incorrectly defined.");
                return false;
            }
        }
        Debug.LogError("Error registering unit to BattleManager. Null Reference.");
        return false;
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
            EventHandler_Gameplay.OnTurnBegin(this.gameObject, 1);
            state = State.P1_Draw;
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
            state = State.P2_Exhaust;
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
            EventHandler_Gameplay.OnTurnBegin(this.gameObject, 2);
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
            state = State.P1_Exhaust;
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
                if (unit.info.exhausted)
                {
                    Debug.Log("Unit is exhausted");
                }
            }
            state = State.P1_Draw;
        }        
        else // Player 2
        {
            foreach (Unit unit in P2_Units)
            {
                if (unit.info.exhausted)
                {
                    Debug.Log("Unit is exhausted");
                }
            }
            state = State.P2_Draw;
        }
       
    }
    //Called when a unit dies
    private void OnUnitDestroy(GameObject unit, int player)
    {
        Debug.Log("A Unit has died : " + unit.name + " from player " + player);
        CheckLethal();
    }

    #endregion
}
