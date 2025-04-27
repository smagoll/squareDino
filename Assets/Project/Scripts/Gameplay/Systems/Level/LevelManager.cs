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
    private Pistol _pistol;
    [SerializeField]
    private Level _level;
    [SerializeField]
    private Player _playerPrefab;

    private LevelProgressionController _levelProgressionController;

    private void Start()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        var player = SpawnPlayer(_pistol);
        player.transform.rotation = _level.GetInitialRotation();

        SetupCamera(player); 
        
        _levelProgressionController = new LevelProgressionController(_level, player);
        
        StartCoroutine(WaitForPlayerInput(_levelProgressionController, player));
    }
    
    private Player SpawnPlayer(IWeapon weapon)
    {
        Player player = Instantiate(
            _playerPrefab,
            _level.WayPoints[0].transform.position,
            Quaternion.identity,
            _level.transform
        );
        player.Init(weapon);
        player.DisableControls();
        return player;
    }
    
    private void SetupCamera(Player player)
    {
        _virtualCamera.Follow = player.transform;
        _virtualCamera.LookAt = player.transform;
    }
    
    private IEnumerator WaitForPlayerInput(LevelProgressionController levelProgressionController, Player player)
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        player.EnableControls();
        levelProgressionController.StartProgression();
    }

    private void Complete()
    {
        EndLevel();
        SceneManager.LoadScene("Game");
    }

    private void Lose()
    {
        EndLevel();
        SceneManager.LoadScene("Game");
    }

    private void EndLevel()
    {
        _levelProgressionController?.Cleanup();
        _levelProgressionController = null;
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
