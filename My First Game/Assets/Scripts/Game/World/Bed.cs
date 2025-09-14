using Game;
using System;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private InputReader _inputReader;
    private bool _playerInRange;

    public event Action onSleep;
    private void Start()
    {
        _inputReader = _player.GetComponent<InputReader>();
    }

    private void Update()
    {
        if (_playerInRange && _inputReader.interactPressed)
        {
            onSleep?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _playerInRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _playerInRange = false;
    }
}
