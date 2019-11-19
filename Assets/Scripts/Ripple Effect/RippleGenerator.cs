using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleGenerator : MonoBehaviour
{
    [SerializeField]
    private Texture2D _rippleTexture;

    [SerializeField]
    private Color _colorRangeMin;

    [SerializeField]
    private Color _colorRangeMax;

    private int _textureHeight;
    private int _textureWidth;

    // Start is called before the first frame update
    void Start()
    {
        _textureHeight = _rippleTexture.height;
        _textureHeight = _rippleTexture.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GenerateRipples(int Ripples, float size, float timeOffset)
    {
        yield return new WaitForSeconds(timeOffset);
    }

    private void drawCircle(int x, int y, float radius, float thickness, Color color)
    {
        float rSquared = radius * radius;

        for (int w = 0; w < _rippleTexture.width; w++)
        {
            for (int h = 0; h < _rippleTexture.height; h++)
            {
                if ((x - h) * (x - h) + (y - h) * (y - h) < rSquared)
                {
                    _rippleTexture.SetPixel(w, h, color);
                }
            }
        }

        _rippleTexture.Apply();
    }
}
