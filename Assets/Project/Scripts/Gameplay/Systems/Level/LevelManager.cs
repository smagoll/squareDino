using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private PoolSystem _poolSystem;
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    private Level _level;
    [SerializeField]
    private Player _playerPrefab;

    private void Start()
    {
        IWeapon weapon = new Pistol(1);
        var pool = _poolSystem.CreatePool();
        weapon.Init(pool);
        
        var player = Instantiate(_playerPrefab, _level.WayPoints[0].transform.position, Quaternion.identity, _level.transform);
        player.Init(weapon);
        player.DisableControls();

        _virtualCamera.Follow = player.transform;
        _virtualCamera.LookAt = player.transform;
        
        LevelController levelController = new LevelController(_level, player);
        
        StartCoroutine(WaitForScreenTap(levelController, player));
    }
    
    private IEnumerator WaitForScreenTap(LevelController levelController, Player player)
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        player.EnableControls();
        levelController.Launch();
    }

    private void Complete()
    {
        SceneManager.LoadScene("Game");
    }

    private void Lose()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnEnable()
    {
        GameEvents.OnComplete += Complete;
        GameEvents.OnLose += Lose;
    }
    
    private void OnDisable()
    {
        GameEvents.OnComplete -= Complete;
        GameEvents.OnLose -= Lose;
    }
}
