//-------------------------KirbyController.cs--------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 11 2021----------------------------------
//  The file containts the script used for moving and animating Kirby 
//  Revision History : 1.0 Platform now moves along a route 

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Kirby controller class
public class KirbyController : MonoBehaviour
{
    public Joystick joyStick;
    public KirbyAnimationState animState;
    float punchColliderTimer = 0.65f;
    float punchColliderCounter = 0.0f;

    [Header("Attributes")]
    public int numLives;
    public Transform centeredPosition;
    public Vector2 startingPos;
    bool attackingWithSword = false;
    public bool kirbyDead;
    public float dealthTimer;
    public float mass;
    public float spriteScale;

    [Header("Jumping")]
    int currentJumpIndex = 0;
    static int maxNumberJumps = 3;

    [Header("Movement")]
    public bool keyboardJumpButtonReleased;
    public bool keyboardJumpButtonDown;
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public float movementSensativity = 1.0f;

    [Header("Components")]
    public Level1UIManager uiControllerRef;
    public SwordController swordCollider;
    public Animator m_animtationController;
    public GameObject m_kirbySword;
    public Rigidbody2D m_rigidBody;

    [Header("Collision")] 
    public PhysicsMaterial2D noFrictionMaterial;
    public PhysicsMaterial2D hasFrictionMaterial;
    public CircleCollider2D capsuleCollider;
    public float groundedRadius;
    public LayerMask groundLayerMask;

    // Start is called before the first frame update
    private void Start()
    {
        mass = m_rigidBody.mass * m_rigidBody.gravityScale;
        startingPos = transform.position;
        capsuleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        CheckKeyboardJumpInput();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
        SwordAttackTimer();
        Debug.Log(animState);
    }

    //Checks if Kirby is on the ground/platform
    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(centeredPosition.position, groundedRadius, Vector2.down, groundedRadius, groundLayerMask);

        if (!isGrounded && hit)
        {
            animState = KirbyAnimationState.LANDING;
            m_animtationController.Play("Landing");
            m_animtationController.SetInteger("AnimState", (int)animState);
            currentJumpIndex = 0;
            SoundEffectManager.PlaySound("Landing");
        }

