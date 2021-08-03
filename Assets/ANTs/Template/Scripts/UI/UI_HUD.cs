using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ANTs.Template.UI
{
    public class UI_HUD : SingletonMonoBehaviour<UI_HUD>
    {
        Dictionary<int, List<DisplayOnHUDComponent>> displayComponents = new Dictionary<int, List<DisplayOnHUDComponent>>();

        public void AddDisplayComponent(DisplayOnHUDComponent displayComponent)
        {
            if (!displayComponents.ContainsKey(displayComponent.displayOrder))
            {
                displayComponents.Add(displayComponent.displayOrder, new List<DisplayOnHUDComponent>());
            }

            displayComponents[displayComponent.displayOrder].Add(displayComponent);
        }

        private void OnGUI()
        {
            Rect currentOffset = new Rect(0, 0, 100, 18);
            foreach (var pair in displayComponents.OrderBy(key => key.Key))
            {
                foreach (DisplayOnHUDComponent displayComponent in pair.Value)
                {
                    currentOffset = displayComponent.OnDisplayOnHUD(currentOffset);
                }
            }
        }
    }
}