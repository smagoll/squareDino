using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> _wayPoints;
    [SerializeField]
    private PlayerController _playerController;

    private Waypoint _currentWaypoint;

    public void Launch()
    {
        NextWaypoint();
    }

    private void MoveToWaypoint(Waypoint waypoint)
    {
        _currentWaypoint = waypoint;
        _currentWaypoint.OnComplete += CompleteWaypoint;
        
        _playerController.MoveTo(waypoint.transform.position);
    }

    private void CompleteWaypoint()
    {
        _currentWaypoint.OnComplete -= CompleteWaypoint;
        
        NextWaypoint();
    }

    private void NextWaypoint()
    {
        var nextWaypoint = _wayPoints.FirstOrDefault(x => !x.IsComplete);

        if (nextWaypoint)
        {
            MoveToWaypoint(nextWaypoint);
        }
        else
        {
            FinishLevel();
        }
    }
    
    private void FinishLevel()
    {
        Debug.Log("Уровень завершен!");
    }

    private void ActivateWaypoint()
    {
        _currentWaypoint.Activate(_playerController);
    }

    private void OnEnable()
    {
        _playerController.OnStop += ActivateWaypoint;
    }
    
    private void OnDisable()
    {
        _playerController.OnStop -= ActivateWaypoint;
    }
}
