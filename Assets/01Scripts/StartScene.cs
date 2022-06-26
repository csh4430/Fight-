using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public Transform cameraPos;
    public Transform targetPos;

    public GameObject BackgroundObject;

    public Button nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Intro");
        });
        DontDestroyOnLoad(BackgroundObject);
    }

    private void Update()
    {
        cameraPos.RotateAround(targetPos.position, Vector3.up, Time.deltaTime);        
    }
}
