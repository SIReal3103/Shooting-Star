using UnityEngine;
using UnityEngine.UI;

namespace ANTs.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] Text text;

        public void SetDamage(float damage)
        {
            text.text = string.Format("{0:0}", damage);
        }
    }
}