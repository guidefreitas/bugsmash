﻿public static class ExtensionMethods
{
    public static decimal Map (this decimal value, decimal fromSource, decimal toSource, decimal fromTarget, decimal toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

	public static float Map (this float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public static float Map (this int value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}