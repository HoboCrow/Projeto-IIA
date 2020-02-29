using System;
using System.Collections;
using UnityEngine;

public class LinearRobotUnitBehaviour : RobotUnit
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
        resouceAngle = resourcesDetector.GetAngleToClosestResource();

        resourceValue = weightResource * resourcesDetector.GetLinearOuput();

        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards

        obstacleAngle = blockDetector.GetAngleToClosestObstacle();

        obstacleValue = weightObstacle * blockDetector.GetLinearOuput();

        applyForce(obstacleAngle, obstacleValue);
        

    }


}






