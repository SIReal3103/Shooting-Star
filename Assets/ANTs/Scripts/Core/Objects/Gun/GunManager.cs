using ANTs.Template;
using UnityEngine;

namespace ANTs.Core
{
    public class GunManager : Singleton<GunManager>
    {
        [SerializeField] Gun defaultGunPrefab;

        private ExpandedDictionary<Gun> gunDictionary;

        private void Start()
        {
            gunDictionary = new ExpandedDictionary<Gun>(gameObject);
        }

        public Gun GetNextGun(Gun currentGun)
        {
            return gunDictionary.GetNextItem(currentGun);
        }

        public Gun GetDefaultGunPrefab()
        {
            return defaultGunPrefab;
        }
    }
}