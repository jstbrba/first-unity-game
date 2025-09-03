using UnityEngine;
using UnityEngine.UI;
public class PowerUI : MonoBehaviour {

    [SerializeField] private Generator gen;
    [SerializeField] private Image powerBar;

    private void OnEnable()
    {
        gen.OnPowerChange += UpdatePowerBar;
    }
    private void OnDisable()
    {
        gen.OnPowerChange -= UpdatePowerBar;
    }
    private void UpdatePowerBar(int current, int max)
    {
        powerBar.fillAmount = (float)current / max;
    }
}
