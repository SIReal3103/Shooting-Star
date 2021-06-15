using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    [RequireComponent(typeof(Character))]
    public class FiringBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Add this class to player for auto firing.
        ///
        /// </summary>
        /// 

        

        [SerializeField] float timeBetweenFire = 0.5f;
        float timeSinceLastFire = Mathf.Infinity;

        /// Player Gun
        [Tooltip("Character Gun")]
        public GameObject PlayerGun;

        Gun _currentGun;
        Character _character;

        private void Start()
        {
            _character = GetComponent<Character>();

            _currentGun = PlayerGun.GetComponent<Gun>();
            ChangeGun(PlayerGun);
        }

        private void Update()
        {
            FireBehaviour();

            UpdateTimer();
        }

        public void ChangeGun(GameObject gun)
        {
            if (_currentGun != null) Destroy(_currentGun.gameObject);

            _currentGun = Instantiate(gun, transform.position, Quaternion.identity).GetComponent<Gun>();
            _currentGun._character = _character;
        }

        private void UpdateTimer()
        {
            timeSinceLastFire += Time.deltaTime;
        }

        public void FireBehaviour()
        {
            if (timeSinceLastFire > timeBetweenFire)
            {
                _currentGun.Firing();

                timeSinceLastFire = 0;
            }
        }
    }
    
}
