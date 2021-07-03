using UnityEngine;
using ANTs.Template;

namespace ANTs.Core
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("IProgressable")]
        [Space(10)]
        [SerializeField] ProgressIdentifier currentLevel;
        [SerializeField] ProgressIdentifier nextLevel;

        protected GameObject owner;

        public ProgressIdentifier CurrentLevel { get => currentLevel; }
        public ProgressIdentifier NextLevel { get => nextLevel; }
    }

    public abstract class WeaponData
    {

    }
}