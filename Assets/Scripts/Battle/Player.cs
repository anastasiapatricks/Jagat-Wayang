using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float speed = 15f;
	public Vector2 maxVelocity = new Vector2 (60, 100);
	public float flySpeed = 20f;
	public bool standing;
	public float standingThreshold = 4f;
	public float airSpeedMultiplier = .3f;

	private Rigidbody2D body2D;
	private PlayerController controller;
	private Animator animator;

    public Slider playerHealthBar;
    public int playerMaxHealth;
    private int playerCurrentHealth;
    public Slider playerAudienceSatisfaction;

    public GameObject bloodEffect;
    public Animator camAnim;

    private GameObject explosionRef;

    public float knockback;
    public float knockbackCount = 0.2f;
    public float knockbackLength;
    public bool knockFromRight;

    public string gameOverMessage = "YOU LOSE";

    // Use this for initialization
    void Start () {
		body2D = GetComponent<Rigidbody2D> ();
		controller = GetComponent<PlayerController> ();
		animator = GetComponent<Animator> ();

        playerCurrentHealth = playerMaxHealth;

        explosionRef = Resources.Load<GameObject>("Battle/Explosion");

    }

    // Update is called once per frame
    void Update () {

		var absVelX = Mathf.Abs (body2D.velocity.x);
		var absVelY = Mathf.Abs (body2D.velocity.y);

        standing = absVelY <= standingThreshold;

		var forceX = 0f;
		var forceY = 0f;

		if (controller.moving.x != 0) {
			if (absVelX < maxVelocity.x) {

				var newSpeed = speed * controller.moving.x;

				forceX = standing ? newSpeed : (newSpeed * airSpeedMultiplier);

                transform.localRotation = Quaternion.Euler(forceX < 0 ? Vector3.zero : new Vector3(0, 180, 0));
            }/*
            animator.SetInteger("AnimState", 1);
*/        }
        else {
/*			animator.SetInteger ("AnimState", 0);	
*/		}

		if (controller.moving.y > 0) {
			if (absVelY < maxVelocity.y) {
				forceY = flySpeed * controller.moving.y;
			}

/*			animator.SetInteger ("AnimState", 2);	
*/		} else if (absVelY > 0 && !standing) {
/*			animator.SetInteger ("AnimState", 3);	
*/		}


        if (knockbackCount <= 0)
        {
		    body2D.AddForce(new Vector2(forceX, forceY));
        } else
        {
            if (knockFromRight)
                body2D.velocity = new Vector2(-knockback, knockback/3);
            if (!knockFromRight)
                body2D.velocity = new Vector2(knockback, knockback/3);
            
            knockbackCount -= Time.deltaTime;
        }

        if (playerAudienceSatisfaction.value <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        playerHealthBar.value = playerCurrentHealth;
        CreateAndDestroyLater(bloodEffect, 1);
        camAnim.SetTrigger("shake");

        if (playerCurrentHealth <= 0)
        {
            Die();
        }


    }
    private void Die()
    {
            Debug.Log("Player died");

            // Die animation
            CreateAndDestroyLater(explosionRef, 1);
            Destroy(gameObject);

            // UIManager.Instance.DisplayPromptMessage(gameOverMessage);

            GameManager.Instance.LoadLevelLater("Hub", 3);

            // Disable enemy
    }

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
