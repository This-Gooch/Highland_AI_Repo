using UnityEngine;
using NSGameplay;
/// <summary>
/// Base Class
/// </summary>
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

    public void SelectTarget()
    {
        return;
    }

    public void Use(int utility)
    {
        if (utility < cost)
        {
            //TODO: Play sound letting the player know they do not have the necessary utlitity banked.
            return;
        }
        
    }

    public void SendEffect()
    {

    }

    public void OnTurnBegin()
    {
        numberOfTimeUsed = 0;
    }
}



