using Game;
using System;
using UnityEngine;

public class NightState : BaseTimeState
{
    // private EnemySpawner _enemySpawner;
    public NightState(TimeController controller, float duration) : base(controller, duration) { }

    public override void OnEnter()
    {
        _controller.SetTimeNight();
        // _enemySpawner.DespawnEnemiesOutOfView();
        elapsed = 0;
    }
    public override void Update()
    {
        elapsed += Time.deltaTime;
        if (IsFinished) _controller.SetCanSleep(true);
    }
    public bool IsFinished => elapsed > _duration;
}
