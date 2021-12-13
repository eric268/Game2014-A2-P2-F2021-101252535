//-------------------------WaspController.cs---------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 10 2021----------------------------------
//  The file containts the script used for controlling the wasp enemies seek attack
//  Revision History : 1.4 Wasp now moves via add force 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wasp controller class
public class WaspController : MonoBehaviour
{
    public AerialEnemyController aerialController;
    public Vector2 startingPosition;
    public Vector2 playerPosition;
    public float maxChaseRange;
    public ContactFilter2D contactFilter;
    public Collider2D losCollider;
    public List<Collider2D> collisionList;
    public float test;
    public bool movingToStartPos;
    Rigidbody2D rigidbody;
    Animator animator;
    public float waspChaseSpeed = 75.0f;

    // Start is called before the first frame update
    void Start()
    {
        aerialController = GetComponent<AerialEnemyController>();
        startingPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    //Moves the wasp
    void Move()
    {
        if (CheckIfPlayerSpotted() && !movingToStartPos)
        {
            if (FarFromStartingPos())
            {
                StopChasing();
            }
            else
            {
                ChasePlayer();
            }
        }
        else if (aerialController.playerSpotted)
        {
            StopChasing();
        }
    }
    //Checks if the wasp line of sight has collided with player
    bool CheckIfPlayerSpotted()
    {
        Physics2D.GetContacts(losCollider,contactFilter, collisionList);
        if (collisionList.Count > 0)
        {
            playerPosition = collisionList[0].gameObject.transform.position;
            aerialController.playerSpotted = true;
            return true;
        }
        return false;
    }
    //Moves the wasp towards the player position
    void ChasePlayer()
    {
        animator.SetBool("isAttacking", true);
        Vector2 direction = (playerPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        rigidbody.AddForce(direction * waspChaseSpeed);
        rigidbody.velocity *= 0.9f;
    }
    //Checks if the wasp has moved too far away from its patrol area
    bool FarFromStartingPos()
    {
        float distance = Vector2.Distance(transform.position, startingPosition);
        if ( distance >= maxChaseRange)
            return true;
        else
             return false;
    }
    //Makes wasp move towards patrol area
    void MoveToStartingPosition()
    {
        Vector2 direction = (startingPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        rigidbody.AddForce(direction * 25.0f);
        rigidbody.velocity *= 0.9f;
        movingToStartPos = true;
        CheckIfFacingCorrectDirection(direction.x);
    }
    //Makes wasp change direction if not facing direction it is moving
    void CheckIfFacingCorrectDirection(float xDir)
    {
        if (xDir < 0 && aerialController.direction > 0 || xDir > 0 && aerialController.direction < 0)
         aerialController.ChangeDirection();

    }
    //Checks to see if the wasp has returned to its patrol area
    void CheckAtStartingPositon()
    {
        float distance = Vector2.Distance(transform.position, startingPosition);
        if (movingToStartPos && distance <= 5.0f)
        {
            rigidbody.velocity = Vector2.zero;
            aerialController.playerSpotted = false;
            movingToStartPos = false;
        }
    }

    void StopChasing()
    {
        animator.SetBool("isAttacking", false);
        MoveToStartingPosition();
        CheckAtStartingPositon();
    }
}
