using Game;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private bool inRange;

    private Transform Player;
    private InputReader input;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        input = Player.GetComponent<InputReader>();
    }

    private void Update()
    {
        if (inRange && input.interactPressed) Player.transform.position = destination.position;
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
