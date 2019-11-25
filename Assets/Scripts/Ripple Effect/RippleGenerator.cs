using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RippleGenerator : MonoBehaviour
{
    public class Ripple
    {
        public int x
        {
            get { return _x; }
            private set { _x = x; }
        }
        private int _x;

        public int y
        {
            get { return _y; }
            private set { _y = y; }
        }
        private int _y;

        public Wave[] waves
        {
            get { return _waves; }
            private set { _waves = waves; }
        }
        private Wave[] _waves;

        public float dimmingRate
        {
            private set { _dimmingRate = dimmingRate; }
            get { return _dimmingRate;}
        }
        private float _dimmingRate;

        public float volume
        {
            private set { _volume = volume; }
            get { _volume -= _dimmingRate; return _volume; }
        }
        private float _volume;

        public Ripple(int cx, int cy, float volume, float dimmingRate, int radius)
        {
            _x = cx;
            _y = cy;
            _dimmingRate = dimmingRate;
            _volume = volume;
            _waves = new Wave[radius]; 
            for(int i = 0; i < radius; i++)
            {
                Wave wave = new Wave(_volume);
                _waves[i] = wave;
            }
        }

        public class Wave
        {
            public float Volume
            {
                private get { return _volume; }
                set { _volume = Volume; }
            } 
            private float _volume;

            private float _travelledDistance;

            public Wave(float volume)
            {
                _travelledDistance = 0;
                _volume = volume;
            }

            public float GetVolume(float travelled)
            {
                _travelledDistance += travelled;
                return _volume - _travelledDistance;
            }
        }
    }

    public class OnRipple : UnityEvent<Texture2D> { };

    public OnRipple onRipple = new OnRipple();
    [SerializeField]
    private int _textureHeight;

    [SerializeField]
    private int _textureWidth;

    [SerializeField]
    private int _interval;

    private Texture2D _sampleTexture;

    private List<Ripple> _ripples;    

    // Start is called before the first frame update
    void Start()
    {
        _textureWidth = _textureWidth != 0 ? _textureWidth : 400;
        _textureHeight = _textureHeight != 0 ? _textureHeight : 400;
        _sampleTexture = new Texture2D(_textureWidth, 
                                       _textureHeight);
        _ripples = new List<Ripple>();
        Color[] heightMapDefault = new Color[_textureWidth*_textureHeight];

        Color[] canvas = _sampleTexture.GetPixels();

        for (int i = 0; i < canvas.Length; i++)
        {
            heightMapDefault[i] = new Color(0.5f, 0.5f, 0.5f);
        }

        _sampleTexture.SetPixels(heightMapDefault);
    }

    //Instantiates a ripple that will be drawn in the heightmap
    public IEnumerator GenerateRipple(Vector2 position, float rippleStrength, int radius, float timeOffset)
    {
        yield return new WaitForSeconds(timeOffset);

        Ripple ripple = new Ripple((int)position.x, (int)position.y, rippleStrength, 0.01f, radius);

        _ripples.Add(ripple);
        
        StartCoroutine(DrawRipple(_ripples[_ripples.Count-1]));
    }

    //Generates a ripple by drawing circles
    private IEnumerator DrawRipple(Ripple ripple)
    {
        bool lowestPoint = true;
        Ripple.Wave[] waves = ripple.waves;
        for (int i = 0; i < waves.Length; i++)
        {
            float intensity = Mathf.Sin(i*0.11f-1.8f)/Mathf.Pow(1.1f,i*0.11f-1.8f);

            float rgb = (intensity + 1.156f)/ 2.021f*0.3f+0.4f ;

            Debug.LogError(rgb);

            Color color = new Color(rgb,rgb,rgb);

            DrawCircle(ripple.x, ripple.y, i, color);

            onRipple.Invoke(_sampleTexture);

            if(i % _interval == 0)
                yield return new WaitForEndOfFrame();
        }

        foreach(Ripple.Wave wave in waves)
        {
            if(ripple.dimmingRate <= wave.GetVolume(ripple.dimmingRate))
            {
                lowestPoint = false;
                break;
            }
        }

        //if (!lowestPoint)
        //    StartCoroutine(DrawRipple(ripple));
    }

    //Draw a circle on the sampleTexture
    private void DrawCircle(int x, int y, int r, Color color)
    {
        float radius = Mathf.Ceil(r / Mathf.Sqrt(2));
        int diamater = 1 / 4 - r;
        
        for (int p = 0; p <= radius; p++)
        {
            _sampleTexture.SetPixel(x + p, y + r, color);
            _sampleTexture.SetPixel(x + p, y - r, color);
            _sampleTexture.SetPixel(x - p, y + r, color);
            _sampleTexture.SetPixel(x - p, y - r, color);
            _sampleTexture.SetPixel(x + r, y + p, color);
            _sampleTexture.SetPixel(x + r, y - p, color);
            _sampleTexture.SetPixel(x - r, y + p, color);
            _sampleTexture.SetPixel(x - r, y - p, color);

            diamater += 2 * p + 1;
            if (diamater > 0)
            {
                diamater += 2 - 2 * r--;
            }
        }

        _sampleTexture.Apply();
    }
}
