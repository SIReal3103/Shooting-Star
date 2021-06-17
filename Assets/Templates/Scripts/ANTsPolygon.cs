using System;

using UnityEngine;


public class ANTsPolygon : MonoBehaviour
{
    struct Edge
    {
        public Vector2 a;
        public Vector2 b;
    }

    public Vector2 GetRandomPointOnSurface()
    {
        Edge e = new Edge { a = GetRandomPointOnPath(), b = GetCenter() };
        return GetRandomPointBetween(e);
    }

    public Vector2 GetCenter()
    {
        Vector3 result = Vector3.zero;
        for(int i = 0; i < transform.childCount; i++)
        {
            result += GetPositionOfChild(i);
        }

        return result / transform.childCount;
    }

    public Vector2 GetRandomPointOnPath()
    {
        return GetRandomPointBetween(GetRandomEdge());
    }

    private Edge GetRandomEdge()
    {
        int a = RandomIntRange(0, transform.childCount);
        int b = GetNextChildIndex(a);
        return new Edge { a = GetPositionOfChild(a), b = GetPositionOfChild(b) };
    }

    private Vector2 GetRandomPointBetween(Edge edge)
    {
        Vector2 delt = edge.b - edge.a;
        return edge.a + delt * RandomBetween01();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
            Gizmos.DrawLine(GetPositionOfChild(i), GetPositionOfChild(GetNextChildIndex(i)));
    }

    private int GetNextChildIndex(int i)
    {
        return (i + 1) % transform.childCount;
    }

    private Vector3 GetPositionOfChild(int i)
    {
        return transform.GetChild(i).position;
    }

    private int RandomIntRange(int left, int right)
    {
        return UnityEngine.Random.Range(left, right);
    }

    private float RandomBetween01()
    {
        return UnityEngine.Random.value;
    }
}
