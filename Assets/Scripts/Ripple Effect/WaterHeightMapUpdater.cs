using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeightMapUpdater : MonoBehaviour
{
    private Material _material;

    void Start()
    {
        _material = gameObject.GetComponent<MeshRenderer>().material;
        gameObject.GetComponent<RippleGenerator>().onRipple.AddListener(ReplaceTexture);
    }
    // Event when there is a water ripple generated
    public void ReplaceTexture(Texture2D heightMap)
    {
        Debug.Log("Ripple");
        _material.SetTexture("_Water_Texture2D", heightMap);
    }
}
