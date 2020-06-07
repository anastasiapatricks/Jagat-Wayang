using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : LevelScript
{
    private void Update()
    {
        if (Input.GetButton("Interact"))
        {
            UIManager.Instance.AddFocus("companion");
        }
        if (Input.GetKey(KeyCode.Return))
        {
            GameManager.Instance.LoadScene("Battle");
        }
    }
}
