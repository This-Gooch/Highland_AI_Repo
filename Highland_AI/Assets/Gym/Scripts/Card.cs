﻿using System.Collections.Generic;
using NSGameplay.Cards;
using System.Xml;
using System.Xml.Serialization;

//XML root name in the file.
[XmlRoot("CardList")]
// include type class Unit
[XmlInclude(typeof(Card))]
//Class holding a list of units. Used for storing and loading purposes.
public class CardList
{

    [XmlArray("List")]
    public List<Card> cardList = new List<Card>();

    [XmlElement("Listname")]
    public string Listname { get; set; }

    // Constructor
    public CardList() { }

    public CardList(string name)
    {
        this.Listname = name;
    }

    public void Add(Card card)
    {
        cardList.Add(card);
    }
}

/// <summary>
/// Parent Class for all cards (Actions/Minion/Passives):
/// Serializable because most likely will be saved as XML, JSON or other for ease of edit/mod.
/// </summary>
[System.Serializable]
[XmlRoot("Card")]
public abstract class Card {

    //Te unique ID for the card.
    public CardKeys id;

    //owning unit
    public Unit m_OwningUnit { get; set; }

    //Tooltip information (May move this outside of the class).
    public Tooltip m_Tooltip;
    //Type of card. Once played it will trigger differently.
    public CardType m_type { get; protected set; }
    //Name of the card
    public string m_Name { get; private set; }
    //Cost for using the card.
    public int m_Cost { get; protected set; }
    //Needs to be implemented in the child class.
    public abstract void Play();
    
    public virtual int Burn()
    {   //Burn utility recovered bonus is half the cost rounded down with 1 being the minimum.
        //This is virtual so the cost/implementation can be changed in the child class.
        return (m_Cost / 2) > 0 ? (m_Cost / 2) : 1;
    }

}
//Minion
public class Minion: Card
{
    public Minion() { m_type = CardType.Minion; }


    public int m_Health { get; private set; }
    public int m_Defence { get; set; }
    public int m_BaseDefence { get; private set; }
    public int m_Attack { get; private set; }
    public int m_Utility { get; private set; }
    public bool m_Exhausted { get; private set; }
    public int m_OwningPlayer { get; private set; }
    private string m_PortraitImagePath;

    public override void Play()
    {
        //TODO: Implement play function for minion
    }
}
//Actions
public class Action : Card
{   
    public override void Play()
    {
        //TODO: Implement play function for actions.
    }   

}
//Passive Card
public class Passive : Card, IDuration
{
    public Passive() { m_type = CardType.PassiveEffect; }

    public override void Play()
    {
        //TODO: Implement play function for actions.
    }

    public void Increment()
    {
        if (m_Duration != null)
        {
            ++m_Duration;
        }
    }

    public void Decrement()
    {
        if (m_Duration != null)
        {
            --m_Duration;
        }
    }

    public void Reset()
    {
        m_Duration = m_StartingBaseDuration;
    }

    /// <summary>
    /// m_CallPhase: The phase/phases that this card's effect is called.
    /// Used as flags (START_TURN = 8, END_TURN = 128 -> so 136 = START_TURN & END_TURN)
    /// </summary>
    public ECallPhase m_CallPhase { get; protected set;/*Setting the call phase should never be done in code. should be set at card creation.*/ }
    //How long does this effect lasts. If null = lasts for ever.
    public int? m_Duration { get; set; }
    //base duration
    public int? m_StartingBaseDuration { get; private set; }
}
