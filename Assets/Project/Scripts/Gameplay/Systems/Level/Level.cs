using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Waypoint[] _wayPoints;
    
    public Waypoint[] WayPoints => _wayPoints;
    
    public Vector3 GetInitialDirection()
    {
        if (_wayPoints.Length < 2)
            return Vector3.forward;
            
        return (_wayPoints[1].transform.position - _wayPoints[0].transform.position).normalized;
    }
    
    public Quaternion GetInitialRotation()
    {
        return Quaternion.LookRotation(GetInitialDirection());
    }
}