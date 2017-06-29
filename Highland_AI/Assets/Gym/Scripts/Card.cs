using NSGameplay.Cards;
/// <summary>
/// Parent Class for all cards (Actions):
/// Serializable because most likely will be saved as XML, JSON or other for ease of edit/mod.
/// </summary>
[System.Serializable]
public class Card {

    /// <summary>
    /// m_CallPhase: The phase/phases that this card's effect is called.
    /// Used as flags (START_TURN = 8, END_TURN = 128 -> so 136 = START_TURN & END_TURN)
    /// </summary>
    public ECallPhase m_CallPhase { get; private set;/*Setting the call phase should never be done in code. should be set at card creation.*/ }

    //Tooltip information (May move this outside of the class).
    public Tooltip m_Tooltip;

    //TODO add relevant variables/functions for this class based on design.

}
