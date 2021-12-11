using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    bool updateSwordPosition = false;
    public Transform swordStartPos;
    public Transform swordEndPos;
    float delayTimer = 0.15f;
    float delayCounter = 0.0f;

    public Collider2D collidesWith;
    public ContactFilter2D contactFilter;
    public BoxCollider2D swordCollider;
    public List<Collider2D> colliderList;

    public LayerMask enemyBoxColliderMask;
    // Start is called before the first frame update

    private void OnEnable()
    {
        updateSwordPosition = true;
        transform.position = swordStartPos.position;
        delayCounter = 0.0f;
    }
    private void OnDisable()
    {
        updateSwordPosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (updateSwordPosition)
        {
            delayCounter += Time.deltaTime;

            Physics2D.GetContacts(swordCollider, contactFilter, colliderList);
            CheckCollisionWithEnemies();  
            if (delayCounter >= delayTimer)
                transform.localPosition = Vector2.Lerp(transform.localPosition, swordEndPos.localPosition, Time.deltaTime * 5.5f);
        }

    }

    void CheckCollisionWithEnemies()
    {
        foreach(Collider2D collider in colliderList)
        {
            if (collider.CompareTag("Enemy"))
            {
                UIController.score += 10;
                Destroy(collider.gameObject);
            }
        }
    }
}
