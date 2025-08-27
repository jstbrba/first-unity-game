using System.Collections;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI moneyText;

    private void Start()
    {
        UpdateMoney();
    }
    public void UpdateMoney()
    {
        StartCoroutine(UpdateMoneyNextFrame());
    }
    private IEnumerator UpdateMoneyNextFrame()
    {
        yield return null;
        moneyText.text = "$$$ " + GameManager.Instance.Money.ToString();
    }
}
