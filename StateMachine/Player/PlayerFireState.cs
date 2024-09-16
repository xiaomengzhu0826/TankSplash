using UnityEngine;

public class PlayerFireState : PlayerBaseState
{
    readonly int TANK_FIRE = Animator.StringToHash("Tank_Fire");

    private float _LaunchForce;
    private float _MinLaunchForce;

    private float _StartTime=0.3f;
    public PlayerFireState(PlayerStateMachine stateMachine,float launchForce,float minLaunchForce) : base(stateMachine)
    {
        _LaunchForce = launchForce;
        _MinLaunchForce = minLaunchForce;
    }

    public override void Enter()
    {
        Debug.Log("Player Fire State");
        stateMachine.ChangeAnimation(TANK_FIRE, 0, 0.2f);
    }

    private void Fire()
    {
        Rigidbody shellInstance = stateMachine.ShellInstance();
        shellInstance.linearVelocity = _LaunchForce * stateMachine.FireTransform.forward;
        stateMachine.ShootingAudio.clip = stateMachine.FireClip;
        stateMachine.ShootingAudio.Play();
        _LaunchForce = _MinLaunchForce;
    }
    public override void Exit()
    {
        
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        
    }

    public override void Tick(float deltaTime)
    {

        //AnimatorStateInfo stateInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0); // 0 是默认的层级索引
        //float normalizedTime = stateInfo.normalizedTime;
        //float animationLength = stateInfo.length;
        //float actualTime = normalizedTime * animationLength;
        _StartTime -= deltaTime;
        if (_StartTime<0)
        {
            Fire();
            stateMachine.SwitchState(new PlayerMoveState(stateMachine, 3f));
        }

    }
}
