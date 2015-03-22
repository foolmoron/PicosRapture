using UnityEngine;

public class SplitRangeAttribute : PropertyAttribute {
    public float Start { get; private set; }
    public float Midpoint { get; private set; }
    public float End { get; private set; }

    public SplitRangeAttribute(float start, float midpoint, float end) {
        Start = start;
        Midpoint = midpoint;
        End = end;
    }
}