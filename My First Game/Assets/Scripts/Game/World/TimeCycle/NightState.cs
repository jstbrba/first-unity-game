using Game;
using System;
using UnityEngine;

public class NightState : BaseTimeState
{
    private EnemySpawner _enemySpawner;
    public NightState(DayNightCycle dayCycle, SpriteRenderer background, float duration, EnemySpawner enemySpawner) : base(dayCycle, background, duration) 
    {
        _enemySpawner = enemySpawner;
    }

    public override void OnEnter()
    {
        dayCycle.OnEnterNightState();
        elapsed = 0f;
        targetColour = nightColour;

        _enemySpawner.DespawnEnemiesOutOfView();
    }
    public override void Update()
    {
        background.color = Color.Lerp(background.color, targetColour, lerpSpeed * Time.deltaTime);

        elapsed += Time.deltaTime;
    }
    public bool IsFinished() => elapsed >= duration;
}
