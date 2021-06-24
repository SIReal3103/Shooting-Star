using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(Mover))]
    public class Character : MonoBehaviour
    {
        [SerializeField] Transform model;

        private Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            model.localScale = new Vector2(IsFacingLeft() ? -1 : 1, 1);
        }

        private bool IsFacingLeft()
        {
            return mover.GetMoveDirection().x < 0f;
        }
    }
}