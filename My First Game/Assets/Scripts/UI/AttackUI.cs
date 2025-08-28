using UnityEngine;
using UnityEngine.UI;
public class AttackUI : MonoBehaviour {
    [SerializeField] private IntEventChannel attackChannel;
    [SerializeField] private Button[] attackButtons;

    private void Awake() {
        for (int i = 0; i < attackButtons.Length; i++) {
            int index = i;
            attackButtons[i].onClick.AddListener(() => attackChannel.Invoke(index));
        }
    }
}
