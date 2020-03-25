using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveUnitBehaviour : UnitBehaviour
{
    public bool ignoreThreshold = true;
    private bool attackMode = false;
    List<UnitBehaviour> disabledBeahaviours = new List<UnitBehaviour>();
    void FixedUpdate()
    {
        angle = detector.GetAngleToClosestObject();
        if (detector.strength == 0 || GetComponent<RobotUnit>().resourcesGathered < 5)
        {
            if (attackMode)
            {
                attackMode = false;
                disabledBeahaviours.ForEach(b => b.enabled = true);
            }
            output = 0; // No activation
            return;
        }

        if (!attackMode)
        {
            attackMode = true;
            // Disable all other behaviours
            foreach (var b in GetComponents<UnitBehaviour>())
            {
                if (b == this) continue;
                b.enabled = false;
                disabledBeahaviours.Add(b);
            }
        }

        // Limiares direita e esquerda
        if (!ignoreThreshold && detector.strength < xEsquerda)
            output = yInferior;
        else if (!ignoreThreshold && detector.strength > xDireita)
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
        if (!ignoreThreshold &&  output < yInferior) output = yInferior;
        else if (!ignoreThreshold && output > ySuperior) output = ySuperior;

        GetComponent<RobotUnit>().applyForce(angle, output * weight); // go towards
    }
}





