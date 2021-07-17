using UnityEngine;
using UnityEngine.UI;

namespace ANTs.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] Transform Foreground;
        [SerializeField] Text text;

        public void OnHealthUpdate(float health, float maxHealth)
        {
            Foreground.localScale = new Vector3(health / maxHealth, 1);
            text.text = string.Format("{0:0}/{1:0}", health, maxHealth);
        }
    }
}