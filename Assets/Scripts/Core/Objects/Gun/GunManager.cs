using ANTs.Template;

namespace ANTs.Game
{
    public class GunManager : Singleton<GunManager>
    {
        ExpandedDictionary<Gun> gunDictionary;

        private void Start()
        {
            gunDictionary = new ExpandedDictionary<Gun>(gameObject);
        }

        public Gun GetNextGun(Gun currentGun)
        {
            return gunDictionary.GetNextItem(currentGun);
        }
    }
}