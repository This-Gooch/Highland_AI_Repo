using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCards : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateNewCard();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            CreateNewUnit();
        }
    }

    private void CreateNewCard()
    {
        Card c = new Minion();
        c.id = NSGameplay.Cards.ECardKeys.DirectAttack;
        c.m_Cost = 2;
        c.m_Name = c.id.ToString();
                
    }

    private void CreateNewUnit()
    {
        Unit u = new Unit();
    }
}
