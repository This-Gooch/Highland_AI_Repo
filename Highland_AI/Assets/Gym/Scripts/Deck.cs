using System.Collections.Generic;
/// <summary>
/// Class containing all the cards (Actions).
/// Mostly a holder but with functions to draw, shuffle and move cards.
/// Serializable for possible save functions.
/// </summary>
[System.Serializable]
public class Deck {


    #region Member Variables
    /// <summary>
    /// Main deck list of cards(actions) remaining(Not bruned).
    /// </summary>
    private List<Card> m_Deck = new List<Card>();
    //How many cards are left in the deck.
    public int m_CardsInDeck { get; private set; }

    /// <summary>
    /// List of cards discarded.
    /// </summary>
    private List<Card> m_Discards = new List<Card>();
    //How many cards are left in discards.
    public int m_CardsInDiscards { get; private set; }

    #endregion

    #region Main Functions
    //Checks how many cards are left in the deck. 

    //Removes the top card from the deck and returns it.
    public Card Draw_From_Deck()
    {
        m_CardsInDeck--;
        return m_Deck.Pop();
    }
    //Removes the top card from the discards and returns it.
    public Card Draw_From_Discards()
    {
        m_CardsInDiscards--;
        return m_Discards.Pop();
    }
    //Adds a Card to the Deck at the end index.
    public void Add_To_Deck_End(Card c)
    {
        m_CardsInDeck++;
        m_Deck.Add(c);
    }
    //Adds a Card to the Deck at the 0 index.
    public void Add_To_Deck_Start(Card c)
    {
        m_CardsInDeck++;
        m_Deck.Insert(0 , c);
    }
    //Adds a Card to the Discards at the end index.
    public void Add_To_Discards_End(Card c)
    {
        m_CardsInDiscards++;
        m_Discards.Add(c);
    }
    //Adds a Card to the Discards at the 0 index.
    public void Add_To_Discards_Start(Card c)
    {
        m_CardsInDiscards++;
        m_Discards.Insert(0 , c);
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
    //Saves the class's status with all the cards.
    public bool Save(string path)
    {
        //TODO: Implement save once design is set.
        return true;
    }
    //Loads the class from a saved file.
    public bool Load(string path)
    {
        //TODO: Implement load once design is set.
        return true;
    }
    
    #endregion

}
