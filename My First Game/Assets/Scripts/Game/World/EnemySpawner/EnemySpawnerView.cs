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
    }
    private void UpdateKillCountText()
    {
        _killCountText.text = _model.KillCount.Value + " / " + _model.SpawnLimit.Value;
    }
}