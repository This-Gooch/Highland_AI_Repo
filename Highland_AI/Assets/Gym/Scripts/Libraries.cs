using NSGameplay;
using NSGameplay.Cards;
using System.Collections.Generic;
using UnityEngine;

public class Libraries : MonoBehaviour {

    public static Libraries instance;

    [SerializeField]
    [Tooltip("Full path location of the cards library file.")]
    private string _Library_Cards_Path;

    [SerializeField]
    [Tooltip("Full path location of the cards library file.")]
    private string _Library_Units_Path;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;


    }

    //Repository of all cards data.
    //Images will be loaded seperatly from path.
    private Dictionary<string, Card> Library_Card = new Dictionary<string, Card>();

    //Repo for unit data.
    private Dictionary<string, UnitInfo> Library_Unit = new Dictionary<string, UnitInfo>();
    
    //Loads the card library from file.
    public void Load_Card_Library (List<Card> list)
    {
        foreach (Card c in list)
        {
            Library_Card[c.id] = c;
            Debug.Log("Loading : " + c.name );
        }        
    }

    public void Load()
    {
        //Units
        XMLDataSerializer.LoadUnits("Assets/Data/Units.xml");
        //Cards
        XMLDataSerializer.LoadCards("Assets/Data/Cards.xml");
        //Todo Abilities
        //XMLDataSerializer.LoadAbilities("Assets/Data/Abilities.xml");
    }

    //Loads the unit library from file.
    public void Load_Unit_Library(List<UnitInfo> list)
    {
        foreach (UnitInfo c in list)
        {
            Library_Unit[c.name] = c;
            Debug.Log("Loading : " + c.name);
        }
    }

    //Add or update a card entry.
    public bool Save_Card_Local(Card c)
    {
        bool IsNewEntry = true;
        if (Library_Card.ContainsKey(c.id))
        {
            Library_Card[c.id] = c;
            IsNewEntry = false;
        }
        else { Library_Card.Add(c.id, c); }
        return IsNewEntry;
    }

    //Save Card Library to file
    public int Save_Cards_To_File()
    {
        int count = 0;
        CardList list = new CardList("main");
        foreach (KeyValuePair<string, Card> c in Library_Card)
        {
            list.cardList.Add(c.Value);
            ++count;
        }
        XMLDataSerializer.SaveCards(list, "Assets/Data/Cards.xml");
        Debug.Log("Saving " + count + " cards to file.");
        return count;
    }

    //Add or update and a Unit entry.
    public bool Save_Unit_Local(UnitInfo u)
    {
        bool IsNewEntry = false;
        if (Library_Unit[u.name] == null)
        {
            IsNewEntry = true;
        }
        Library_Unit[u.name] = u;
        return IsNewEntry;
    }

    //Save the Unit Library to file.
    public int Save_Units_To_File()
    {
        int count = 0;
        UnitList list = new UnitList("main");
        foreach (KeyValuePair<string, UnitInfo> u in Library_Unit)
        {
            list.unitList.Add(u.Value);
            ++count;
        }
        XMLDataSerializer.SaveUnits(list, _Library_Units_Path);
        Debug.Log("Saving " + count + " units to file.");
        return count;
    }

    //Retreive a specific card from the library.
    public Card GetCard(string key)
    {
        return Library_Card[key];
    }
    //Retreive a specific Unit from the library.
    public UnitInfo GetUnit(string key)
    {
        return Library_Unit[key];
    }

}
