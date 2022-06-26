using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DeadScene : MonoBehaviour
{
    public Text MainText;
    public Text SubText;
    public Button replayButton;

    private Sequence seq;

    private void Awake()
    {

        seq = DOTween.Sequence();
        seq.Append(MainText.DOText("All Teamates Had to Die", 3).SetEase(Ease.Linear));
        seq.Append(SubText.DOText("Because of Your bad decisions", 2).SetEase(Ease.Linear));

        replayButton.onClick.AddListener(() =>
        {
            seq.Kill();
            SceneManager.LoadScene("Game");
        });
    }
}
