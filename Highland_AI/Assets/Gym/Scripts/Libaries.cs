using NSGameplay;
using NSGameplay.Cards;
using System.Collections.Generic;
using UnityEngine;

public class Libaries : MonoBehaviour {

    public static Libaries instance;

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
    private Dictionary<ECardKeys, Card> Library = new Dictionary<ECardKeys, Card>();

    //Repo for unit data.
    private Dictionary<EUnitIDs, Unit> Barrack = new Dictionary<EUnitIDs, Unit>();
    
    //Loads the card library from file.
    public void Load_Card_Library (List<Card> list)
    {
        foreach (Card c in list)
        {
            Library[c.id] = c;
        }
    }

    //Loads the unit library from file.
    public void Load_Unit_Library(List<Unit> list)
    {
        foreach (Unit c in list)
        {
            Barrack[c.id] = c;
        }
    }

    //Retreive a specific card from the library.
    public Card GetCard(ECardKeys key)
    {
        return Library[key];
    }


}