        isGrounded = (hit) ? true : false;
    }

    void CheckKeyboardJumpInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            keyboardJumpButtonReleased = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            keyboardJumpButtonDown = true;
    }

    //Moves Kirby
    private void Move()
    {
        if (!kirbyDead && !attackingWithSword)
        {
            //Moves Kirby along x axis
            float x = (Input.GetAxisRaw("Horizontal") + joyStick.Horizontal) * movementSensativity;
            Debug.Log(x);
            m_rigidBody.AddForce(new Vector2(x * horizontalForce, 0) * mass);

            if (x != 0)
            {
                //Only want to flip direction facing when Kirby is not attack. Is a balancing feature
                FlipAniamtion(x);
                //Kirby is moving on x axis and on ground so play walking animation
                if (isGrounded)
                {
                    animState = KirbyAnimationState.WALKING;
                    m_animtationController.SetInteger("AnimState", (int)animState);
                }
                else
                {
                    animState = KirbyAnimationState.FLYING;
                    m_animtationController.SetInteger("AnimState", (int)animState);
                }
            }
            //Kirby is grounded and not moving so play idle animation
            else if (m_rigidBody.velocity.x <= 0.05f)
            {
                if (isGrounded)
                {
                    animState = KirbyAnimationState.IDLE;
                    m_animtationController.SetInteger("AnimState", (int)animState);
                    //m_animtationController.Play("Idle");
                }
                else
                {
                    animState = KirbyAnimationState.FLYING;
                    m_animtationController.SetInteger("AnimState", (int)animState);
                }

            }
            KeyboardKirbyJump();
            m_rigidBody.velocity *= 0.97f;
        }
    } 

    public void KeyboardKirbyJump()
    {
        if (keyboardJumpButtonReleased && keyboardJumpButtonDown && !kirbyDead)
        {
            keyboardJumpButtonReleased = false;
            KirbyJump();
        }
    }
    //Jump button pressedd (A)
    public void KirbyJump()
    {
        if (!kirbyDead)
        {
            if (currentJumpIndex < maxNumberJumps)
            {
                SoundEffectManager.PlaySound("Jump");

                if (animState == KirbyAnimationState.IDLE || animState == KirbyAnimationState.WALKING)
                {
                    animState = KirbyAnimationState.JUMP;
                    m_animtationController.SetInteger("AnimState", (int)animState);
                }
                else if (animState != KirbyAnimationState.FLYING)
                    animState = KirbyAnimationState.FLYING;

                //if (!isGrounded)
                //    m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, 0);

                currentJumpIndex++;
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, 0);
                m_rigidBody.AddForce(new Vector2(m_rigidBody.velocity.x, verticalForce) * mass);
            }
        }
    }
    // Kirby used sword attack (B button pressed)
    public void SwordAttack()
    {
        if (!kirbyDead)
        {
            animState = KirbyAnimationState.SWORDATTACK;
            m_animtationController.SetInteger("AnimState", (int)animState);
            attackingWithSword = true;
            m_kirbySword.SetActive(true);
        }
    }
    //Change direction Kirby is facing
    float FlipAniamtion(float x)
    {
        x = (x > 0) ? 1 : -1;
        transform.localScale = new Vector3(x * spriteScale, spriteScale, spriteScale);
        return x;
        
    }
    //Check if sword attack is active or finished
    private void SwordAttackTimer()
    {
        if (attackingWithSword)
        {
            punchColliderCounter += Time.deltaTime;
            if (punchColliderCounter >= punchColliderTimer)
            {
                punchColliderCounter = 0.0f;
                attackingWithSword = false;
                m_kirbySword.SetActive(false);
            }
        }

    }
    //Corountine for playing Kirby death animation
    IEnumerator KirbyDeath()
    {
        SoundEffectManager.PlaySound("Death");
        yield return new WaitForSeconds(dealthTimer);
        ResetPlayer();
    }
    //Kirby died
    public void KirbyLostLife()
    {
        numLives--;
        kirbyDead = true;
        //Checks if game is over
        if (numLives <= 0)
        {
            GameOver(false);
        }
        //Starts death coroutine, updates UI
        else
        {
            uiControllerRef.UpdateHeartUI();
            StartCoroutine(KirbyDeath());
            capsuleCollider.enabled = false;
            animState = KirbyAnimationState.DEATH;
            m_animtationController.Play("Death");
            //m_animtationController.SetInteger("AnimState", (int)animState);
        }

    }
    //Saves game informatin for game over scene
    private void GameOver(bool gameWon)
    {
        int won = (gameWon) ? 1 : 0;
        PlayerPrefs.SetInt("Game Won", won);
        PlayerPrefs.SetInt("Score", Level1UIManager.score);
        PlayerPrefs.SetInt("Game Saved", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }
    //Moves Kirby to starting position and game resumes
    private void ResetPlayer()
    {
        kirbyDead = false;
        transform.position = startingPos;
        capsuleCollider.enabled = true;
        animState = KirbyAnimationState.IDLE;
        m_animtationController.SetInteger("AnimState", (int)animState);
        m_animtationController.Play("Idle");
    }
    //Increases score and removes coin when collected
    private void CoinPickedUp(GameObject coin)
    {
        Level1UIManager.score +=coin.GetComponent<CoinController>().scoreValue;
        Destroy(coin);
    }
    //Checks collision with environment objects
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

    //Checks that kirby is now not on moving platform so removes it as parent
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
    //Checks if Kirby collected a coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            SoundEffectManager.PlaySound("Coin");
            CoinPickedUp(collision.gameObject);
        }
    }
    //Debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centeredPosition.position, groundedRadius);
    }
}
