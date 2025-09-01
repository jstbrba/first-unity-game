using Game;
using System;
using UnityEngine;
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float dayDuration;
    [SerializeField] private float nightDuration;

    private StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine();

        DayState dayState = new DayState(this, background, dayDuration);
        NightState nightState = new NightState(this, background, nightDuration);

        At(dayState, nightState, new FuncPredicate(() => dayState.IsFinished()));
        At(nightState, dayState, new FuncPredicate(()=> nightState.IsFinished()));

        stateMachine.SetState(dayState);
    }
    private void Update()
    {
        stateMachine.Update();
    }

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    public event Action OnDayTime;
    public event Action OnNightTime;
    public void OnEnterDayState() => OnDayTime?.Invoke();
    public void OnEnterNightState() => OnNightTime?.Invoke();
}
