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
    private Dictionary<CardKeys, Card> Library = new Dictionary<CardKeys, Card>();
    
    //Loads the card library from file.
    public void Load_Card_Library (List<Card> list)
    {
        foreach (Card c in list)
        {
            Library[c.id] = c;
        }
    }

    //Retreive a specific card from the library.
    public Card GetCard(CardKeys key)
    {
        return Library[key];
    }


}
