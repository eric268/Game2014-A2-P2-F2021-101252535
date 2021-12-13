//-------------------------SwordController.cs-----------------------------------------
//----------------Author: Eric Galway 101252535---------------------------------------
//----------------Date Last Modified: Dec 10 2021-------------------------------------
//  The file containts the script used for checking collision with Kirbys sword attack
//  Revision History : 1.2 Sword lerps faster

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sword controller class
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

    //Sword has been enabled so start timer, move position etc
    private void OnEnable()
    {
        updateSwordPosition = true;
        transform.position = swordStartPos.position;
        delayCounter = 0.0f;
    }
    //Sword has been disabled so stop updated
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
            Move();
        }

    }
    //Moves sword
    private void Move()
    {
        if (delayCounter >= delayTimer)
            transform.localPosition = Vector2.Lerp(transform.localPosition, swordEndPos.localPosition, Time.deltaTime * 5.5f);
    }
    //Checks if sword is colliding with enemies
    void CheckCollisionWithEnemies()
    {
        foreach(Collider2D collider in colliderList)
        {
            if (collider.CompareTag("Enemy"))
            {
                Level1UIManager.score += 10;
                Destroy(collider.gameObject);
                SoundEffectManager.PlaySound("EnemyKilled");
            }
        }
    }
}
