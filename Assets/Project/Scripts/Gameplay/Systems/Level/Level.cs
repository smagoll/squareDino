using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> _wayPoints;
    
    public List<Waypoint> WayPoints => _wayPoints;
}