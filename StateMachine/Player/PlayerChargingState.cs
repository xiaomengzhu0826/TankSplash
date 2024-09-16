using UnityEngine;

public class PlayerChargingState : PlayerBaseState
{
    private float minLaunchForce = 8f;
    private float maxLaunchForce = 16f;
    private float maxChargeTime = 0.75f;

    private float currentLaunchForce;
    private float chargeSpeed;
    private float _StartTime=0;
    private float pressDuration;

    readonly int TANK_CHARGING = Animator.StringToHash("Tank_Charging");
    readonly int TRACK_STOP = Animator.StringToHash("Track_Stop");
    public PlayerChargingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("Player Charging State");
        stateMachine.AimSlider.gameObject.SetActive(true);
        stateMachine.InputReader.FireCancelEvent += OnFireReleased;
        stateMachine.ChangeAnimation(TANK_CHARGING, 0, 0.2f);
        stateMachine.ChangeAnimation(TRACK_STOP, 1);
        //pressStartTime = Time.time;
        currentLaunchForce = minLaunchForce;
        stateMachine.AimSlider.value = minLaunchForce;
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
        stateMachine.ShootingAudio.clip = stateMachine.ChargingClip;
        stateMachine.ShootingAudio.Play();
    }


    public override void Exit()
    {
        stateMachine.InputReader.FireCancelEvent -= OnFireReleased;
    }

    public override void FixedTick(float fixedDeltaTime)
    {

    }

    public override void Tick(float deltaTime)
    {
        //pressDuration = Time.time - pressStartTime;
        _StartTime += Time.deltaTime;
        currentLaunchForce += chargeSpeed * Time.deltaTime;
        stateMachine.AimSlider.value = currentLaunchForce;
        if (_StartTime >= maxChargeTime)
        {             
            stateMachine.AimSlider.gameObject.SetActive(false);      
            stateMachine.SwitchState(new PlayerFireState(stateMachine,currentLaunchForce,minLaunchForce));            
        }

    }

    private void OnFireReleased()
    {
        stateMachine.AimSlider.gameObject.SetActive(false);
        stateMachine.SwitchState(new PlayerFireState(stateMachine, currentLaunchForce, minLaunchForce));
        //pressDuration = Time.time - pressStartTime; // 计算按钮按下的持续时间

    }
}
