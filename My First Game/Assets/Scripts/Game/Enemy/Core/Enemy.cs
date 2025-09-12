using UnityEngine;
namespace Game
{
    public class Enemy : Flyweight
    {
        private IContext _context;
        private EnemyStatsModel _statsModel;
        private HealthModel _healthModel;
        [HideInInspector] new EnemySettings settings => (EnemySettings)base.settings;
        private float _movementSpeed;
        private int _moneyOnDeath;

        private Rigidbody2D _body;
        private Animator _anim;
        private PlayerDetector _playerDetector;
        private TargetDetector _targetDetector;
        private Vector3 _originalScale;

        private StateMachine _stateMachine;
        private EnemyIdleState _idleState;

        private float _lowHealthThreshold = 2;
        private bool _isLowHealth = false;

        private EnemyAttack _enemyAttack;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _enemyAttack = GetComponent<EnemyAttack>();
            _playerDetector = GetComponent<PlayerDetector>();
            _targetDetector = GetComponent<TargetDetector>();
            _originalScale = transform.localScale;

            ConfigureStateMachine();
        }
        public void Initialise(IContext context)
        {
            _context = context;

            _healthModel = _context.ModelLocator.Get<HealthModel>();
            _healthModel.CurrentHealth.onValueChanged += HealthCheck;
            _context.CommandBus.AddListener<DeathCommand>(HandleDeath);

            _statsModel = _context.ModelLocator.Get<EnemyStatsModel>();
            _movementSpeed = _statsModel.Speed.Value;
            _moneyOnDeath = _statsModel.MoneyOnDeath.Value;

            _statsModel.Speed.onValueChanged += Model_Speed_OnValueChanged;
            _statsModel.MoneyOnDeath.onValueChanged += Model_MoneyOnDeath_OnValueChanged;
        }
        private void OnEnable()
        {
            _stateMachine.SetState(_idleState);

            _context?.CommandBus.Dispatch(new RespawnCommand());
        }
        private void OnDisable()
        {
            _context?.CommandBus.RemoveListener<DeathCommand>(HandleDeath);       
        }
        private void Update()
        {
            _stateMachine.Update();
        }
        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
        public void Chase() => _body.linearVelocity = new Vector2(_movementSpeed * _playerDetector.Direction(), _body.linearVelocity.y);
        public void Retreat() => _body.linearVelocity = new Vector2(-_movementSpeed * _playerDetector.Direction(), _body.linearVelocity.y);
        public void FacePlayer()
        {
            if (_playerDetector.PlayerActive && _playerDetector.Player.position.x < transform.position.x)
                transform.localScale = new Vector3(-_originalScale.x, _originalScale.y, _originalScale.z);
            else
                transform.localScale = _originalScale;
        }
        private void ConfigureStateMachine()
        {
            _stateMachine = new StateMachine();

            _idleState = new EnemyIdleState(this, _anim);
            var chaseState = new EnemyChaseState(this, _anim);
            var retreatState = new EnemyRetreatState(this, _anim);
            var attackState = new EnemyAttackState(this, _anim, _enemyAttack);

            At(_idleState, chaseState, new FuncPredicate(() => _playerDetector.InRange() && !_playerDetector.InAttackRange() && !_targetDetector.TargetInRange()));

            At(chaseState, _idleState, new FuncPredicate(() => !_playerDetector.InRange() || _playerDetector.InAttackRange()));

            At(attackState, _idleState, new FuncPredicate(() => attackState.IsAttackFinished));

            At(retreatState, _idleState, new FuncPredicate(() => _isLowHealth && _playerDetector.SafeRange() || !_isLowHealth && _playerDetector.InAttackRange()));

            Any(retreatState, new FuncPredicate(() => (_isLowHealth && !_playerDetector.SafeRange() || _playerDetector.CloseRange()) && _enemyAttack.IsRunning));
            Any(attackState, new FuncPredicate(() => (_playerDetector.CanAttack() || _targetDetector.TargetInRange()) && !_enemyAttack.IsRunning));


            // stateMachine.SetState(idleState);
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
        public void HandleDeath(DeathCommand command)
        {
            settings.moneyChannel.Invoke(settings.moneyOnDeath);
            FlyweightFactory.ReturnToPool(this);
        }
        public void HealthCheck(int previous, int current)
        {
            _isLowHealth = current < _lowHealthThreshold ? true : false;
        }
        public void Model_Speed_OnValueChanged(float previous, float current) => _movementSpeed = current;
        public void Model_MoneyOnDeath_OnValueChanged(int previous, int current) => _moneyOnDeath = current;
    }
}
