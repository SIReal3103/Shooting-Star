using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToDestroy;

    public void Destroy()
    {
        Destroy(gameObjectToDestroy);
    }
}
