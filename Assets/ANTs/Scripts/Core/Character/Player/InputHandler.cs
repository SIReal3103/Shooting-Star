using UnityEngine;

namespace ANTs.Core
{
    [RequireComponent(typeof(PlayerFacade))]
    public class InputHandler : MonoBehaviour
    {
        private PlayerFacade player;

        private void Start()
        {
            player = GetComponent<PlayerFacade>();
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