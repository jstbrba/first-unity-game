using UnityEngine;
public class ActiveTurretState : BaseTurretState
{
    private float fireTimer = 0;
    public ActiveTurretState(Turret turret) : base(turret) { }

    public override void OnEnter()
    {
        Debug.Log("Turret is active");
    }
    public override void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer > turret.FireRate)
        {
            turret.Fire();
            fireTimer = 0;
        }
    }
}
