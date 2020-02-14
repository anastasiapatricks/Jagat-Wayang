using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void LevelCleared() {
        print("Congratulations, Human. You have passed the Turning Test. You may have some toast.");
        GameManager.Instance.LoadScene("CutsceneEnding");
    }
}
