using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDetectorScript : DetectorScript
{
    public override void Start()
    {
        base.Start();
        detectTag = "Player";
    }


    public override List<ObjectInfo> GetVisibleObjects(string objectTag)
    {
        RaycastHit hit;
        List<ObjectInfo> objectsInformation = new List<ObjectInfo>();
        //temp
        Vector3 offset = new Vector3(0, -0.5f, 0);
        for (int i = 0; i * angleOfSensors < 360f; i++)
        {
            if (debug_mode) Debug.DrawRay(this.transform.position+offset, Quaternion.AngleAxis(-angleOfSensors * i, initialTransformUp) * initialTransformFwd * rangeOfSensors, scanRayColor);
            if (Physics.Raycast(this.transform.position+offset, Quaternion.AngleAxis(-angleOfSensors * i, initialTransformUp) * initialTransformFwd, out hit, rangeOfSensors))
            {

                if (hit.transform.gameObject.CompareTag(objectTag))
                {
                    if (debug_mode)
                    {
                        Debug.DrawRay(this.transform.position+offset, Quaternion.AngleAxis((-angleOfSensors * i), initialTransformUp) * initialTransformFwd * hit.distance, detectRayColor);
                    }
                    ObjectInfo info = new ObjectInfo(hit.distance, angleOfSensors * i + 90);
                    objectsInformation.Add(info);
                }
            }
        }

        objectsInformation.Sort();

        return objectsInformation;
    }
}
