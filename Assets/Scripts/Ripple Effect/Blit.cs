using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Blit : MonoBehaviour
{
    [SerializeField]
    Texture source;
    
    [SerializeField]
    RenderTexture destination;
    [SerializeField]
    RenderTexture frameBuffer;

    [SerializeField]
    private Material blendMaterial;

    [SerializeField] private Texture startCanvas;
    
    void Start()
    {
        if (!source || !destination)
        {
            Debug.LogError("A texture or a render texture are missing, assign them.");
        }
        //InvokeRepeating("GraphicsCopyTexture", 1.0f, 1.0f);
    }

    void Update()
    {
        
        //Graphics.CopyTexture(startCanvas, destination);
        //GraphicsBlit();
        GraphicsCopyTexture();
    }

    void GraphicsCopyTexture()
    {
        for (var i = 0; i < 10; i++)
        {
//            frameBuffer.Release();
            Graphics.CopyTexture(source, 0, 0, 0, 0, 128, 128, frameBuffer, 0, 0, Random.Range(0, 1900), Random.Range(0, 1900));
            Graphics.Blit(frameBuffer, destination);
        }
    }

    void GraphicsBlit()
    {
        var scale = new Vector2(1, 1);
        var offset = new Vector2(0, 0);
        Graphics.Blit(source, destination, scale, offset);
    }
}
