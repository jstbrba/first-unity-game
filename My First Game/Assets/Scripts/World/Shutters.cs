using UnityEngine;

public class Shutters : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float shutterSpeed;
    private Vector3 originalPosition;
    [SerializeField] private BoxCollider2D boxCollider;

    private bool isOn = true;

    private void Start()
    {
        originalPosition = transform.position;
    }
    private void Update()
    {
        // TODO : Add switch for player to interact with to open/close doors
        // TODO : Connect the shutters to a generator
        if (Input.GetMouseButtonDown(0)) isOn = !isOn;

        if (isOn && !isClosed())
        {
            transform.Translate(0f, -shutterSpeed * Time.deltaTime, 0f);
        }
        else if (!isOn && transform.position.y <= originalPosition.y)
        {
            transform.Translate(0f, shutterSpeed * Time.deltaTime, 0f);
        }
        Debug.Log("Is CLOSED: " + isClosed() + " Is ON: " + isOn);
    }

    private bool isClosed()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(0f, boxCollider.bounds.min.y, 0f), Vector3.down, 0.01f, groundLayer);
        return hit.collider != null;
    }
}
