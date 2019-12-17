using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShader : MonoBehaviour
{
    MeshRenderer _meshRenderer;
    SkinnedMeshRenderer _skinnedRenderer;
    Material _mat;
    Material _skinMat;

    bool _animation = false;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
        if (_meshRenderer != null)
        {
            _mat = _meshRenderer.material;
        }
        if (_skinMat != null)
        {
            _skinMat = _skinnedRenderer.material;
        }
        StartRoutine();
    }

    public void StartRoutine()
    {
        if (!_animation)
        {
            StartCoroutine(Animate());
        }
        _animation = true;
    }

    IEnumerator Animate()
    {
        float _value = 0f;
        while (_value < 1f)
        {
            _value += Time.deltaTime;
            _value = Mathf.Clamp01(_value);

            _mat?.SetFloat("_value", _value);
            _skinMat?.SetFloat("_value", _value);

            yield return null;
        }
    }
}