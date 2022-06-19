using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentHpBar : MonoBehaviour
{
    [SerializeField]
    private Transform _hpForm;

    private List<Image> _hpLists = new List<Image>();

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            _hpLists.Add(_hpForm.GetChild(i).GetComponent<Image>());
        }
    }

    public void SetHpBar(float value, float max)
    {
        for (int i = 0; i < _hpLists.Count; i++)
        {
            _hpLists[i].fillAmount = value / max;
        }
    }
}
