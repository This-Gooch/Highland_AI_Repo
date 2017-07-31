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

    UnityEngine.Vector3 GetTargetLocation();

    void ReceiveEffects(Effect[] args);
}

public interface IEffector
{
    int Use(int utilityAvailable, ITargetable[] target);
    void SendEffects(ITargetable target);
}