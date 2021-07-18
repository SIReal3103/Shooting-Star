using UnityEngine;
using UnityEngine.Events;

namespace ANTs.Game
{
    public class BaseStat : MonoBehaviour
    {
        [System.Serializable]
        public class OnExperienceUpdateEvent : UnityEvent<float, float> { }

        [SerializeField] OnExperienceUpdateEvent OnExperienceUpdate;
        [Range(1, 100)]
        [SerializeField] int level = 1;
        [SerializeField] CharacterClass characterClass = CharacterClass.Player;
        [SerializeField] StatManager statManager;
        [SerializeField] bool shouldUseAdditiveProvider = true;

        private Experience currentExperience;

        private void Awake()
        {
            currentExperience = GetComponent<Experience>();

            if (currentExperience != null && level > 1)
            {
                currentExperience.GainExperience(GetStat(StatType.ExperienceToLevelUp, level - 1));
            }
        }

        private void OnEnable()
        {
            if (currentExperience != null)
            {
                currentExperience.OnExperienceUpdateEvent += OnLevelUpdate;
            }
        }

        public int GetLevel()
        {
            return level;
        }

        private void OnDisable()
        {
            if (currentExperience != null)
            {
                currentExperience.OnExperienceUpdateEvent -= OnLevelUpdate;
            }
        }

        private void OnLevelUpdate()
        {
            level = CalculateLevel(currentExperience.GetExperience());
            OnExperienceUpdate.Invoke(currentExperience.GetExperience(), GetStat(StatType.ExperienceToLevelUp));
        }

        private int CalculateLevel(float currentExperience)
        {
            float[] experiences = statManager.GetLevels(characterClass, StatType.ExperienceToLevelUp);
            for (int i = 0; i < experiences.Length; i++)
            {
                if (experiences[i] > currentExperience) return i + 1;
            }
            return experiences.Length;
        }

        public float GetStat(StatType statType, int level = -1)
        {
            if (level == -1) level = this.level;

            float result = GetBaseStat(statType, level);
            if (TryGetComponent(out Upgrade upgrade))
            {
                result += upgrade.GetBonus(statType);
            }
            return result;
        }

        private float GetBaseStat(StatType statType, int level)
        {
            return statManager.GetStat(characterClass, statType, level);
        }
    }
}