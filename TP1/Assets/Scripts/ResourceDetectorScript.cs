using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDetectorScript : DetectorScript
{
    public override void Start()
    {
        base.Start();
        detectTag = "Pickup";
    }
}
