using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public UnityEvent onEnterRange;
    public UnityEvent onExitRange;
    public float minDistance;

    private PlayerHub player;
    private bool hasEntered;

    private void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerHub>();
    }

    private void Update()
    {
        if (!hasEntered && InsideRange()) {
            hasEntered = true;
            onEnterRange.Invoke();
        } else if (hasEntered && !InsideRange()) {
            hasEntered = false;
            onExitRange.Invoke();
        }

        if (InsideRange() && Input.GetKeyDown(player.interactKey)) {
            onInteract.Invoke();
        }
    }

    private bool InsideRange() {
        return Vector2.Distance(transform.position, player.transform.position) < minDistance;
    }
}
