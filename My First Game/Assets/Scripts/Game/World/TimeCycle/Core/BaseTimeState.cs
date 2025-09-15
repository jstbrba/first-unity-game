using Game;
using UnityEngine;

public abstract class BaseTimeState : IState
{
    protected TimeController _controller;
    protected float _duration;

    protected float elapsed;
    protected BaseTimeState(TimeController controller, float duration)
    {
        _controller = controller;
        _duration = duration;
    }
    public virtual void FixedUpdate()
    {
        // noop
    }

    public virtual void OnEnter()
    {
        // noop
    }

    public virtual void OnExit()
    {
        // noop
    }

    public virtual void Update()
    {
        // noop
    }
}
