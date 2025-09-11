using UnityEngine;
using Utilities;
[CreateAssetMenu (fileName = "PlayerStats", menuName = "Models/Player/PlayerStats")]
public class PlayerStatsModel : BaseModel
{
    [SerializeField] private float _baseSpeed;
    [SerializeField] private int _baseAttack;
    [SerializeField] private float _baseJumpPower;

    public Observable<float> Speed { get { return _speed; } }
    public Observable<int> Attack {  get { return _attack; } }
    public Observable<float> JumpPower { get { return _jumpPower; } }

    private Observable<float> _speed =new Observable<float>();
    private Observable<int> _attack =new Observable<int>();
    private Observable<float> _jumpPower = new Observable<float>();
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _speed.Value = _baseSpeed;
        _attack.Value = _baseAttack;
        _jumpPower.Value = _baseJumpPower;
    }
}
