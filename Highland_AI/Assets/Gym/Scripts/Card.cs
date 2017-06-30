using NSGameplay.Cards;
/// <summary>
/// Parent Class for all cards (Actions):
/// Serializable because most likely will be saved as XML, JSON or other for ease of edit/mod.
/// </summary>
[System.Serializable]
public abstract class Card {

    /// <summary>
    /// m_CallPhase: The phase/phases that this card's effect is called.
    /// Used as flags (START_TURN = 8, END_TURN = 128 -> so 136 = START_TURN & END_TURN)
    /// </summary>
    public ECallPhase m_CallPhase { get; private set;/*Setting the call phase should never be done in code. should be set at card creation.*/ }

    //owning unit
    public Unit m_OwningUnit { get; set; }

    //Tooltip information (May move this outside of the class).
    public Tooltip m_Tooltip;
    //Type of card. Once played it will trigger differently.
    public CardType m_type { get; private set; }
    //Cost for using the card.
    public int m_Cost { get; private set; }

    public abstract void Play();
    public virtual int Burn()
    {//Burn utility recovered bonus is half the cost rounded down with 1 being the minimum.
        return (m_Cost / 2) > 0 ? (m_Cost / 2) : 1;
    }

}

[System.Serializable]
public class Minion: Card
{
    public int m_Health { get; private set; }
    public int m_Defence { get; set; }
    public int m_BaseDefence { get; private set; }
    public int m_Attack { get; private set; }
    public int m_Utility { get; private set; }
    public bool m_Exhausted { get; private set; }
    public int m_OwningPlayer { get; private set; }
    private string m_PortraitImagePath;

    public override void Play() { }
}


public enum CardType
{
    NULL,
    DEBUG,
    Instant,
    PassiveEffect,
    Minion,
    Secret,
    ERROR
}

