using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AddPrefabToSurface : MonoBehaviour
{
    public GameObject Prefab;
    public int Count = 50;

    void Start()
    {
        var radius = GetComponent<SphereCollider>().radius;
        var largestScaledVertex = Mathf.Max(new[] { transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z });
        Vector3 center = transform.position;
        for (int i = 0; i < Count; i++)
        {
            Vector3 pos = RandomSurfacePoint(center, radius * largestScaledVertex);
            Instantiate(Prefab, pos, Quaternion.identity, transform);
        }
    }

    Vector3 RandomSurfacePoint(Vector3 center, float radius)
    {
        var u = Random.Range(0f, 1f);
        var v = Random.Range(0f, 1f);
        var theta = 2 * Math.PI * u;
        var phi = Math.Acos(2f * v - 1f);
        var x = center.x + (radius * Math.Sin(phi) * Math.Cos(theta));
        var y = center.y + (radius * Math.Sin(phi) * Math.Sin(theta));
        var z = center.z + (radius * Math.Cos(phi));
        return new Vector3((float)x, (float)y, (float)z);
    }
}
