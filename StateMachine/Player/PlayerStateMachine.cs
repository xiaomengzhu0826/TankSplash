using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }

    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

    [field: SerializeField] public Transform Transform { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public Rigidbody ShellRigidbody { get; private set; }

    [field: SerializeField] public Transform FireTransform { get; private set; }

    [field: SerializeField] public Slider AimSlider { get; private set; }

    [field: SerializeField] public AudioSource ShootingAudio { get; private set; }

    [field: SerializeField] public AudioClip ChargingClip { get; private set; }

    [field: SerializeField] public AudioClip FireClip { get; private set; }

    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

    [field: SerializeField] public Health TankHealth { get; private set; }

    //[field: SerializeField] public Canvas Canvas { get; private set; }

    //[field: SerializeField] public int PlayerIndex { get; private set; }

    private int currentLayer0Animation ;

    private int currentLayer1Animation ;

    private void Start()
    {
        SwitchState(new PlayerMoveState(this,0));
    }
    private void OnEnable()
    {
        TankHealth.OnDie += HandleDie;
    }
    private void OnDisable()
    {
        TankHealth.OnDie -= HandleDie;
    }

    private void HandleDie()
    {
       //SwitchState(new PlayerDeathState(this, PlayerIndex));
    }

    public Rigidbody ShellInstance()
    {
        Rigidbody shellInstance = Instantiate(ShellRigidbody, FireTransform.position, FireTransform.rotation) as Rigidbody;

        return shellInstance;
    }
    //public void OnStart()
    //{
    //    MenuManager.instance.Canvas.gameObject.SetActive(true);
    //    PlayerInput.SwitchCurrentActionMap("UI");
    //    PlayerInput playerInput2 = GameObject.FindWithTag("Player2").GetComponent<PlayerInput>();
    //    playerInput2.SwitchCurrentActionMap("UI");

    //}
    //public void OnResume()
    //{
    //    MenuManager.instance.Canvas.gameObject.SetActive(false);
    //    PlayerInput.SwitchCurrentActionMap("Player");
    //    PlayerInput playerInput2 = GameObject.FindWithTag("Player2").GetComponent<PlayerInput>();
    //    playerInput2.SwitchCurrentActionMap("Player");
    //}

    public void ChangeAnimation(int animationHash, int layerIndex, float crossfade)
    {
        if (layerIndex == 0)
        {
            if (currentLayer0Animation != animationHash)
            {
                currentLayer0Animation = animationHash;
                Animator.CrossFade(animationHash, crossfade, layerIndex);
            }
        }
        if (layerIndex == 1)
        {
            if (currentLayer1Animation != animationHash)
            {
                currentLayer1Animation = animationHash;
                Animator.CrossFade(animationHash, crossfade, layerIndex);
            }
        }



    }

    public void ChangeAnimation(int animationHash, int layerIndex)
    {
        if (layerIndex == 0)
        {
            if (currentLayer0Animation != animationHash)
            {
                currentLayer0Animation = animationHash;
                Animator.Play(animationHash, layerIndex);
            }
        }
        if (layerIndex == 1)
        {
            if (currentLayer1Animation != animationHash)
            {
                currentLayer1Animation = animationHash;
                Animator.Play(animationHash, layerIndex);
            }
        }
    }

}
