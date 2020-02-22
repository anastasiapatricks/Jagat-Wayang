using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Slider enemyHealthBar;
    public GameObject bloodEffect;

    public int enemyMaxHealth = 100;
    private int enemyCurrentHealth;

    // Materials
    private Material matWhite;
    private Material matDefault;

    private GameObject explosionRef;
    //SpriteRenderer sr;
    private Shake shake;

    public Animator camAnim;

    private Transform playerPos;
    private Transform enemyPos;
    private Transform enemyScale;
    public float speed;

/*    private Rigidbody2D body2D;
*/    public float knockback;
    public float knockbackCount;
    public float knockbackLength;
    public bool knockFromRight;


    // Start is called before the first frame update
    void Start()
    {
          enemyCurrentHealth = enemyMaxHealth;
/*        sr = GetComponent<SpriteRenderer>();
          matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
*/
          explosionRef = Resources.Load<GameObject>("Battle/Explosion");

        /*        shake = GameObject.FindGameObjectsWithTag("ScreenShake");
        */
/*          body2D = GetComponent<Rigidbody2D>();
*/

          playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
          enemyPos = GameObject.FindWithTag("Enemies").GetComponent<Transform>();
          enemyScale = GameObject.FindWithTag("Enemies").GetComponent<Transform>();
    }

    private void Update()
    {
        /*      EnemyMoveTowardsPlayer();
        */
        if (playerPos == null) return;
        enemyScale.localScale = (playerPos.position.x > enemyPos.position.x) && (playerPos.position != null) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        /*if (playerPos.position.x > enemyPos.position.x && playerPos.position.x != null)
        {
            enemyScale.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            enemyScale.localScale = new Vector3(1, 1, 1);
        }
*/    
    }


    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        enemyHealthBar.value = enemyCurrentHealth;
        CreateAndDestroyLater(bloodEffect, 1);
        camAnim.SetTrigger("shake");

        /*        shake.CamShake();
        */
        // play hurt animation
        /*        sr.material = matWhite;
        */
        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", .5f);
        }

        
        void Die()
        {
            Debug.Log("Enemy died");

            // Die animation
            CreateAndDestroyLater(explosionRef, 1);

            Destroy(gameObject);

            // Disable enemy
        }
    }
    void ResetMaterial()
    {
/*        sr.material = matDefault;
*/  }


/*    public void EnemyMoveTowardsPlayer()
    {
*//*        if (knockbackCount <= 10)
        {
            Vector2 target = new Vector2(playerPos.position.x, enemyPos.transform.position.y);
            enemyPos.transform.position = Vector2.MoveTowards(playerPos.transform.position, target, speed * Time.deltaTime);
        }
        else
        {*//*
            if (knockFromRight)
                body2D.velocity = new Vector2(-knockback, knockback / 3);
            if (!knockFromRight)
                body2D.velocity = new Vector2(knockback, knockback / 3);

            knockbackCount -= Time.deltaTime;
 *//*       }*//*
    }*/

    private void CreateAndDestroyLater(GameObject prefab, float seconds)
    {
        GameObject clone = Instantiate(prefab, transform.position, Quaternion.identity);
        IEnumerator DestroyLater()
        {
            yield return new WaitForSeconds(seconds);
            Destroy(clone);
        }

        StartCoroutine(DestroyLater());
    }
}
