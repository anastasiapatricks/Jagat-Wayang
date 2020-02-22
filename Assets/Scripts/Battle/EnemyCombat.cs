using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float startTimeBtwAttack;
    private float timeBtwAttack;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 4f)
        {

            Attack();

            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void Attack()
    {
        // Play attack animation 

        // Detect enemies in range of attack 
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        if (hitPlayer.Length > 0)
        {
            Debug.Log("Damage euy");

            Player player = hitPlayer[0].GetComponentInParent<Player>();
            player.TakeDamage(attackDamage);
            player.knockbackCount = player.knockbackLength;
            player.knockFromRight = player.transform.position.x < transform.position.x;
        }


        /*foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Damage Taken");

            player.GetComponent<Player>().TakeDamage(attackDamage);
            player.GetComponent<Player>().knockbackCount = player.GetComponent<Player>().knockbackLength;

            if (player.GetComponent<Player>().transform.position.x < transform.position.x)
                player.GetComponent<Player>().knockFromRight = true;
            else
                player.GetComponent<Player>().knockFromRight = false;

        }*/
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
