using UnityEngine;

public class InactiveTurretState : BaseTurretState
{
    public InactiveTurretState(Turret turret) : base(turret) { }

    public override void OnEnter()
    {
        Debug.Log("Turret is inactive");
    }
}
