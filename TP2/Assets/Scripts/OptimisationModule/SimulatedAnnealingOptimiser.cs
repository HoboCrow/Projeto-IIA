using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class SimulatedAnnealingOptimiser : OptimisationAlgorithm
{
    private List<int> newSolution = null;
    private int CurrentSolutionCost;
    public enum function { LOG10, GEOMETRIC, LINEAR, ARITH_GEO };
    public function SelectedFunction;
    [HideInInspector]
    public float InitialTemperature;
    public float Temperature;
    private float zero = Mathf.Pow(10, -6);// numbers bellow this value can be considered zero.

    string fileName = "Assets/Logs/" + System.DateTime.Now.ToString("ddhmmsstt") + "_SimulatedAnnealingOptimiser.csv";

    [SerializeField]
    [HideInInspector]
    private float logConst;
    [SerializeField]
    [HideInInspector]
    private float geoConst;
    [SerializeField]
    [HideInInspector]
    private float linearConst;

    protected override void Begin()
    {
        CreateFileSA(fileName);
        // Initialization
        // Temp inicializada fora
        InitialTemperature = Temperature;
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
        float rand = UnityEngine.Random.Range(0f,1f);
        Debug.Log($"Cost: {CurrentSolutionCost} vs newCost: {newSolutionCost}\n jumpProb: {jumpProb} vs rand:{rand}");
        
        if (newSolutionCost <= CurrentSolutionCost || jumpProb > rand)
        {
            // Para ter dados de ouput relevantes
            if (newSolutionCost < CurrentSolutionCost)
                BestSequenceIteration = CurrentNumberOfIterations;
            CurrentSolution = newSolution;
            CurrentSolutionCost = newSolutionCost;
        }

        Temperature = TemperatureSchedule(currTemp: Temperature, iter: CurrentNumberOfIterations);

        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, base.CurrentNumberOfIterations, CurrentSolutionCost, CurrentSolution, Temperature);
        base.CurrentNumberOfIterations++;
    }

    private float TemperatureSchedule([Optional] float currTemp, [Optional] int iter)
    {
        float newTemp;
        switch (SelectedFunction)
        {
            case function.LOG10:
                newTemp = logConst / (Mathf.Log(1 + iter,10));
                break;
            case function.GEOMETRIC:
                newTemp = InitialTemperature * Mathf.Pow(geoConst, iter);
                break;
            case function.LINEAR:
                newTemp = InitialTemperature - linearConst * iter;
                break;
            case function.ARITH_GEO:
                throw new NotImplementedException();
            default:
                throw new Exception($"Invalid Temperature function call : enum value {SelectedFunction}. Check if value is set.");
        }
        return newTemp > zero ? newTemp : zero;
    }
}

[CustomEditor(typeof(SimulatedAnnealingOptimiser))]
[CanEditMultipleObjects]
public class SimulatedAnnealingOptimiserEditor : Editor
{
    SerializedProperty logConstProperty;
    SerializedProperty geoConstProperty;
    SerializedProperty linearConstProperty;
    private void OnEnable()
    {
        logConstProperty = serializedObject.FindProperty("logConst");
        geoConstProperty = serializedObject.FindProperty("geoConst");
        linearConstProperty = serializedObject.FindProperty("linearConst");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        var sam = target as SimulatedAnnealingOptimiser;
        switch (sam.SelectedFunction)
        {
            case SimulatedAnnealingOptimiser.function.LOG10:
                EditorGUILayout.PropertyField(logConstProperty, new GUIContent("C"));
                break;
            case SimulatedAnnealingOptimiser.function.GEOMETRIC:
                EditorGUILayout.PropertyField(geoConstProperty, new GUIContent("alfa"));
                break;
            case SimulatedAnnealingOptimiser.function.LINEAR:
                EditorGUILayout.PropertyField(linearConstProperty, new GUIContent("n"));
                break;
            case SimulatedAnnealingOptimiser.function.ARITH_GEO:
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}