using System;
using System.Collections;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [Header("Type of sensor:")]
    public DetectorScript detector;
    public float weight;
    public float value;
    public float angle;
    public Function function = Function.LINEAR;
    [Range(0, 1)]
    public float xMinLim = 0;
    [Range(0, 1)]
    public float xMaxLim = 1f;
    [Range(0, 1)]
    public float yMinLim = 0;
    [Range(0, 1)]
    public float yMaxLim = 1f;
    public RobotUnit unit;
    void Update()
    {
        angle = detector.GetAngleToClosestObject();
        if (detector.strength < xMinLim) value = yMinLim;
        else if (detector.strength > xMaxLim) value = yMinLim;
        else
            switch (function)
            {
                case Function.LINEAR:
                    value = weight * detector.GetLinearOuput();
                    break;
                case Function.LOGARITHMIC:
                    if (xMinLim < 0.01f) xMinLim = 0.01f;
                    value = weight * detector.GetLogaritmicOutput();
                    break;
                case Function.GAUSSIAN:
                    value = weight * detector.GetGaussianOutput();
                    break;
                default:
                    break;
            }
        if (value < yMinLim) value = yMinLim;
        else if (value > yMaxLim) value = yMaxLim;

        unit.applyForce(angle, value); // go towards
    }

    public enum Function
    {
        LINEAR, LOGARITHMIC, GAUSSIAN
    };
}





