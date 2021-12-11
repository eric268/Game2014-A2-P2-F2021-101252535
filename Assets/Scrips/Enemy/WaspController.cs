using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        aerialController = GetComponent<AerialEnemyController>();
        startingPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(Time.deltaTime);
    }

    void Move(float deltaTime)
    {
        if (CheckIfPlayerSpotted() && !movingToStartPos)
        {
            if (FarFromStartingPos())
            {
                MoveToStartingPosition();
                CheckAtStartingPositon();

            }
            else
                ChasePlayer();
        }
        else if (aerialController.playerSpotted)
        {
            MoveToStartingPosition();
            CheckAtStartingPositon();
        }
    }

    bool CheckIfPlayerSpotted()
    {
        Physics2D.GetContacts(losCollider, collisionList);
        if (collisionList.Count > 0 && collisionList[0].CompareTag("Player"))
        {
            playerPosition = collisionList[0].gameObject.transform.position;
            aerialController.playerSpotted = true;
            return true;
        }
        return false;
    }

    void ChasePlayer()
    {
        Vector2 direction = (playerPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        rigidbody.AddForce(direction * 50.0f);

        rigidbody.velocity *= 0.9f;

        //transform.position = Vector2.Lerp(transform.position, playerPosition, test);
    }

    bool FarFromStartingPos()
    {
        float distance = Vector2.Distance(transform.position, startingPosition);
        if ( distance >= maxChaseRange)
            return true;
        else
             return false;
    }

    void MoveToStartingPosition()
    {
        Vector2 direction = (startingPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        rigidbody.AddForce(direction * 25.0f);
        rigidbody.velocity *= 0.9f;
        movingToStartPos = true;
        CheckIfFacingCorrectDirection(direction.x);
    }

    void CheckIfFacingCorrectDirection(float xDir)
    {
        if (xDir < 0 && aerialController.direction > 0 || xDir > 0 && aerialController.direction < 0)
         aerialController.ChangeDirection();

    }

    void CheckAtStartingPositon()
    {
        float distance = Vector2.Distance(transform.position, startingPosition);
        Debug.Log(distance);
        if (movingToStartPos && distance <= 5.0f)
        {
            rigidbody.velocity = Vector2.zero;
            aerialController.playerSpotted = false;
            movingToStartPos = false;
        }
    }
}
