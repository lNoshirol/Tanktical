using UnityEngine;

public class TwoWayRangeAttribute : PropertyAttribute
{
    public float Max;
    public float Min;
    
    public TwoWayRangeAttribute(float min, float max)
    {
        this.Min = min;
        this.Max = max;
    }
}
