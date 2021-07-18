using System.Collections;
using UnityEngine;

namespace ANTs.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageText;

        public void Spawn(float damage)
        {
            DamageText text = Instantiate(damageText, transform);
            text.SetDamage(damage);
        }
    }
}