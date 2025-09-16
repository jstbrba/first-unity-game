using TMPro;
using UnityEngine;
public class EnemySpawnerView : MonoBehaviour, IView
{
    private IContext _context;
    private EnemySpawnerModel _model;

    [SerializeField] private TextMeshProUGUI _killCountText;
    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<EnemySpawnerModel>();

        UpdateKillCountText();

        _model.KillCount.onChanged += UpdateKillCountText;
        _model.SpawnLimit.onChanged += UpdateKillCountText;
    }
    private void UpdateKillCountText()
    {
        _killCountText.text = "Enemies: " +  _model.KillCount.Value + " / " + _model.SpawnLimit.Value;
    }
}
