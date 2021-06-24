using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(PlayerFacadel))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerFacadel player;

        private void Start()
        {
            player = GetComponent<PlayerFacadel>();
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