using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RippleGenerator : MonoBehaviour
{
    public class OnRipple : UnityEvent<Texture> { };

    public OnRipple onRipple = new OnRipple();

    [SerializeField]
    private int _textureHeight;

    [SerializeField]
    private int _textureWidth;

    [SerializeField]
    private int _interval;

    [SerializeField]
    private Texture _rippleBrush;

    [SerializeField]
    private Texture _defaultHeightMap;

    [SerializeField]
    private RenderTexture _bufferTexture;

    [SerializeField]
    private RenderTexture _finalTexture;

    private List<Ripple> _ripples;

    // Start is called before the first frame update
    void Start()
    {
        _textureWidth = _textureWidth != 0 ? _textureWidth : 400;
        _textureHeight = _textureHeight != 0 ? _textureHeight : 400;

        _ripples = new List<Ripple>();
    }
}
