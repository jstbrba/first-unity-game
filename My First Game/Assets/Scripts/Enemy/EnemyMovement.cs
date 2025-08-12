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
    private Animator anim;
    private Vector3 originalScale;
    private EnemyBehaviour currentBehaviour = EnemyBehaviour.Chasing;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
    }
    private void Update()
    {
        // FLIP ENEMY
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        else
            transform.localScale = originalScale;


        behaviourChangeTimer += Time.deltaTime;
        if (behaviourChangeTimer > 2f)  // CHANCE TO CHANGE BEHAVIOUR EVERY 3 SECONDS
            SwitchBehaviour();

        direction = Mathf.Sign(player.position.x - transform.position.x);

        if (currentBehaviour == EnemyBehaviour.Chasing)
        {
            body.linearVelocity = new Vector2(movementSpeed * direction, body.linearVelocity.y);
            anim.SetFloat("moveDir", 1f);
            anim.SetBool("walking", true);
        }
        else
        {
            body.linearVelocity = new Vector2(-movementSpeed * direction, body.linearVelocity.y);
            anim.SetFloat("moveDir", -1f);
            anim.SetBool("walking", true);
        }
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
        //Debug.Log("Behaviour: " + currentBehaviour);
    }
}
