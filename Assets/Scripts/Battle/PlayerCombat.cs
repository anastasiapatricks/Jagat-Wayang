using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Animator camAnim;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float startTimeBtwAttack;

    private float timeBtwAttack;

    private const int MAX_HISTORY = 3;

    private List<(char, float)> inputHistory = new List<(char, float)>(MAX_HISTORY);
    private (string, float, int)[] comboList = { ("ADA", 0.5f, 4), ("QEQ", 0.5f, 3) };

    public Slider audience;
    public float neutralAudience = 50f;
    private float currentAudience;
    private Sprite audienceIcon;

    void Start()
    {
        currentAudience = neutralAudience;
        /*        audienceIcon = audience.image.GetComponent<Sprite>();
        */
        audienceIcon = audience.targetGraphic.GetComponent<Sprite>();
        /*        audienceIcon = audience.handleRect.GetComponent<Sprite>();
        */
        audienceIcon = Resources.Load<Sprite>("Battle/audienceIcon40_0");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0.3)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetInteger("AttackState", 1);
                /*                Attack();*/
                inputHistory.Add(('A', Time.time));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetInteger("AttackState", 2);
                /*                Attack();*/
                inputHistory.Add(('D', Time.time));
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                inputHistory.Add(('Q', Time.time));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                inputHistory.Add(('E', Time.time));
            }
            else
            {
                animator.SetInteger("AttackState", 0);
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        /*        // Health icon changes based on audience satisfaction level
                if (audience.value > 80)
                    audienceIcon = Resources.Load<Sprite>("audienceIcon80_0");
                else if (audience.value > 60)
                    audienceIcon = Resources.Load<Sprite>("audienceIcon60_0");
                else if (audience.value > 40)
                    audienceIcon = Resources.Load<Sprite>("audienceIcon40_0");
                else if (audience.value > 20)
                    audienceIcon = Resources.Load<Sprite>("audienceIcon20_0");
                else
                    audienceIcon = Resources.Load<Sprite>("audienceIcon0_0");*/

        // Decreasing audience satisfaction overtime
        currentAudience -= 0.05f;
        audience.value = currentAudience;

        // Handle combo detection
        int count = inputHistory.Count;
        if (count > MAX_HISTORY)
        {
            inputHistory.RemoveRange(0, count - MAX_HISTORY);
        }
        if (inputHistory.Count == 3)
        {
            bool isCombo((string, float, int) combo)
            {
                for (int i = 0; i < count; i++)
                {
                    if (inputHistory[i].Item1 != combo.Item1[i])
                    {
                        return false;
                    }
                }
                return inputHistory[count - 1].Item2 - inputHistory[0].Item2 < combo.Item2;
            }

            for (var i = 0; i < comboList.Length; i++)
            {
                if (isCombo(comboList[i]))
                {
                    print("combo " + i);
                    animator.SetInteger("AttackState", comboList[i].Item3);
                    currentAudience += 15f;
                    audience.value = currentAudience;
                    inputHistory.Clear();
                }
            }
        }
    }

    void Attack()
    {
        // Detect enemies in range of attack 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        currentAudience += 0.1f;
        audience.value = currentAudience;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
