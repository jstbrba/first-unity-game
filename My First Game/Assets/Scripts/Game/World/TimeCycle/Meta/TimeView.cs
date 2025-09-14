using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _dayNightText;
    [SerializeField] private TextMeshProUGUI _dayCounterText;

    private IContext _context;
    public void Initialise(IContext context)
    {
        _context = context;


    }
}
