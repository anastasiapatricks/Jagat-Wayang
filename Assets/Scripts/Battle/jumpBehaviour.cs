using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class jumpBehaviour : StateMachineBehaviour
{
    public float timer;
    public float minTime, maxTime;

    public float evadeRange;
    private bool isEvading;
    public float safeDistance;
    public float evadeSpeed;

    private Transform playerPos;
    public float speed;

    public Rigidbody2D body2D;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

        timer = Random.Range(minTime, maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }


        if (playerPos == null)
        {
            return;
        }

        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);

        var xDiff = Mathf.Abs(playerPos.position.x - animator.transform.position.x);

        if (!isEvading && xDiff < evadeRange)
        {
            isEvading = true;
        }

        if (isEvading)
        {
            if (playerPos.position.x < animator.transform.position.x)
            {
                animator.transform.position += Vector3.right * Time.deltaTime * evadeSpeed;
            }
            else if (playerPos.position.x > animator.transform.position.x)
            {
                animator.transform.position += Vector3.left * Time.deltaTime * evadeSpeed;
            }
            if (xDiff > safeDistance)
            {
                isEvading = false;
            }
        }
        else if (target != null)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            animator.SetTrigger("idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
                                                                                                                                                  
    }
}
