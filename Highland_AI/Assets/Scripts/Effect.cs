using NSGameplay;

[System.Serializable]
public struct Effect : IDuration
{
    public EEffect type;
    public int value;

    public int originalDuration;
    public int duration;

    public Effect(EEffect type, int value, int startingDuration = 0)
    {
        this.type = type;
        this.value = value;
        this.originalDuration = startingDuration;
        this.duration = originalDuration;
    }

    public void Increment(int modifier)
    {
        duration += modifier;
    }

    public void SetDuration(int duration)
    {
        this.duration = duration;
    }

    public void Decrement(int modifier)
    {
        duration -= modifier;
    }

    public void Reset()
    {
        duration = originalDuration;
    }


}