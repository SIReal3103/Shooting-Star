using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(PlayerControl))]
    public class InputHandler : MonoBehaviour
    {
        private PlayerControl player;

        private void Awake()
        {
            player = GetComponent<PlayerControl>();
        }

        private void Update()
        {
            //player.StartMovingWith(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

            player.StartMovingTo(GetMousePosition());

            if (Input.GetKeyDown(KeyCode.Z))
            {
                player.ChangeStrongerBullet();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.UpgradeWeapon();
            }
        }

        private static Vector2 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}