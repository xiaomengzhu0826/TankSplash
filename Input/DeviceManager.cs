using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class DeviceManager : MonoBehaviour
{

    public static DeviceManager instance = null;
    private List<Keyboard> unassignedKeyboards = new List<Keyboard>();
    private List<Mouse> unassignedMouses = new List<Mouse>();
    private List<Gamepad> unassignedGamepads = new List<Gamepad>();

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

    //InputUser.PerformPairingWithDevice

    void Start()
    {
       
    }
    //打印出所有未分配的控制器
    public void PrintOutUnassignedDevice()
    {
        GetUnassignedDevices();
        // 输出未分配的设备
        Debug.Log($"Unassigned Keyboards: {unassignedKeyboards.Count}");
        Debug.Log($"Unassigned Mouse: {unassignedMouses.Count}");
        Debug.Log($"Unassigned Gamepads: {unassignedGamepads.Count}");
    }

    public void AssignDeviceToPlayer(PlayerInput playerInput)
    {
        GetUnassignedDevices();

        if (unassignedGamepads.Count != 0)
        {
            playerInput.SwitchCurrentControlScheme(unassignedGamepads[0]);
        }
        else if( unassignedKeyboards.Count != 0 && unassignedMouses.Count != 0)
        {
            playerInput.SwitchCurrentControlScheme(unassignedMouses[0], unassignedKeyboards[0]);
        }
    }

    //返回所有未分配的控制器
    private void GetUnassignedDevices()
    {
        unassignedKeyboards.Clear();
        unassignedMouses.Clear();
        unassignedGamepads.Clear();
        // 获取所有已连接的设备
        var allDevices = InputSystem.devices;

        // 遍历所有设备
        foreach (var device in allDevices)
        {
            // 检查设备是否已分配给某个玩家
            bool isAssigned = false;

            foreach (var playerInput in PlayerInput.all)
            {
                if (playerInput.devices.Contains(device))
                {
                    isAssigned = true;
                    break;
                }
            }

            // 如果设备未分配，则添加到列表中
            if (!isAssigned)
            {
                if (device is Keyboard keyboard)
                {
                    unassignedKeyboards.Add(keyboard);
                }
                else if (device is Mouse mouse)
                {
                    unassignedMouses.Add(mouse);
                }
                else if (device is Gamepad gamepad)
                {
                    unassignedGamepads.Add(gamepad);
                }
            }
        }

    }

    void OnEnable()
    {
        //InputUser.onChange += OnInputDeviceChange;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void OnDisable()
    {
       // InputUser.onChange -= OnInputDeviceChange;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {

            case InputDeviceChange.Added:
                Debug.Log(device.name + " is Added");
                if(device is Gamepad)
                {
                    foreach (var player in PlayerInput.all)
                    {
                        if (player.currentControlScheme == "Keyboard&Mouse")
                        {
                            player.SwitchCurrentControlScheme(device);                 
                            Debug.Log(player.gameObject.name + "is Added");
                            break;
                        
                        } 
                    }
                }
                
                break;
            case InputDeviceChange.Removed:
                Debug.Log(device.name + " is Removed");
                foreach (var player in PlayerInput.all)
                { 
                    if (player.devices.Contains(device) && player.gameObject.name.Contains("SecondaryPlayer"))
                    {
                       
                        Destroy(player.gameObject);
                        Camera[] cameras = Camera.allCameras;
                        foreach (Camera camera in cameras)
                        {
                            if (camera.name == "MainPlayerCamera")
                            {
                                Rect rect1 = camera.rect;
                                rect1.x = 0f;
                                camera.rect = rect1;
                            }
                        }
                         break;
                    }
                    if (player.devices.Contains(device) && player.gameObject.name.Contains("MainPlayer"))
                    {
                        AssignDeviceToPlayer(player);
                        break;
                    }
                  
                }
                break;

        }
    }

}
