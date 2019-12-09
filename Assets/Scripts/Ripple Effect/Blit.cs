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

    [SerializeField] private Texture _startCanvas;

    private List<Ripple> _ripples = new List<Ripple>();



    void Start()
    {
        if (!source || !destination)
        {
            Debug.LogError("A texture or a render texture are missing, assign them.");
        }
        frameBuffer.Release();
        destination.Release();
        //InvokeRepeating("GraphicsCopyTexture", 1.0f, 1.0f);
    }

    void Update()
    {
        Graphics.CopyTexture(_startCanvas, frameBuffer);

        GraphicsCopyTexture();

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(GenerateRipple(new Vector2(Random.Range(0,2048), Random.Range(0, 2048)),1,1,0));
        }
    }


    //Instantiates a ripple that will be drawn in the heightmap
    public IEnumerator GenerateRipple(Vector2 position, float rippleStrength, int waves, float timeOffset)
    {
        yield return new WaitForSeconds(timeOffset);

        Ripple ripple = new Ripple((int)position.x, (int)position.y, rippleStrength, 0.01f, waves, Time.time);

        _ripples.Add(ripple);
    }

    void GraphicsCopyTexture()
    {
        foreach (Ripple ripple in _ripples)
        {            
            Graphics.CopyTexture(source, 0, 0, 0, 0, Mathf.Min(frameBuffer.width-ripple.x, 128), 
                Mathf.Min(frameBuffer.height - ripple.y, 128), frameBuffer, 0, 0, ripple.x,ripple.y);

            Vector2 scale = new Vector2(1-(Time.time - ripple.age), 1-(Time.time - ripple.age));
            Vector2 offset = new Vector2((Time.time - ripple.age), (Time.time - ripple.age));

            Debug.Log(scale);

            Graphics.Blit(frameBuffer, destination,scale,offset);
        }

    }
}
