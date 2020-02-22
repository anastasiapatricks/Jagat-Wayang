using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    public int sceneNumber = 0;
    public Scene[] scenes;

    public UnityEvent onFinish;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialog;

    public Color greyOutColor = Color.grey;

    void Start()
    {
        ShowScene(sceneNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            sceneNumber++;
            if (sceneNumber < scenes.Length) {
                ClearScene(sceneNumber - 1);
                ShowScene(sceneNumber);
            } else {
                onFinish.Invoke();
            }
        }
    }

    void ShowScene(int sceneNumber) {
        Scene scene = scenes[sceneNumber];

        scene.gameObject.SetActive(true);

        characterName.text = scene.characterName;
        dialog.text = scene.dialog;

        if (scene.leftSprite != null && scene.leftGreyOut) {
            scene.leftSprite.color = greyOutColor;
        }

        if (scene.rightSprite != null && scene.rightGreyOut) {
            scene.rightSprite.color = greyOutColor;
        }
    }

    void ClearScene(int sceneNumber) {
        Scene scene = scenes[sceneNumber];
        scene.gameObject.SetActive(false);
    }
}
