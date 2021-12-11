using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    bool m_bCollidedWithPlayer = false;
    bool m_bStartingRespawn = false;

    float m_fStopPlatformValue = -20.0f;
    float m_fBeginFallTimer = 1.5f;
    float m_fBeingFallCounter = 0.0f;
    float m_fRespawnTimer = 5.0f;
    float m_fRespawnCounter = 0.0f;

    Vector2 m_vStartingPosition;
    Rigidbody2D m_rigidBody;
    BoxCollider2D m_collider;
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

        //If the counter exceeds timer platform collapses
        if (m_fBeingFallCounter >= m_fBeginFallTimer)
        {
            m_bCollidedWithPlayer = false;
            m_fBeingFallCounter = 0.0f;

            if (m_rigidBody)
                m_rigidBody.bodyType = RigidbodyType2D.Dynamic;
            m_collider.enabled = false;
        }

        //If platform has moved far enough off the screen stop it then begin respawn process
        if (!m_bStartingRespawn && m_rigidBody.transform.position.y <= m_fStopPlatformValue)
        {
            m_rigidBody.velocity = Vector2.zero;
            if (m_rigidBody)
                m_rigidBody.bodyType = RigidbodyType2D.Static;
            m_collider.enabled = true;
            m_bStartingRespawn = true;
        }

        //If platform has begun respawning and that timer has been met respawn it
        if (m_bStartingRespawn)
        {
            m_fRespawnCounter += Time.deltaTime;
            if (m_fRespawnCounter >= m_fRespawnTimer)
            {
                m_fRespawnCounter = 0.0f;
                m_bStartingRespawn = false;
                transform.position = m_vStartingPosition;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.contacts[0].normal == Vector2.down)
            {
                m_bCollidedWithPlayer = true;
            }
        }
    }
}
