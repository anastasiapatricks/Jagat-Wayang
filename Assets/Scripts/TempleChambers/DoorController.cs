using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayClose;
    }


    private void OnDoorwayOpen()
    {
        LeanTween.moveLocalY(gameObject, 4f, 1f).setEaseOutQuad();
    }
    private void OnDoorwayClose()
    {
        LeanTween.moveLocalY(gameObject, 1.6f, 2f).setEaseInQuad();
    }
}
