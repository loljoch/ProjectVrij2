using UnityEngine;

public class FPD_PercentageAttribute : PropertyAttribute
{
    public readonly float Min;
    public readonly float Max;
    public readonly bool from0to100;
    public readonly bool editableValue;

    public FPD_PercentageAttribute(float min, float max, bool goOver100Perc = false, bool editable = true)
    {
        Min = min;
        Max = max;
        from0to100 = !goOver100Perc;
        editableValue = editable;
    }
}

