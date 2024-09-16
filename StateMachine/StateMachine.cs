using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        currentState?.FixedTick(Time.fixedDeltaTime);
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public State GetCurrentState()
    {
        return currentState;
    }
}
