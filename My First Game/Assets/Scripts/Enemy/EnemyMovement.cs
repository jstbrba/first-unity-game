using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private enum EnemyBehaviour
    {
        Chasing,
        Retreating
    }

    private float behaviourChangeTimer = 0;

    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform player;
    private float direction;

    private Rigidbody2D body;
    private EnemyBehaviour currentBehaviour = EnemyBehaviour.Chasing;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        behaviourChangeTimer += Time.deltaTime;
        if (behaviourChangeTimer > 2f)  // CHANCE TO CHANGE BEHAVIOUR EVERY 3 SECONDS
            SwitchBehaviour();

        direction = Mathf.Sign(player.position.x - transform.position.x);
        if (currentBehaviour == EnemyBehaviour.Chasing)
            body.linearVelocity = new Vector2(movementSpeed * direction , body.linearVelocity.y);
        else 
            body.linearVelocity = new Vector2(-movementSpeed * direction , body.linearVelocity.y);
    }
    private void SwitchBehaviour()
    {
        if (Random.value < 0.3f){   // SWITCH BEHAVIOUR AT A 30% CHANCE
            if (currentBehaviour == EnemyBehaviour.Chasing)
                currentBehaviour = EnemyBehaviour.Retreating;
            else
                currentBehaviour = EnemyBehaviour.Chasing;
        }
        behaviourChangeTimer = 0;
        Debug.Log("Behaviour: " + currentBehaviour);
    }
}
