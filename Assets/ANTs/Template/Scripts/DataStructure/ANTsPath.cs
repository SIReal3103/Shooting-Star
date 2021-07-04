using UnityEngine;

namespace ANTs.Core
{
    public class ANTsPath : MonoBehaviour
    {
        [SerializeField] bool isLoop;

        private int currentIndex = 0;
        private int sign = 1;

        public int GetCurrentIndex() { return currentIndex; }
        public Vector2 GetPosition() { return GetPositionOfChild(currentIndex); }
        public void Progress() { 
            if(isLoop)
            {
                currentIndex = GetNextChildIndex(currentIndex); 
            }
            else
            {                
                if(currentIndex == GetChildCount() - 1)
                {
                    sign = -1;
                }
                else if(currentIndex == 0)
                {
                    sign = 1;
                }
                currentIndex += sign;
            }
        }
        public void ResetIndex() { 
            currentIndex = 0;
            sign = 1;
        }
       
        private void OnDrawGizmos()
        {
            for (int i = 0; i < GetChildCount() - (isLoop ? 0 : 1); i++)
                Gizmos.DrawLine(GetPositionOfChild(i), GetPositionOfChild(GetNextChildIndex(i)));
        }

        private int GetNextChildIndex(int i)
        {              
            return (i + 1) % GetChildCount();
        }        

        private Vector2 GetPositionOfChild(int i)
        {
            return transform.GetChild(i).position;
        }

        private int GetChildCount()
        {
            return transform.childCount;
        }
    }
}