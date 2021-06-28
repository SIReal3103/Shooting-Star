using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(PlayerControl))]
    public class InputHandler : MonoBehaviour
    {
        private PlayerControl player;

        private void Start()
        {
            player = GetComponent<PlayerControl>();
        }

        private void Update()
        {
            player.StartMovingTo(GetMousePosition());

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.ChangeStrongerGun();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                player.ChangeStrongerBullet();
            }
        }

        private static Vector2 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}