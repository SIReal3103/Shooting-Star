using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(MoverAction))]
    public class Character : MonoBehaviour
    {
        [SerializeField] Transform model;

        private MoverAction mover;

        private void Awake()
        {
            mover = GetComponent<MoverAction>();
        }

        private void Start()
        {
        }

        private void Update()
        {

        }


    }
}