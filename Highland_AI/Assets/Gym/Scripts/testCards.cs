using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCards : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J))//Creates a new card, saves it in the library then saves the entire library to file
        {
            CreateNewCardToLibrary();
        }

        if (Input.GetKeyDown(KeyCode.I))//Creates a new unit, saves it in the library then saves the entire library to file
        {
            CreateNewUnitToLibrary();
        }

        if (Input.GetKeyDown(KeyCode.C))//create a test card ands save it to file directly
        {
            CreateNewCard();
        }
        if (Input.GetKeyDown(KeyCode.U))//Creates a test unitinfo and saves it to file directly
        {
            CreateNewUnit();
        }
        if (Input.GetKeyDown(KeyCode.L))//loads all unitinfo from file to library
        {
            LoadUnits();
           
           
        }
        if (Input.GetKeyDown(KeyCode.K))//Loads all the cards from file into the library
        {
            LoadCards();
        }

        if (Input.GetKeyDown(KeyCode.M))//Tests fetching a card from the library
        {
            Card t = Libraries.instance.GetCard("Minion") ?? new Minion();
            Debug.Log(t.name);
        }

        if (Input.GetKeyDown(KeyCode.N))//tests fetching a unitInfo from the library
        {
            UnitInfo u = Libraries.instance.GetUnit("HunterClassGuy") ?? new UnitInfo();
            Debug.Log(u.name);
        }
    }

    private void CreateNewCardToLibrary()
    {
        Minion c = new Minion();
        
        c.cost = 2;
        c.name = c.name.ParseName();
        c.tooltip = new NSGameplay.Cards.Tooltip();
        c.tooltip.description = "The description.";
        c.tooltip.title = "Title of tooltip";
        c.tooltip.image_Path = "path of an image";
        c.attack = 4;
        c.baseDefence = 2;
        c.defence = 2;
        c.health = 20;
        c.utility = 0;

        //Saves the card into the library
        Libraries.instance.Save_Card_Local(c);
        //Saves the entire library to file
        Libraries.instance.Save_Cards_To_File();
    }

    private void CreateNewUnitToLibrary()
    {
        UnitInfo u = new UnitInfo(100, 5, 10, 1, "The first unit's name");
        u.name = "Hunter";

        //Saves the Unit into the library
        Libraries.instance.Save_Unit_Local(u);
        //Saves the entire library to file
        Libraries.instance.Save_Units_To_File();
    }

    private void CreateNewCard()
    {
        Minion c = new Minion();
        
        c.cost = 2;
        c.name = c.name.ParseName();
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
        UnitInfo u = new UnitInfo( 100, 5,10,1,"The first unit's name");
        u.name = "Hunter";
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
