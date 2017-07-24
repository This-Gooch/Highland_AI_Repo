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

public interface ISelectable : IEventSystemHandler
{
    void OnMouseOver(PointerEventData eventData);
    void OnMouseClick(PointerEventData eventData);
}