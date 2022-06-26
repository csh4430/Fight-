using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using ToolBox.Pools;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { 
        get 
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    [SerializeField]
    private Transform Player;

    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private GameObject enemySpawner;

    [SerializeField]
    private List<Transform> EnemySpawnPos = new List<Transform>(); 

    public List<Collider> _mapCol = new List<Collider>();

    private int _enemyCount = 8;
    private int _stage = 0;

    public void FadeCamera(bool on, Action callback = null)
    {
        fadeImage.DOFade(on ? 1 : 0, 2).onComplete += () => callback?.Invoke();
    }

    public void SummonEnemy(int index)
    {

        if (_stage != 0)
            _mapCol[_stage - 1].enabled = false;
        GameObject spawner = null;
        spawner = Instantiate(enemySpawner, EnemySpawnPos[index]);
        _enemyCount = 8;
        for (int i =0; i < spawner.transform.childCount; i++)
        {
            spawner.transform.GetChild(i).Find("AI").GetComponent<AIBase>().TargetPos = Player;
        }
    }

    public void KillEnemy()
    {
        _enemyCount--;
        if(_enemyCount <= 0)
        {
            if (_stage >= _mapCol.Count)
                return;
            _mapCol[_stage++].isTrigger = true;
        }
    }

    public void GameClear()
    {
        FadeCamera(true, () =>
        {
            SceneManager.LoadScene("Clears");
        });
        _mapCol[3].enabled = false;
    }

    public void GameFail()
    {
        FadeCamera(true, () =>
        SceneManager.LoadScene("Dead"));
    }

    private void Awake()
    {
        FadeCamera(false);
    }
}
