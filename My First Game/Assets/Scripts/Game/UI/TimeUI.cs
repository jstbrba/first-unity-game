using TMPro;
using UnityEngine;
public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private DayNightCycle dayNightCycle;
    private void OnEnable()
    {
        dayNightCycle.OnDayTime += SetToDay;
        dayNightCycle.OnNightTime += SetToNight;
    }
    private void OnDisable()
    {
        dayNightCycle.OnDayTime -= SetToDay;
        dayNightCycle.OnNightTime -= SetToNight;
    }

    private void SetToDay() => timeText.SetText("Daytime");
    private void SetToNight() => timeText.SetText("Nighttime");
}
