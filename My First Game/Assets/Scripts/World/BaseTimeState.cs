using Game;
using UnityEngine;

public abstract class BaseTimeState : IState
{
    protected static readonly Color dayColour = new Color(127f / 255f, 196f / 255f, 245f / 255f);
    protected static readonly Color nightColour = new Color(23f / 255f, 36f / 255f, 66f / 255f);
    protected Color targetColour;

    protected float lerpSpeed = 1f;

    protected DayNightCycle dayCycle;
    protected SpriteRenderer background;
    protected float duration;
    protected float elapsed;
    protected BaseTimeState(DayNightCycle dayCycle, SpriteRenderer background, float duration) {
        this.dayCycle = dayCycle;
        this.background = background;
        this.duration = duration;
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
