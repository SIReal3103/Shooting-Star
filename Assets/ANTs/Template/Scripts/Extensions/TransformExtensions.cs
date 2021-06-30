using UnityEngine;


static public class TransformExtensions
{
    static public void SetParentPreserve(this Transform trans, Transform parentToSet)
    {
        Vector3 localPosition = trans.localPosition;
        Quaternion localRotation = trans.localRotation;
        trans.SetParent(parentToSet);
        trans.localRotation = localRotation;
        trans.localPosition = localPosition;
    }
}
