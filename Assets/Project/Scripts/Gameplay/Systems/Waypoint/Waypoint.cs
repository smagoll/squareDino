using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public event Action OnComplete;
    
    [SerializeField]
    private Enemy[] _enemies;
    
    public bool IsComplete { get; private set; }

    public void Activate(PlayerController playerController)
    {
        var aliveEnemies = GetAliveEnemies();
    
        if (aliveEnemies.Length > 0)
        {
            Vector3 centerPosition = GetEnemiesCenterPosition(aliveEnemies);

            playerController.RotateTowards(centerPosition);

            foreach (var enemy in aliveEnemies)
            {
                enemy.MoveTo(playerController.transform.position);
            }
        }
        
        StartCoroutine(WaitComplete());
    }
    
    private Enemy[] GetAliveEnemies()
    {
        return _enemies.Where(enemy => !enemy.IsDead).ToArray();
    }
    
    private Vector3 GetEnemiesCenterPosition(Enemy[] enemies)
    {
        if (enemies.Length == 0)
            return transform.position;
            
        Vector3 centerPosition = Vector3.zero;
        foreach (var enemy in enemies)
        {
            centerPosition += enemy.transform.position;
        }
        centerPosition /= enemies.Length;
        
        return centerPosition;
    }
    
    private IEnumerator WaitComplete()
    {
        while (_enemies.Any(x => !x.IsDead))
        {
            yield return new WaitForSeconds(0.2f);
        }
    
        Complete();
    }
    
    private void Complete()
    {
        IsComplete = true;
        
        OnComplete?.Invoke();
    }
}
