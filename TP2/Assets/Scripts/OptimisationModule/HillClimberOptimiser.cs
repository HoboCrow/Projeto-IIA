using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;


public class HillClimberOptimiser : OptimisationAlgorithm
{

    private int bestCost;
    private List<int> newSolution = null;
    

    string fileName = "Assets/Logs/" + System.DateTime.Now.ToString("ddhmmsstt") + "_HillClimberOptimiser.csv";


    protected override void Begin()
    {
        CreateFile(fileName);
        // Initialization
        CurrentSolution = GenerateRandomSolution(targets.Count);
        bestCost = Evaluate(CurrentSolution);
        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, base.CurrentNumberOfIterations, this.Evaluate(base.CurrentSolution), base.CurrentSolution);
        base.CurrentNumberOfIterations++;
    }

    protected override void Step()
    {
        newSolution = GenerateNeighbourSolution(CurrentSolution);
        int newSolutionCost = Evaluate(newSolution);

        if (newSolutionCost <= bestCost)
        {
            // Para ter dados de ouput
            if (newSolutionCost < bestCost) // so apontar quando melhora, não quando troca entre equivalentes
                BestSequenceIteration = CurrentNumberOfIterations;
            CurrentSolution = newSolution;
            bestCost = newSolutionCost;
        }

        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, base.CurrentNumberOfIterations, this.Evaluate(base.CurrentSolution), base.CurrentSolution);
        base.CurrentNumberOfIterations++;

    }

   

}
