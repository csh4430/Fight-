using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ClearScene: MonoBehaviour
{
    public Text MainText;
    public Text SubText;
    public Button replayButton;

    private Sequence seq;

    private void Awake()
    {

        seq = DOTween.Sequence();
        seq.Append(MainText.DOText("This War Could Be Stopped", 3).SetEase(Ease.Linear));
        seq.Append(SubText.DOText("It Was for All of you", 2).SetEase(Ease.Linear));

        replayButton.onClick.AddListener(() =>
        {
            seq.Kill();
            SceneManager.LoadScene("Start");
        });
    }
}
