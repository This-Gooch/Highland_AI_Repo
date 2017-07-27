using UnityEngine;
using NSGameplay;
/// <summary>
/// Base Class
/// </summary>
[System.Serializable]
public class Ability {

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
    LayerMask targetableMask;
    /// <summary>
    /// The actual effects this ability do.
    /// </summary>
    public Effect[] effects;

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
        SendEffect(target);

        return cost;
    }

    public void SendEffect(ITargetable target)
    {
        target.ReceiveEffects(effects);
    }

    public void OnTurnBegin()
    {
        numberOfTimeUsed = 0;
    }
}



