using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelProgressionController
{
    private PlayerController _playerController;
    private Waypoint _currentWaypoint;
    private Level _level;

    public LevelProgressionController(Level level, Player player)
    {
        _level = level;
        _playerController = player.GetComponent<PlayerController>();
    }
    
    public void StartProgression()
    {
        AdvanceNextWaypoint();
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
        
        AdvanceNextWaypoint();
    }

    private void AdvanceNextWaypoint()
    {
        var nextWaypoint = _level.WayPoints.FirstOrDefault(x => !x.IsComplete);

        if (nextWaypoint)
        {
            MoveToWaypoint(nextWaypoint);
        }
        else
        {
            CompleteLevel();
        }
    }
    
    private void CompleteLevel()
    {
        GameEvents.RaiseComplete();
    }

    private void ActivateWaypoint()
    {
        _currentWaypoint.Activate(_playerController);
    }
    
    public void Cleanup()
    {
        _playerController.OnStop -= ActivateWaypoint;
    }
}
