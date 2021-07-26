﻿using UnityEngine;

public class HubAndLimitFPS : MonoBehaviour
{
    [SerializeField] int targetFPS = 60;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
    }
}
