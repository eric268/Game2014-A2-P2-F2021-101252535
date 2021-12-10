using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyController : MonoBehaviour
{
    public Joystick joyStick;

    public Rigidbody2D m_rigidBody;
    public Animator m_animtationController;
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform centeredPosition;
    public float groundedRadius;
    public LayerMask groundLayerMask;
    public float movementSensativity = 1.0f;
    public float mass;
    public float spriteScale;
    [Header("Jumping")]
    int currentJumpIndex = 0;
    static int maxNumberJumps = 3;

    // Start is called before the first frame update
    void Start()
    {
        mass = m_rigidBody.mass * m_rigidBody.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkIfGrounded();
        Move();
        Debug.Log(isGrounded);
    }

    void checkIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(centeredPosition.position, groundedRadius, Vector2.down, groundedRadius, groundLayerMask);

        if (!isGrounded && hit)
        {
            currentJumpIndex = 0;
        }

        isGrounded = (hit) ? true : false;
    }

    private void Move()
    {
        float x = (Input.GetAxis("Horizontal") + joyStick.Horizontal) * movementSensativity;
        m_rigidBody.AddForce(new Vector2(x * horizontalForce, 0) * mass);

        float jump = 0;

        if (x != 0)
            FlipAniamtion(x);
        
        if (isGrounded)
        {
            jump = Input.GetAxisRaw("Jump");
            if (jump > 0)
            {
                currentJumpIndex++;
                m_rigidBody.AddForce(new Vector2(0, verticalForce) * mass);
            }

        }
        m_rigidBody.velocity *= 0.97f;
    }

    public void KirbyJump()
    {
        Debug.Log("Jump");
        if (currentJumpIndex < maxNumberJumps)
        {
            if (!isGrounded)
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, 0);

            currentJumpIndex++;
            m_rigidBody.AddForce(new Vector2(0, verticalForce) * mass);
        }
    }

    public void KirbyPunch()
    {
        Debug.Log("Punch");
    }

    float FlipAniamtion(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x * spriteScale, spriteScale);
        return x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centeredPosition.position, groundedRadius);
    }
}
