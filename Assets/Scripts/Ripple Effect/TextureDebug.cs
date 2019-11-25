using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureDebug : MonoBehaviour
{
    public RippleGenerator ripples;

    private MeshRenderer mesh_renderer;

    void Start()
    {
        mesh_renderer = gameObject.GetComponent<MeshRenderer>();
        ripples.onRipple.AddListener(SetTexture);
    }

    public void SetTexture(Texture2D texture)
    {
        mesh_renderer.material.SetTexture("_Test_Texture2D", texture);
    }


}
