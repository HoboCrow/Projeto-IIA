using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedAnnealingOptimiser : OptimisationAlgorithm
{
    private List<int> newSolution = null;
    private int CurrentSolutionCost;
    private enum function { LOG10 };
    [SerializeField]
    private function SelectedFunction;
    public float Temperature;
    private float zero = Mathf.Pow(10, -6);// numbers bellow this value can be considered zero.

    string fileName = "Assets/Logs/" + System.DateTime.Now.ToString("ddhmmsstt") + "_SimulatedAnnealingOptimiser.csv";


    protected override void Begin()
    {
        CreateFileSA(fileName);
        // Initialization
        // Temp inicializada fora
        CurrentSolution = GenerateRandomSolution(targets.Count);
        CurrentSolutionCost = Evaluate(CurrentSolution);

        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, base.CurrentNumberOfIterations, CurrentSolutionCost, CurrentSolution, Temperature);
        base.CurrentNumberOfIterations++;
    }

    protected override void Step()
    {

        newSolution = GenerateNeighbourSolution(CurrentSolution);
        int newSolutionCost = Evaluate(newSolution);

        float jumpProb = Mathf.Pow((float)System.Math.E, (CurrentSolutionCost - newSolutionCost) / Temperature);

        if (newSolutionCost <= CurrentSolutionCost || jumpProb > UnityEngine.Random.Range(0, 1))
        {
            CurrentSolution = newSolution;
            CurrentSolutionCost = newSolutionCost;
        }

        Temperature = TemperatureSchedule(Temperature);

        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, base.CurrentNumberOfIterations, CurrentSolutionCost, CurrentSolution, Temperature);
        base.CurrentNumberOfIterations++;
    }

    private float TemperatureSchedule(float temp)
    {
        float newTemp;
        switch (SelectedFunction)
        {
            case function.LOG10:
                newTemp = temp / 1.1f;
                break;
            default:
                throw new Exception($"Invalid Temperature function : enum value {SelectedFunction}. Check if value is set.");
        }
        return newTemp > zero ? newTemp : zero;
    }
}