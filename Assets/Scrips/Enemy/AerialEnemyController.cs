using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialEnemyController : MonoBehaviour
{
    public float movementSpeed;
    public float direction;
    public float leftXLimit;
    public float rightXLimit;
    public float scale;
    Vector2 startingPosition;
    Rigidbody2D rigidBody;
    public bool playerSpotted;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfLimitReached();
    }

    void CheckIfLimitReached()
    {
        if (!playerSpotted && transform.position.x < startingPosition.x - leftXLimit || transform.position.x > startingPosition.x + rightXLimit)
        {
            AddOverlap();
            ChangeDirection();
        }

    }

    void  AddOverlap()
    {
        if (transform.position.x < startingPosition.x - leftXLimit)
        {
            float offset = transform.position.x - (startingPosition.x - leftXLimit);
            transform.position = new Vector3(transform.position.x - (-1 - offset), transform.position.y, transform.position.z);
        }
        else
        {
            float offset = transform.position.x - (startingPosition.x + rightXLimit);
            transform.position = new Vector3(transform.position.x - 1 + offset, transform.position.y, transform.position.z);
        }
    }

    public void ChangeDirection()
    {
        rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        direction *= -1;
        transform.localScale = new Vector3(direction * scale, transform.localScale.y, transform.localScale.z);
    }

    void Move()
    {
        rigidBody.AddForce(Vector2.right * direction * movementSpeed);
        rigidBody.velocity *= 0.99f;
    }
}
