using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : MonoBehaviour
{
    public GameObject _liquid;
    public GameObject _liquidMesh;

    public int _sloshSpeed = 10;
    public int _rotateSpeed = 60;
    [SerializeField]
    private int _difference = 25;

    private void Update()
    {
        Slosh();

        _liquidMesh.transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime, Space.Self);
    }

    private void Slosh()
    {
        Quaternion _inverseRotation = Quaternion.Inverse(transform.localRotation);

        Vector3 _finalRotation = Quaternion.RotateTowards(_liquid.transform.localRotation, _inverseRotation, _sloshSpeed * Time.deltaTime).eulerAngles;

        _finalRotation.x = ClampRotationValue(_finalRotation.x, _difference);
        _finalRotation.z = ClampRotationValue(_finalRotation.z, _difference);

        _liquid.transform.localEulerAngles = _finalRotation;
    }

    private float ClampRotationValue(float _value, float difference)
    {
        float _returnValue = 0.0f;

        if(_value > 180)
        {
            _returnValue = Mathf.Clamp(_value, 360 - difference, 360);
        }
        else
        {
            _returnValue = Mathf.Clamp(_value, 0, difference);
        }

        return _returnValue;
    }
}
