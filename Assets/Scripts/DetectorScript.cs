using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectorScript : MonoBehaviour
{
    public float angleOfSensors = 10f;
    public float rangeOfSensors = 0.1f;
    protected Vector3 initialTransformUp;
    protected Vector3 initialTransformFwd;
    public float strength;
    public float angle;
    public int numObjects;
    public bool debug_mode;

    protected string detectTag = "";
    // Start is called before the first frame update
    public virtual void Start()
    {

        initialTransformUp = this.transform.up;
        initialTransformFwd = this.transform.forward;
    }

    // FixedUpdate is called at fixed intervals of time
    void FixedUpdate()
    {
        ObjectInfo anObject;
        anObject = GetClosestPickup();
        if (anObject != null)
        {
            angle = anObject.angle;
            strength = 1.0f / (anObject.distance + 1.0f);
        }
        else
        { // no object detected
            strength = 0;
            angle = 0;
        }

    }

    public float GetAngleToClosestObject()
    {
        return angle;
    }


    public float GetLinearOuput()
    {
        return strength;
    }

    public float GetGaussianOutput()
    {
        return (float)Math.Pow(Math.E, -Math.Pow((0.5 * (strength - 0.5)) / 0.12, 2));
    }

    public float GetLogaritmicOutput()
    {
        // YOUR CODE HERE
        throw new NotImplementedException();
    }


    public ObjectInfo[] GetVisiblePickups()
    {
        return (ObjectInfo[])GetVisibleObjects(detectTag).ToArray();
    }

    public ObjectInfo GetClosestPickup()
    {
        ObjectInfo[] a = (ObjectInfo[])GetVisibleObjects(detectTag).ToArray();
        if (a.Length == 0)
        {
            return null;
        }
        return a[a.Length - 1];
    }

    public List<ObjectInfo> GetVisibleObjects(string objectTag)
    {
        RaycastHit hit;
        List<ObjectInfo> objectsInformation = new List<ObjectInfo>();

        for (int i = 0; i * angleOfSensors < 360f; i++)
        {
            if (Physics.Raycast(this.transform.position, Quaternion.AngleAxis(-angleOfSensors * i, initialTransformUp) * initialTransformFwd, out hit, rangeOfSensors))
            {

                if (hit.transform.gameObject.CompareTag(objectTag))
                {
                    if (debug_mode)
                    {
                        Debug.DrawRay(this.transform.position, Quaternion.AngleAxis((-angleOfSensors * i), initialTransformUp) * initialTransformFwd * hit.distance, Color.red);
                    }
                    ObjectInfo info = new ObjectInfo(hit.distance, angleOfSensors * i + 90);
                    objectsInformation.Add(info);
                }
            }
        }

        objectsInformation.Sort();

        return objectsInformation;
    }

    private void LateUpdate()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
    }
}

