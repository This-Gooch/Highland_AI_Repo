using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Hold all global rules implemented.
/// I.E. what is the max/min deck size you can have.
/// </summary>
public class Rules : MonoBehaviour {

    public static Rules instance;

    public int _MinimumDeckSize = 20;
    public int _MaximumDeckSize = 30;

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
}
