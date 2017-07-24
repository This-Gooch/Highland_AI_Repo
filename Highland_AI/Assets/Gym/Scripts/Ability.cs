using UnityEngine;



public class Ability : MonoBehaviour {

    /// <summary>
    /// Level required to use.
    /// </summary>
    public int levelRequired;
    /// <summary>
    /// Can this ability choose a target.
    /// </summary>
    public bool targetable;
    /// <summary>
    /// What can this ability target.
    /// </summary>
    LayerMask targetableMask;

    public void SelectTarget()
    {
        return;
    }
    

}
