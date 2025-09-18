using UnityEngine;
using Game;

public class EnemySpawnerInstaller : MonoBehaviour 
{
    private IContext _context;

    private EnemySpawnerModel _model;
    [SerializeField] private EnemySpawnerView _view;
    private EnemySpawnerController _controller;

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
    }
    private void Update()
    {
        _controller.Update(Time.deltaTime);
    }
}