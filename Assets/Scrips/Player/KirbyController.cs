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
    public KirbyAnimationState animState;
    bool punchPressed = false;

    public PhysicsMaterial2D noFrictionMaterial;
    public PhysicsMaterial2D hasFrictionMaterial;

    // Start is called before the first frame update
    void Start()
    {
        mass = m_rigidBody.mass * m_rigidBody.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Move();
        checkIfGrounded();
        Debug.Log(isGrounded);
    }

    void checkIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(centeredPosition.position, groundedRadius, Vector2.down, groundedRadius, groundLayerMask);

        if (!isGrounded && hit)
        {
            animState = KirbyAnimationState.LANDING;
            m_animtationController.SetInteger("AnimState", (int)animState);
            currentJumpIndex = 0;
        }

        isGrounded = (hit) ? true : false;
    }

    private void Move()
    {
        float x = (Input.GetAxis("Horizontal") + joyStick.Horizontal) * movementSensativity;
        m_rigidBody.AddForce(new Vector2(x * horizontalForce, 0) * mass);

        float jump = 0;

        Debug.Log("X: " + x);
        Debug.Log("ANim state: " + (int)animState);


        if (x != 0)
        {
            FlipAniamtion(x);
            if (isGrounded)
            {
                animState = KirbyAnimationState.WALKING;
                m_animtationController.SetInteger("AnimState", (int)animState);
            }
        }
        else if (m_rigidBody.velocity.x == 0 &&  isGrounded && animState != KirbyAnimationState.IDLE)
        {
            animState = KirbyAnimationState.IDLE;
            m_animtationController.SetInteger("AnimState", (int)animState);
        }
        
        if (isGrounded)
        {
            jump = Input.GetAxisRaw("Jump");
            if (jump > 0)
            {
                animState = KirbyAnimationState.JUMP;
                m_animtationController.SetInteger("AnimState", (int)animState);
                currentJumpIndex++;
                m_rigidBody.AddForce(new Vector2(0, verticalForce) * mass);
            }

        }
        else
        {
            animState = KirbyAnimationState.FLYING;
            m_animtationController.SetInteger("AnimState", (int)animState);
        }
        m_rigidBody.velocity *= 0.97f;
    }

    public void KirbyJump()
    {
        Debug.Log("Jump");
        if (currentJumpIndex < maxNumberJumps)
        {
            if (animState == KirbyAnimationState.IDLE || animState == KirbyAnimationState.WALKING)
            {
                animState = KirbyAnimationState.JUMP;
                m_animtationController.SetInteger("AnimState", (int)animState);
            }
            else if (animState != KirbyAnimationState.FLYING)
                animState = KirbyAnimationState.FLYING;

            if (!isGrounded)
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, 0);

            currentJumpIndex++;
            m_rigidBody.AddForce(new Vector2(0, verticalForce) * mass);
        }
    }

    public void KirbyPunch()
    {
        animState = KirbyAnimationState.PUNCH;
        m_animtationController.SetInteger("AnimState", (int)animState);
        punchPressed = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
           if (collision.contacts[0].normal == Vector2.up)
            {
                collision.gameObject.GetComponent<Collider2D>().sharedMaterial = hasFrictionMaterial;
            }
            else
                collision.gameObject.GetComponent<Collider2D>().sharedMaterial = noFrictionMaterial;
        }

    }
}
