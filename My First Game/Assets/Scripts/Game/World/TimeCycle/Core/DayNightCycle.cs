using Game;
using System;
using UnityEngine;
public class DayNightCycle : MonoBehaviour
{
    private IContext _context;

    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float dayDuration;
    [SerializeField] private float nightDuration;
    [SerializeField] private Bed _bed;
    [SerializeField] private EnemySpawner _enemySpawner;

    private StateMachine stateMachine;
    private DayState dayState;
    private NightState nightState;

    private void Awake()
    {
        stateMachine = new StateMachine();

        dayState = new DayState(this, background, dayDuration);
        nightState = new NightState(this, background, nightDuration, _enemySpawner);

        At(dayState, nightState, new FuncPredicate(() => dayState.IsFinished()));
        // At(nightState, dayState, new FuncPredicate(()=> nightState.IsFinished()));

        stateMachine.SetState(dayState);
    }
    public void Initialise(IContext context)
    {
        _context = context;
    }

    private void OnEnable()
    {
        _bed.onSleep += Bed_OnSleep;
    }
    private void OnDisable()
    {
        _bed.onSleep -= Bed_OnSleep;
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

    public void Bed_OnSleep()
    {
        if (stateMachine.GetCurrentState() == nightState && nightState.IsFinished())
            stateMachine.SetState(dayState);
        else
            Debug.Log("Night not finished yet or current state is " + stateMachine.GetCurrentState().ToString());
    }
}
