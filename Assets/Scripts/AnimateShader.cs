using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShader : MonoBehaviour
{
    Material _mat;

    bool _animation = false;

    [SerializeField] private float _animateTime = 1f;

    private void Start() 
    {
        _mat = GetComponent<Renderer>().material;
    }

    public void StartRoutineUp()
    {
        if (true)
        {
            StartCoroutine(AnimateUp());
        }
        _animation = true;
    }

    public void StartRoutineDown() {
        if (true) 
        {
            StartCoroutine(AnimateDown());
        }
        _animation = true;
    }

    IEnumerator AnimateUp()
    {
        float _value = 0f;
        while (_value < 1f)
        {
            _value += Time.deltaTime / _animateTime;
            _value = Mathf.Clamp01(_value);

            _mat?.SetFloat("_value", _value);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator AnimateDown() {
        float _value = 1f;
        while (_value > 0f) {
            _value -= Time.deltaTime / _animateTime;
            _value = Mathf.Clamp01(_value);

            _mat?.SetFloat("_value", _value);
            yield return new WaitForEndOfFrame();
        }
    }
}