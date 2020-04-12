using System;
using System.Collections;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [Header("Type of sensor:")]
    public DetectorScript detector;
    public float weight;
    public float output;
    public float angle;
    public Function function = Function.LINEAR;
    [Range(0, 1)]
    public float xEsquerda = 0;
    [Range(0, 1)]
    public float xDireita = 1f;
    [Range(0, 1)]
    public float yInferior = 0;
    [Range(0, 1)]
    public float ySuperior = 1f;
    //public RobotUnit unit;
    void FixedUpdate()
    {
        angle = detector.GetAngleToClosestObject();
        if (detector.strength == 0)
        {
            output = 0; // No activation
            return;
        }
        // Limiares direita e esquerda
        if (detector.strength < xEsquerda)
            output = yInferior;
        else if (detector.strength > xDireita)
            output = yInferior;
        else
            switch (function)
            {
                case Function.LINEAR:
                    output = detector.GetLinearOuput();
                    break;
                case Function.LOGARITHMIC:
                    output = detector.GetLogaritmicOutput();
                    break;
                case Function.GAUSSIAN:
                    output = detector.GetGaussianOutput();
                    break;
                case Function.CUBIC:
                    output = detector.GetCubicOutput();
                    break;
                default:
                    break;
            }
        // Limiares superior e inferior
        if (output < yInferior) output = yInferior;
        else if (output > ySuperior) output = ySuperior;

        GetComponent<RobotUnit>().applyForce(angle, output * weight); // go towards
    }

    public enum Function
    {
        LINEAR, LOGARITHMIC, GAUSSIAN, CUBIC
    };
}





