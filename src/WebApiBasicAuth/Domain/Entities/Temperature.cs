namespace WebApiBasicAuth.Domain.Entities;

public class Temperature
{
    public float Min { get; init; }
    public float Max { get; init; }

    public Temperature(float min, float max)
    {
        Min = min;
        Max = max;
    }
}
