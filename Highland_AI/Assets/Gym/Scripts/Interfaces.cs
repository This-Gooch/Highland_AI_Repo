using UnityEngine.EventSystems;
using NSGameplay;
/// <summary>
/// Interface that lets you increment decrement
/// a duration value.
/// </summary>
public interface IDuration
{
    void Increment(int modifier);

    void SetDuration(int duration);

    void Decrement(int modifier);

    void Reset();
}


public interface ITargetable
{
    void Select();
    void Deselect();

    void ReceiveEffects(int numberOfEffects, int[] effectModifier, params Effect[] args);
}