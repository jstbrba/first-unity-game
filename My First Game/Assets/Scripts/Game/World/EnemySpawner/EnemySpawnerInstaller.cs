using UnityEngine;
using Game;

public class EnemySpawnerInstaller : MonoBehaviour 
{
    private IContext _context;

    private EnemySpawnerModel _model;
    [SerializeField] private EnemySpawnerView _view;
    private EnemySpawnerController _controller;

    private EnemySpawner _spawner;
    private void Start()
    {
        _context = new EnemySpawnerContext();

        _model = new EnemySpawnerModel();
        _model.Initialise(_context);
        _context.ModelLocator.Register(_model);

        _view.Initialise(_context);
        _context.ViewLocator.Register(_view);

        _controller = new EnemySpawnerController(_model, _view);
        _controller.Initialise(_context);

        _spawner = GetComponent<EnemySpawner>();
        _spawner.Initialise(_context);
    }
}