using UnityEngine;

public class DayState : BaseTimeState
{
    public DayState(TimeController controller, float duration) : base(controller, duration) { }

    public override void OnEnter()
    {
        _controller.SetTimeDay();
        _controller.SetCanSleep(false);
        elapsed = 0;
    }
    public override void Update()
    {
        elapsed += Time.deltaTime;
    }
    public bool IsFinished => elapsed > _duration;
}
