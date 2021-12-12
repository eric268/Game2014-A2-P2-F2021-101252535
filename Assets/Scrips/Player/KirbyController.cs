using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KirbyController : MonoBehaviour
{
    public Joystick joyStick;
    public int numLives;
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
    public GameObject m_kirbySword;
    public Vector2 startingPos;
    bool attackingWithSword = false;
    float punchColliderTimer = 0.65f;
    float punchColliderCounter = 0.0f;

    public CircleCollider2D circleCollider;

    public bool kirbyDead;
    public float deathCounter;
    public float dealthTimer;

    public UIController uiControllerRef;
    public SwordController swordCollider;
    public PhysicsMaterial2D noFrictionMaterial;
    public PhysicsMaterial2D hasFrictionMaterial;

    // Start is called before the first frame update
    void Start()
    {
        mass = m_rigidBody.mass * m_rigidBody.gravityScale;
        startingPos = transform.position;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Move();
        checkIfGrounded();
        CheckPunchCollider(Time.deltaTime);

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
        if (!kirbyDead)
        {
            float x = (Input.GetAxis("Horizontal") + joyStick.Horizontal) * movementSensativity;
            m_rigidBody.AddForce(new Vector2(x * horizontalForce, 0) * mass);

            float jump = 0;

            if (x != 0)
            {
                if (!attackingWithSword)
                    FlipAniamtion(x);
                if (isGrounded)
                {
                    animState = KirbyAnimationState.WALKING;
                    m_animtationController.SetInteger("AnimState", (int)animState);
                }
            }
            else if (m_rigidBody.velocity.x == 0 && isGrounded && animState != KirbyAnimationState.IDLE)
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
    }

    public void KirbyJump()
    {
        if (!kirbyDead)
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
    }

    public void KirbyPunch()
    {
        if (!kirbyDead)
        {
            animState = KirbyAnimationState.PUNCH;
            m_animtationController.SetInteger("AnimState", (int)animState);
            attackingWithSword = true;
            m_kirbySword.SetActive(true);
        }
    }

    float FlipAniamtion(float x)
    {
        x = (x > 0) ? 1 : -1;
        transform.localScale = new Vector3(x * spriteScale, spriteScale, spriteScale);
        return x;
        
    }

    private void CheckPunchCollider(float deltaTime)
    {
        if (attackingWithSword)
        {
            punchColliderCounter += deltaTime;
            if (punchColliderCounter >= punchColliderTimer)
            {
                punchColliderCounter = 0.0f;
                attackingWithSword = false;
                m_kirbySword.SetActive(false);
            }
        }

    }

    IEnumerator KirbyDeath()
    {
        yield return new WaitForSeconds(dealthTimer);
        ResetPlayer();
    }

    public void KirbyLostLife()
    {
        numLives--;
        kirbyDead = true;
        if (numLives <= 0)
        {
            GameOver(false);
        }
        else
        {
            uiControllerRef.UpdateHeartUI();
            StartCoroutine(KirbyDeath());
            circleCollider.enabled = false;
            animState = KirbyAnimationState.DEATH;
            m_animtationController.Play("Death");
            //m_animtationController.SetInteger("AnimState", (int)animState);
        }

    }

    private void GameOver(bool gameWon)
    {
        int won = (gameWon) ? 1 : 0;
        PlayerPrefs.SetInt("Game Won", won);
        PlayerPrefs.SetInt("Score", UIController.score);
        PlayerPrefs.SetInt("Game Saved", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }

    private void ResetPlayer()
    {
        kirbyDead = false;
        transform.position = startingPos;
        circleCollider.enabled = true;
        animState = KirbyAnimationState.IDLE;
        m_animtationController.SetInteger("AnimState", (int)animState);
    }

    private void CoinPickedUp(GameObject coin)
    {
        UIController.score +=coin.GetComponent<CoinController>().scoreValue;
        Destroy(coin);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            if (collision.contacts[0].normal == Vector2.up)
            {
                collision.gameObject.GetComponent<Collider2D>().sharedMaterial = hasFrictionMaterial;
            }
            else
                collision.gameObject.GetComponent<Collider2D>().sharedMaterial = noFrictionMaterial;

        }
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.gameObject.transform.parent);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            KirbyLostLife();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            GameOver(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            CoinPickedUp(collision.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centeredPosition.position, groundedRadius);
    }
}
