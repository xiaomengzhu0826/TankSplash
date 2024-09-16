using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private float _FireCoolDownTime;
    public PlayerMoveState(PlayerStateMachine stateMachine,float fireCoolDownTime) : base(stateMachine)
    {
        _FireCoolDownTime = fireCoolDownTime;
    }

    private float speed = 5f;
    private float turnSpeed = 3f;

    readonly int TANK_MOVE = Animator.StringToHash("Tank_Move");
    readonly int TRACK_MOVE = Animator.StringToHash("Track_Move");
    readonly int TRACK_STOP = Animator.StringToHash("Track_Stop");
    readonly int TANK_IDLE = Animator.StringToHash("Tank_Idle");

    public override void Enter()
    {
        Debug.Log("Player Move State");
        stateMachine.InputReader.FireEvent += OnFire;
    }

    public override void Exit()
    {
        stateMachine.InputReader.FireEvent -= OnFire;
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        Move(fixedDeltaTime);
    }

    public override void Tick(float deltaTime)
    {
        _FireCoolDownTime-=deltaTime;
        Vector3 movement = new Vector3(stateMachine.InputReader.MoveInput.x, 0.0f, stateMachine.InputReader.MoveInput.y);
        if (movement != Vector3.zero)
        {
            stateMachine.ChangeAnimation(TRACK_MOVE, 1);
            stateMachine.ChangeAnimation(TANK_MOVE, 0,0.2f);
        }
        else
        {
            stateMachine.ChangeAnimation(TRACK_STOP, 1);
            stateMachine.ChangeAnimation(TANK_IDLE, 0, 0.2f);
        }
    }

    private void Move(float fixedDeltaTime)
    {
        Vector3 movement = new Vector3(stateMachine.InputReader.MoveInput.x, 0.0f, stateMachine.InputReader.MoveInput.y);

        if (movement != Vector3.zero)
        {

            Quaternion targetRotation = Quaternion.LookRotation(movement);

            // 平滑旋转到目标方向
            stateMachine.Rigidbody.MoveRotation(Quaternion.Lerp(stateMachine.Rigidbody.rotation, targetRotation, turnSpeed * fixedDeltaTime));

            // 移动玩家
            stateMachine.Rigidbody.MovePosition(stateMachine.Transform.position + fixedDeltaTime * speed * movement);
        }
        else
        {

        }
    }

    private void OnFire()
    {
        if (_FireCoolDownTime < 0)
        {
            stateMachine.SwitchState(new PlayerChargingState(stateMachine));
        }
            
    }
}
