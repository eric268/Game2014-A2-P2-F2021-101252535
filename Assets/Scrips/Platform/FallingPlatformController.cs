//----------------FallingPlatformController.cs-------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 10 2021----------------------------------
//  The file containts the script used collapsing and respawning cloud platforms
//  Revision History : 1.2 Platform repawns after alloted time 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fallig platform class
public class FallingPlatformController : MonoBehaviour
{
    bool m_bCollidedWithPlayer;
    bool m_bStartingRespawn;

    public float m_fStopPlatformValue;
    public float m_fBeginFallTimer;
    public float m_fBeingFallCounter;
    public float m_fRespawnTimer;
    public float m_fRespawnCounter;

    public Vector2 m_vStartingPosition;
    public Rigidbody2D m_rigidBody;
    public BoxCollider2D m_collider;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_vStartingPosition = transform.position;
        m_collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Starts the counter to when the platform will collapse on collision with player
        if (m_bCollidedWithPlayer)
            m_fBeingFallCounter += Time.deltaTime;

        if (m_fBeingFallCounter >= m_fBeginFallTimer)
        {
            CollapsePlatform();
        }

        if (!m_bStartingRespawn && m_rigidBody.transform.position.y <= m_fStopPlatformValue)
        {
            StopPlatformFalling();
        }

        if (m_bStartingRespawn)
        {
            RespawnPlatform();
        }


    }
    //Platform begins to collapse
    void CollapsePlatform()
    {

        //If the counter exceeds timer platform collapses
            m_bCollidedWithPlayer = false;
            m_fBeingFallCounter = 0.0f;

            if (m_rigidBody)
                m_rigidBody.bodyType = RigidbodyType2D.Dynamic;
            m_collider.enabled = false;
        
    }
    //Platform has fallen far enough off screen so stop updating it
    void StopPlatformFalling()
    {
        //If platform has moved far enough off the screen stop it then begin respawn process

            m_rigidBody.velocity = Vector2.zero;
            if (m_rigidBody)
                m_rigidBody.bodyType = RigidbodyType2D.Static;
            m_collider.enabled = true;
            m_bStartingRespawn = true;
        
    }
    //Return platform to starting position with reset values
    void RespawnPlatform()
    {
        //If platform has begun respawning and that timer has been met respawn it
            m_fRespawnCounter += Time.deltaTime;
            if (m_fRespawnCounter >= m_fRespawnTimer)
            {
                m_fRespawnCounter = 0.0f;
                m_bStartingRespawn = false;
                transform.position = m_vStartingPosition;
            }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Only want to collapse platform when collided with the top of platform/bottom of player
            if (other.contacts[0].normal == Vector2.down)
            {
                //Start collapsing process
                m_bCollidedWithPlayer = true;
            }
        }
    }
}
