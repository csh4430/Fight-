using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class IntroScene : MonoBehaviour
{
    public Text MainText;
    public Button replayButton;

    private Sequence seq;

    private void Awake()
    {

        seq = DOTween.Sequence();
        seq.Append(MainText.DOText("In this War, You Entered Enemy's kingdom.", 4).SetEase(Ease.Linear));
        seq.AppendInterval(2);
        seq.AppendCallback(() => MainText.text = "");
        seq.Append(MainText.DOText("And, You Ordered Your Soldiers", 4).SetEase(Ease.Linear));
        seq.AppendInterval(2);
        seq.AppendCallback(() => MainText.text = "");
        seq.Append(MainText.DOText("\"Bring Me The Enemy's King's Head\".", 4).SetEase(Ease.Linear));

        replayButton.onClick.AddListener(() =>
        {
            seq.Kill();
            SceneManager.LoadScene("Game");
        });
    }
}
