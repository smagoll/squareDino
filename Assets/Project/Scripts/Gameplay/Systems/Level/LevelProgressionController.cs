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
        _playerController.OnStop += ActivateWaypoint;
        AdvanceNextWaypoint();
    }

    private void MoveToWaypoint(Waypoint waypoint)
    {
        _currentWaypoint = waypoint;
        _currentWaypoint.OnComplete += CompleteWaypoint;
        
        _playerController.MoveTo(waypoint.Place.position);
    }

    private void CompleteWaypoint()
    {
        _currentWaypoint.OnComplete -= CompleteWaypoint;
        
        AdvanceNextWaypoint();
    }

    private void AdvanceNextWaypoint()
    {
        var nextWaypoint = GetNextWaypoint();

        if (nextWaypoint)
        {
            MoveToWaypoint(nextWaypoint);
            Debug.Log("next waypoint - " + nextWaypoint.gameObject.name);
        }
        else
        {
            CompleteLevel();
        }
    }
    
    private Waypoint GetNextWaypoint()
    {
        return _level.WayPoints.FirstOrDefault(x => !x.IsComplete);
    }
    
    private void CompleteLevel()
    {
        GameEvents.RaiseComplete();
    }

    private void ActivateWaypoint()
    {
        Debug.Log("activate " + _currentWaypoint.gameObject.name);
        _currentWaypoint.Activate(_playerController);
    }
    
    public void Cleanup()
    {
        _playerController.OnStop -= ActivateWaypoint;
    }
}
