using ANTs.Game;
using UnityEngine;
using UnityEngine.UI;

namespace ANTs.UI
{
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
}