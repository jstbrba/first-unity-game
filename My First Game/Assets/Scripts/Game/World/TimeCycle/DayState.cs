using UnityEngine;

public class DayState : BaseTimeState
{
    public DayState(DayNightCycle dayCycle, SpriteRenderer background, float duration) : base(dayCycle, background, duration) { }

    public override void OnEnter()
    {
        dayCycle.OnEnterDayState();
        elapsed = 0f;
        targetColour = dayColour;
    }
    public override void Update()
    {
        background.color = Color.Lerp(background.color, targetColour, lerpSpeed * Time.deltaTime);

        elapsed += Time.deltaTime;
    }
    public bool IsFinished() => elapsed >= duration;
}
