using UnityEngine;

namespace ANTs.Game
{
    [RequireComponent(typeof(BaseStat))]
    public class Experience : MonoBehaviour
    {
        public event System.Action OnExperienceUpdateEvent;
        [SerializeField] float currentExperience = 0;

        private void Start()
        {
            OnExperienceUpdateEvent?.Invoke();
        }

        public float GetExperience()
        {
            return currentExperience;
        }

        public void GainExperience(float experience)
        {
            currentExperience += experience;
            OnExperienceUpdateEvent?.Invoke();
        }
    }
}