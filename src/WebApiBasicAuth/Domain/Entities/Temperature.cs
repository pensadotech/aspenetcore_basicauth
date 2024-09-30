namespace WebApiBasicAuth.Domain.Entities;

// Entity to store Temperature
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
