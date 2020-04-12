using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetectorScript : DetectorScript
{
    public override void Start()
    {
        base.Start();
        detectTag = "Wall";
    }
}
