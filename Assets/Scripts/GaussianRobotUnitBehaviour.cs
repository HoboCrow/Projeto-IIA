using System;
using System.Collections;
using UnityEngine;

public class GaussianRobotUnitBehaviour : RobotUnit
{
    public float weightResource;
    public float resourceValue;
    public float resouceAngle;

    public float weightObstacle;
    public float obstacleValue;
    public float obstacleAngle;

    void Update()
    {

        // get sensor data
        resouceAngle = resourcesDetector.GetAngleToClosestObject();
        resourceValue = weightResource * resourcesDetector.GetGaussianOutput();
        Debug.Log("Strengh:" + resourcesDetector.GetLinearOuput() + " Gauss: " + resourcesDetector.GetGaussianOutput());
        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards
        
        // Meta 1 Analogous to the code above, same logic
        obstacleAngle = blockDetector.GetAngleToClosestObject();
        obstacleValue = weightObstacle * blockDetector.GetGaussianOutput();
        // O weightObstacle will be negative -> agent moves away from the obstacle
        applyForce(obstacleAngle, obstacleValue);
    }


}






