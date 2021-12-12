using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float currentDistance;
    public float maxDistance;
    public float direction;
    public bool isActive;
    public Transform platformTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (isActive)
        {
            transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime * direction), transform.position.y);
            currentDistance += (speed * Time.deltaTime);
            if (currentDistance >= maxDistance)
            {
                currentDistance = 0;
                direction *= -1;
            }
        }
    }
}
