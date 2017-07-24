using UnityEngine.EventSystems;
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
