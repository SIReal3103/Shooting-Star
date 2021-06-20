using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(PlayerBehaviours))]
    public class PlayerInputHandler : MonoBehaviour
    {
        PlayerBehaviours player;

        private void Start()
        {
            player = GetComponent<PlayerBehaviours>();
        }

        private void Update()
        {
            player.MoveBehaviour(GetMousePosition());

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.ChangeStrongerGunBehaviour();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                player.ChangeStrongerBulletBehaviour();
            }
        }

        private static Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}