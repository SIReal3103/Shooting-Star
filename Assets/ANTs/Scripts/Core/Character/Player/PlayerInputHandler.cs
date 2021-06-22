using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(PlayerControlFacade))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerControlFacade player;

        private void Start()
        {
            player = GetComponent<PlayerControlFacade>();
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