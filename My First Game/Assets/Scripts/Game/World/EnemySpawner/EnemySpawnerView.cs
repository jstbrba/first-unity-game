using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class EnemySpawnerView : MonoBehaviour, IView
{
    private IContext _context;
    private EnemySpawnerModel _model;

    [SerializeField] private TextMeshProUGUI _killCountText;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<EnemySpawnerModel>();

        UpdateKillCountText();

        _model.SpawnCount.onValueChanged += Model_SpawnCount_OnValueChanged;
        _model.KillCount.onChanged += UpdateKillCountText;
        _model.SpawnLimit.onChanged += UpdateKillCountText;
    }
    private void Model_SpawnCount_OnValueChanged(int previous, int current)
    {
        int additionalCount = current - previous;
        if (additionalCount < 0) return;
        for (int i = 0; i < additionalCount; i++)
        {
            SpawnEnemy();
            Debug.Log("View spawned " + additionalCount + " more");
        }
    }
    private void UpdateKillCountText()
    {
        _killCountText.text = "Enemies: " +  _model.KillCount.Value + " / " + _model.SpawnLimit.Value;
    }
    private void SpawnEnemy()
    {
        var enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
    }
}
