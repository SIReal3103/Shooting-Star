﻿using UnityEngine;

public class HubAndLimitFPS : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = -1;
    }
}
