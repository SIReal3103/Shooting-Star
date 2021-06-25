using UnityEngine;
using Panda;

namespace Assets.ANTs.Scripts.Core.Character.Enemy.Charger.BT.Tasks
{
    public class AttackPlayerTask : MonoBehaviour
    {
        [SerializeField] GameObject attackArea;

        private void Awake()
        {
            attackArea.SetActive(false);
        }

        
    }
}