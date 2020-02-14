using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent onTrigger;

    public void Fire() {
        onTrigger.Invoke();
    }
}
