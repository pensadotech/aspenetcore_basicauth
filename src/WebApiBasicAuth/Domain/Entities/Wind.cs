namespace WebApiBasicAuth.Domain.Entities;

public class Wind
{
    public float Speed { get; init; }
    public float Degrees { get; init; }

    public Wind(float speed, float degrees)
    {
        Speed = speed;
        Degrees = degrees;
    }

}
