using UnityEngine;

public class HubAndLimitFPS : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = -1;
    }
}
