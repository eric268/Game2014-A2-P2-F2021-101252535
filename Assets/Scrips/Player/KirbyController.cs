using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyController : MonoBehaviour
{
    public Joystick joyStick;

    Rigidbody2D m_rigidBody;
    Animator m_animtationController;
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform centeredPosition;
    public float groundedRadius;
    public LayerMask groundLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkIfGrounded();
        Debug.Log(isGrounded);
    }

    void checkIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(centeredPosition.position, groundedRadius, Vector2.down, groundedRadius, groundLayerMask);

        isGrounded = (hit) ? true : false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centeredPosition.position, groundedRadius);
    }
}
