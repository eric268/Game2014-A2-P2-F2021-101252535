//-------------------------FrogController.cs---------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 11 2021----------------------------------
//  The file containts the script used for controlling the frog enemies jump attack
//  Revision History : 1.2 Added check to ensure player hasn't been spotted.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Frog controller class
public class FrogController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;
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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLOSCollision();
        FrogJumpAttackEnabled();
        CheckIfGrounded();

    }
    //Checks if the frog sees the enemy
    void CheckLOSCollision()
    {
        Physics2D.GetContacts(frogLOSCollider, contactFilter, collisionList);
        //Since using a player contractFilter and only one enemy exists if this is not 0 player is seen
        if (collisionList.Count > 0)
        {
            distanceToPlayer = new Vector2();
            playerSeen = true;
            playerPosition = collisionList[0].gameObject.transform.position;
        }
        else
            playerSeen = false;
    }
    //Does the projectile motion calculations and sets velocity to that 
    void FrogJumpAttackEnabled()
    {
        if (playerSeen && !jumpMoveMade)
        {
            animator.Play("Jump");

            groundController.isAttacking = true;
            jumpMoveMade = true;

            distanceToPlayer.x = playerPosition.x - transform.position.x;
            distanceToPlayer.y = playerPosition.y - transform.position.y;

            velocity.x = distanceToPlayer.x / jumpTime * 2.0f;
            //Jump height offset is so the frog doesnt jump directly at the player making it a bit more unpredictable/harder
            velocity.y = (distanceToPlayer.y + (9.8f * (jumpTime * jumpTime) / 2.0f)) / jumpTime * jumpHeightOffset;

            //rigidbody.velocity = velocity;
            rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
    //Checks if the frog is on the ground
    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(centeredPosition.position, groundedRadius, Vector2.down, groundedRadius, platformLayerMask);

        if (!isGrounded && hit)
        {
            animator.Play("Run");
            jumpMoveMade = false;
            groundController.isAttacking = false;
        }

        isGrounded = (hit) ? true : false;
    }
    // Debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centeredPosition.position, groundedRadius);
    }
}
