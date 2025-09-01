using UnityEngine;
using Game;
public class ShutterSwitch : MonoBehaviour
{
    [SerializeField] private Shutter shutter;
    private Transform player;
    private InputReader inputReader;
    private BoxCollider2D boxCollider;
    private bool inRange;

    // TODO : Use interface like IInteractable cos this class and Stairs look mostly similar
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inputReader = player.GetComponent<InputReader>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (inRange && inputReader.interactPressed) shutter.ToggleSwitch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) inRange = false;
    }
}