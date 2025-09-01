using System;
using UnityEngine;

public class NightState : BaseTimeState
{
    public NightState(DayNightCycle dayCycle, SpriteRenderer background, float duration) : base(dayCycle, background, duration) { }

    public override void OnEnter()
    {
        dayCycle.OnEnterNightState();
        elapsed = 0f;
        targetColour = nightColour;
        Debug.Log("Night time");
    }
    public override void Update()
    {
        background.color = Color.Lerp(background.color, targetColour, lerpSpeed * Time.deltaTime);

        elapsed += Time.deltaTime;
        Debug.Log(elapsed);
    }
    public bool IsFinished() => elapsed >= duration;
}
