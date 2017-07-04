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
        Card c = new Action();
        c.id = NSGameplay.Cards.ECardKeys.DirectAttack;
        /*c.m_Cost = 2;
        c.m_Name = c.id.ToString();
        c.m_Tooltip = new NSGameplay.Cards.Tooltip();
        c.m_Attack = 4;
        c.m_BaseDefence = 2;
        c.m_Defence = 2;
        c.m_Health = 20;
        c.m_Utility = 0;*/

        CardList cl= new CardList("main");
        cl.cardList.Add(c);
        XMLDataSerializer.SaveCards(cl, "Assets/Data/Cards.xml");
    }

    private void CreateNewUnit()
    {
        Unit u = new Unit();
    }
}
