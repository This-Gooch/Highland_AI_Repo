/// <summary>
/// Interface that lets you increment decrement
/// a duration value.
/// </summary>
internal interface IDuration
{
    void Increment(int modifier);

    void SetDuration(int duration);

    void Decrement(int modifier);

    void Reset();
}