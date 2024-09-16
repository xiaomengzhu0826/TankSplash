using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOneGameManager : MonoBehaviour
{
    public static LevelOneGameManager instance = null;

    [SerializeField] private Transform[] spawnPositons;

    [SerializeField] private GameObject[] playerPrefabs;

    [SerializeField] private PlayerInputManager playerInputManager;

    private PlayerInput _MainPlayerInput;

    private PlayerInput _SecondaryPlayerInput;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
       //InitMainPlayer();
    }

    private void InitMainPlayer()
    {
        playerInputManager.playerPrefab = playerPrefabs[0];
        playerInputManager.playerPrefab.transform.position = spawnPositons[0].position;
        _MainPlayerInput = PlayerInputManager.instance.JoinPlayer();
        DeviceManager.instance.AssignDeviceToPlayer(_MainPlayerInput);
        DeviceManager.instance.PrintOutUnassignedDevice();
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerInput.gameObject.name.Contains("MainPlayer"))
        { 
            playerInputManager.playerPrefab = playerPrefabs[1];
            playerInputManager.playerPrefab.transform.position = spawnPositons[1].position;
        }
        else
        {
             Camera[] cameras = Camera.allCameras;
            foreach (Camera camera in cameras)
            {
                if (camera.name == "MainPlayerCamera")
                {
                    Rect rect1 = camera.rect;
                    rect1.x = -0.5f;
                    camera.rect = rect1;
                }
                if (camera.name == "SecondaryPlayerCamera")
                {
                    Rect rect2 = camera.rect;
                    rect2.x = 0.5f;
                    camera.rect = rect2;
                }
            }
        }  
    }
}
