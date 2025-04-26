using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController
{
    private PlayerController _playerController;
    private Waypoint _currentWaypoint;
    private Level _level;

    public LevelController(Level level, Player player)
    {
        _level = level;
        _playerController = player.GetComponent<PlayerController>();
    }
    
    public void Launch()
    {
        NextWaypoint();
        _playerController.OnStop += ActivateWaypoint;
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
        var nextWaypoint = _level.WayPoints.FirstOrDefault(x => !x.IsComplete);

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
        GameEvents.RaiseComplete();
    }

    private void ActivateWaypoint()
    {
        _currentWaypoint.Activate(_playerController);
    }
    
    private void OnDestroy()
    {
        _playerController.OnStop -= ActivateWaypoint;
    }
}
