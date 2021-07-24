using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToDestroy;

    public void Destroy()
    {
        Destroy(gameObjectToDestroy);
    }
}
