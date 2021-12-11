using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : MonoBehaviour
{
    public Transform lookAheadPoint;
    public Transform lookForGround;
    Rigidbody2D rigidBody;
    Animator animator;
    public bool isGroundForward;
    public float movementForce;
    public float currentDirection;
    public LayerMask platformMask;
    public LayerMask enemyLayerMask;
    public float scale;
    public bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfHittingGound();
        CheckIfHittingObject();
    }

    void CheckIfHittingGound()
    {
        var hit = Physics2D.Linecast(transform.position, lookForGround.position, platformMask);
        
        isGroundForward = (hit) ? true : false;

    }

    void CheckIfHittingObject()
    {
        RaycastHit2D[] hitArray = Physics2D.LinecastAll(transform.position, lookForGround.position, enemyLayerMask);
        foreach(RaycastHit2D hit in hitArray)
        {
            if (hit.collider.gameObject != this.gameObject || hit.collider.CompareTag("Boundary"))
                ChangeDirection();
        }
    }

    void Move()
    {
        if (!isAttacking)
        {
            if (isGroundForward)
            {
                if (gameObject.CompareTag("Test"))
                {
                    Debug.Log("Velocity: " + rigidBody.velocity);
                }

                Vector2 force = Vector2.right * movementForce * transform.localScale.x/scale;
                rigidBody.AddForce(force);
                if (rigidBody.velocity == Vector2.zero)
                    rigidBody.velocity = Vector2.right * movementForce * transform.localScale.x / scale;
                rigidBody.velocity *= 0.99f;
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    void ChangeDirection()
    {
        rigidBody.velocity = new Vector2(0.0f, rigidBody.velocity.y);
        currentDirection *= -1;
        transform.localScale = new Vector3(currentDirection * scale, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, lookAheadPoint.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, lookForGround.position);
    }

}
