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
        foreach (var enemy in _enemies)
        {
            enemy.MoveTo(playerController.transform.position);
        }
        
        StartCoroutine(WaitComplete());
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
