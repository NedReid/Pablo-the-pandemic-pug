using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    [SerializeField] private float tileX = 1;
    [SerializeField] private float tileY = 1;
    Mesh mesh;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mesh = GetComponent<MeshFilter>().mesh;

    }

    void Update()
    {
        List<float> max1 = new List<float>();
        max1.Add(transform.lossyScale.x);
        max1.Add(transform.lossyScale.y);
        max1.Add(transform.lossyScale.z);
        max1.Remove(Mathf.Min(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z));

        mat.mainTextureScale = new Vector2(max1[0] / 100 * tileX, max1[1] / 100 * tileY);
    }
}
