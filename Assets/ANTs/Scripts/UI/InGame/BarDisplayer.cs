using UnityEngine;
using UnityEngine.UI;

namespace ANTs.UI
{
    public class BarDisplayer : MonoBehaviour
    {
        [SerializeField] Transform Foreground;
        [SerializeField] Text text;

        public void UpdateBar(float amount, float maxAmount)
        {
            Foreground.localScale = new Vector3(amount / maxAmount, 1);
            text.text = string.Format("{0:0}/{1:0}", amount, maxAmount);
        }
    }
}