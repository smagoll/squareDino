using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private WaypointNavigator _waypointNavigator;

    private void Start()
    {
        _waypointNavigator.Launch();
    }
}
