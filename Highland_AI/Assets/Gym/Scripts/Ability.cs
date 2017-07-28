using UnityEngine;
using NSGameplay;
using System;
/// <summary>
/// Base Class
/// </summary>
[System.Serializable]
public class Ability : IEffector{

    /// <summary>
    /// Level required to use.
    /// </summary>
    public int levelRequired;
    /// <summary>
    /// Utility cost of this ability.
    /// </summary>
    public int cost;
    /// <summary>
    /// How many times this ability can be used per turn.
    /// </summary>
    public int numberOfUsesPerTurn;
    /// <summary>
    /// How many times this ability was used.
    /// resets every turn.
    /// </summary>
    public int numberOfTimeUsed;

    /// <summary>
    /// Can this ability choose a target.
    /// </summary>
    public bool targetable;
    /// <summary>
    /// If targetable is false targetLayer decides what target is choosen.
    /// </summary>
    public TargetLayer targetLayer;
    /// <summary>
    /// What can this ability target.
    /// </summary>
    public LayerMask targetableMask;
    /// <summary>
    /// The actual effects this ability do.
    /// </summary>
    public Effect[] effects;

    //Constructors
    public Ability(){  }

    public Ability(Effect[] e, int level, int cost, int usesPerTurn, bool canTarget, LayerMask targetMask, TargetLayer TLayer = TargetLayer.none)
    {
        this.effects = e;
        this.levelRequired = level;
        this.cost = cost;
        this.numberOfUsesPerTurn = usesPerTurn;
        this.targetable = canTarget;
        this.targetableMask = targetMask;
        this.targetLayer = TLayer;        
    }

    public void SelectTarget()
    {
        return;
    }

    public int Use(int utility, ITargetable target)
    {
        if (numberOfTimeUsed >= numberOfUsesPerTurn)
        {
            //TODO: Play sound letting the player know that this ability cannot be used.
            return 0;
        }
        if (utility < cost)
        {
            //TODO: Play sound letting the player know they do not have the necessary utlitity banked.
            return 0;
        }
        numberOfUsesPerTurn--;
        SendEffects(target);

        return cost;
    }

    public void SendEffects(ITargetable target)
    {
        Debug.Log("Sending effects from ability");
        target.ReceiveEffects(effects);
    }

    public void OnTurnBegin()
    {
        numberOfTimeUsed = 0;
    }

}



