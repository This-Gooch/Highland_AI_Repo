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
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadUnits();
           
           
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadCards();
        }
    }

    private void CreateNewCard()
    {
        Minion c = new Minion();
        c.id = NSGameplay.Cards.ECardKeys.BasicMinion;
        c.cost = 2;
        c.name = c.id.ToString();
        c.tooltip = new NSGameplay.Cards.Tooltip();
        c.tooltip.description = "The description.";
        c.tooltip.title = "Title of tooltip";
        c.tooltip.image_Path = "path of an image";
        c.attack = 4;
        c.baseDefence = 2;
        c.defence = 2;
        c.health = 20;
        c.utility = 0;

        CardList cl= new CardList("main");
        cl.cardList.Add(c);
        XMLDataSerializer.SaveCards(cl, "Assets/Data/Cards.xml");
    }

    private void CreateNewUnit()
    {
        UnitInfo u = new UnitInfo( 100, 5,10,1,"The first unit's name", "Units/Portraits/main");
        u.id = NSGameplay.EUnitIDs.UnitX1;
        UnitList ul = new UnitList("main");
        ul.Add(u);
        XMLDataSerializer.SaveUnits(ul, "Assets/Data/Units.xml");
    }

    private void LoadUnits()
    {
        Debug.Log("Loading units");
        XMLDataSerializer.LoadUnits("Assets/Data/Units.xml");
    }

    private void LoadCards()
    {
        XMLDataSerializer.LoadCards("Assets/Data/Cards.xml");
    }
}
