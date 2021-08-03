using System.Collections;
using UnityEngine;

namespace ANTs.Template.UI
{
    public class DisplayOnHUDComponent : MonoBehaviour
    {
        public int displayOrder = 0;

        private IDisplayOnHUD[] displayOnHUDs;

        private void Awake()
        {
            displayOnHUDs = GetComponents<IDisplayOnHUD>();
        }

        private void Start()
        {
            UI_HUD.Instance.AddDisplayComponent(this);
        }

        public Rect OnDisplayOnHUD(Rect offset)
        {
            foreach(IDisplayOnHUD displayOnHUD in displayOnHUDs)
            {
                foreach(string displayInfo in displayOnHUD.GetDisplayInfos())
                {
                    GUI.Label(offset, displayInfo);
                    offset = new Rect(offset.x, offset.y + offset.height, offset.width, offset.height);
                }
            }
            return offset;
        }
    }
}