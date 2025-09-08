using UnityEngine;

public class Shutter : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float shutterSpeed;
    [SerializeField] private Generator gen;
    private Vector3 originalPosition;
    [SerializeField] private BoxCollider2D boxCollider;

    private Health health;

    private bool isOn = true;
    private bool isGenOn = true;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    private void Start()
    {
        originalPosition = transform.position;
    }
    private void OnEnable()
    {
        gen.OnPowerDown += Shutter_OnPowerDown;
    }
    private void OnDisable()
    {
        gen.OnPowerDown -= Shutter_OnPowerDown;
    }
    private void Update()
    {
        if (isOn && !isClosed() && isGenOn)
        {
            transform.Translate(0f, -shutterSpeed * Time.deltaTime, 0f);
        }
        else if ((!isOn || !isGenOn) && transform.position.y <= originalPosition.y)
        {
            transform.Translate(0f, shutterSpeed * Time.deltaTime, 0f);
        }
    }

    private bool isClosed()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(0f, boxCollider.bounds.min.y, 0f), Vector3.down, 0.01f, groundLayer);
        return hit.collider != null;
    }
    public void ToggleSwitch()
    {
        isOn = !isOn;
    }
    private void Shutter_OnPowerDown() => isGenOn = false;
    public void HandleDeath()
    {
        gameObject.SetActive(false);
    }
}
