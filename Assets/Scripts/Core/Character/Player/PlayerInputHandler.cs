using System.Collections;
using UnityEngine;

namespace Game.Core
{
    [RequireComponent(typeof(PlayerBehaviour))]
    public class PlayerInputHandler : MonoBehaviour
    {
        PlayerBehaviour player;

        private void Start()
        {
            player = GetComponent<PlayerBehaviour>();
        }

        private void Update()
        {
            player.MoveBehaviour(GetMousePosition());
        }

        private static Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}