using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public Collider2D frogLOSCollider;
    public List<Collider2D> collisionList;
    public ContactFilter2D contactFilter;
    public bool playerSeen;
    public bool isGrounded;
    public float jumpAngle;
    public float jumpTime;
    Vector2 distanceToPlayer;
    Vector2 playerPosition;
    Vector2 velocity;
    bool jumpMoveMade;
    GroundEnemyController groundController;
    public float jumpHeightOffset;
    public Transform centeredPosition;
    public float groundedRadius;
    public LayerMask platformLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundController = GetComponent<GroundEnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLOSCollision();
        FrogJumpAttackEnabled();
        CheckIfGrounded();

    }
    void CheckLOSCollision()
    {
        Physics2D.GetContacts(frogLOSCollider, contactFilter, collisionList);
        if (collisionList.Count > 0)
        {
            distanceToPlayer = new Vector2();
            playerSeen = true;
            playerPosition = collisionList[0].gameObject.transform.position;
        }
        else
            playerSeen = false;
    }

    void FrogJumpAttackEnabled()
    {
        if (playerSeen && !jumpMoveMade)
        {
            groundController.isAttacking = true;
            jumpMoveMade = true;

            distanceToPlayer.x = playerPosition.x - transform.position.x;
            distanceToPlayer.y = playerPosition.y - transform.position.y;

            velocity.x = distanceToPlayer.x / jumpTime * 2.0f;
            velocity.y = (distanceToPlayer.y + (9.8f * (jumpTime * jumpTime) / 2.0f)) / jumpTime * jumpHeightOffset;

            //rigidbody.velocity = velocity;
            rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        }
    }

    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(centeredPosition.position, groundedRadius, Vector2.down, groundedRadius, platformLayerMask);

        if (!isGrounded && hit)
        {
            jumpMoveMade = false;
            groundController.isAttacking = false;
        }

        isGrounded = (hit) ? true : false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centeredPosition.position, groundedRadius);
    }
}
