using Game;
using UnityEngine;

public class TrapPlacer : MonoBehaviour
{
    private InputReader inputReader;

    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private int maxTurrets = 3;
    private int turretCount = 0;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
    }
    private void Update()
    {
        if (inputReader.placePressed) Place();
    }
    public void Place()
    {
        if (turretCount == maxTurrets) return;

        GameObject turret = GameObject.Instantiate(turretPrefab);
        turret.transform.position = transform.position;
        int direction = (int)Mathf.Sign(transform.localScale.x);
        turret.transform.localScale = new Vector3(turret.transform.localScale.x * direction, turret.transform.localScale.y, turret.transform.localScale.z);
        turretCount++;
    }
}
