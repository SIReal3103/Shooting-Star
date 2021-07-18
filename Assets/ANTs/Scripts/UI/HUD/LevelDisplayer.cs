using ANTs.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplayer : MonoBehaviour
{
    private Text text;
    private BaseStat playerStat;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStat>();
    }

    private void Update()
    {
        text.text = playerStat.GetLevel().ToString();
    }
}
