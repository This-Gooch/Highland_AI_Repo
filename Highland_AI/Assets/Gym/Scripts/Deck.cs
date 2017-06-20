using System.Collections;
using System.Collections.Generic;

public class Deck {

    #region Member Variables
    /// <summary>
    /// Main deck list of cards(actions) remaining(Not bruned).
    /// </summary>
    List<Card> m_Deck = new List<Card>();

    /// <summary>
    /// List of cards discarded.
    /// </summary>
    List<Card> m_Discards = new List<Card>();
    #endregion

    #region Actions
    //Removes the top card from the deck and returns it.
    public Card Draw_From_Deck()
    {
        return m_Deck.Pop();
    }
    //Removes the top card from the discards and returns it.
    public Card Draw_From_Discards()
    {
        return m_Discards.Pop();
    }

    #endregion

    #region Utility Functions
    //Shuffle function for the deck.
    public void Shuffle_Deck()
    {
        //Early exit if the deck has 1 or fewer cards.
        if (m_Deck.Count < 2)
        {
            return;
        }
        m_Deck.Shuffle();
    }
    //Shuffle function for the discards.
    public void Shuffle_Discards()
    {
        //Early exit if the discards have 1 or fewer cards.
        if (m_Discards.Count < 2)
        {
            return;
        }
        m_Discards.Shuffle();
    }
    #endregion

}
