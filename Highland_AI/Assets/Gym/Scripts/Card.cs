using System.Collections.Generic;
using NSGameplay.Cards;
using System.Xml;
using System.Xml.Serialization;
using NSGameplay;

//XML root name in the file.
[XmlRoot("CardList")]
// include type class Unit
[XmlInclude(typeof(Card))]
//Class holding a list of units. Used for storing and loading purposes.
public class CardList
{

    [XmlElement("Listname")]
    public string Listname { get; set; }

    [XmlArray("List"), XmlArrayItem(ElementName = "Card", Type = typeof(Card))]
    public List<Card> cardList = new List<Card>();
    

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
    public ECardKeys id { get; set; }

    //owning unit might be useless
    public EUnitIDs owningUnit { get; set; }

    //Tooltip information (May move this outside of the class).
    public Tooltip tooltip { get; set; }
    //Type of card. Once played it will trigger differently.
    public ECardType type { get; set; }
    //Name of the card
    public string name { get; set; }
    //Cost for using the card.
    public int cost { get; set; }
    //Needs to be implemented in the child class.
    public abstract void Play();
    
    public virtual int Burn()
    {   //Burn utility recovered bonus is half the cost rounded down with 1 being the minimum.
        //This is virtual so the cost/implementation can be changed in the child class.
        return (cost / 2) > 0 ? (cost / 2) : 1;
    }

   

}
//Minion
[System.Serializable]
[XmlRoot("Minion")]
public class Minion: Card
{
    public Minion() { type = ECardType.Minion; }


    public int health { get; set; }
    public int defence { get; set; }
    public int baseDefence { get; set; }
    public int attack { get; set; }
    public int utility { get; set; }
    public bool exhausted { get; set; }
    public int owningPlayer { get; private set; }
    public string portraitPath { get; set; }

    public override void Play()
    {
        //TODO: Implement play function for minion
    }
}
//Actions
[System.Serializable]
[XmlRoot("Action")]
public class Action : Card
{   

    public int damageOut { get; set; }
    public int healOut { get; set; }
    public int damageIn { get; set; }
    public int healIn { get; set; }
    public int armorUpIn { get; set; }
    public int armorDownIn { get; set; }
    public int armorUpOut { get; set; }
    public int armorDownOut { get; set; }
    public int owningPlayer { get; private set; }
    public string portraitPath { get; set; }

    public override void Play()
    {
        //TODO: Implement play function for actions.
    }   

}
//Passive Card
[System.Serializable]
[XmlRoot("Passive")]
public class Passive : Card, IDuration
{
    //This will probably change has we may need different types.
    public Passive() { type = ECardType.PassiveEffect; }

    public override void Play()
    {
        //TODO: Implement play function for actions.
    }

    public void Reset()
    {
        m_Duration = m_StartingBaseDuration;
    }

    public void Increment(int modifier)
    {
        m_Duration += 1 * modifier;
    }

    public void SetDuration(int duration)
    {
        m_Duration = duration;
    }

    public void Decrement(int modifier)
    {
        m_Duration -= 1 * modifier;
    }

    /// <summary>
    /// m_CallPhase: The phase/phases that this card's effect is called.
    /// Used as flags (START_TURN = 8, END_TURN = 128 -> so 136 = START_TURN & END_TURN)
    /// </summary>
    public ECallPhase m_CallPhase { get; protected set;/*Setting the call phase should never be done in code. should be set at card creation.*/ }

    public ECallPriority m_Priority { get; protected set; }

    //How long does this effect lasts. If null = lasts for ever.
    public int? m_Duration { get; set; }
    //base duration
    public int? m_StartingBaseDuration { get; private set; }
}
