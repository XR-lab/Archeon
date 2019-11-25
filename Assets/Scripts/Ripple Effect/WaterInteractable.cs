using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractable : MonoBehaviour
{

    [SerializeField]
    private int _waterLayer;

    [SerializeField]
    private float _rippleVolume;

    [SerializeField]
    private float _timeDelay;

    [SerializeField]
    private int _rippleRadius;

    void Start()
    {
        _rippleRadius = _rippleRadius != 0 && _rippleRadius > 0 ? _rippleRadius : 50;

        _rippleVolume = _rippleVolume > 1 ? _rippleVolume : 1; 
        
    }

    //Trigger when water got touched
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == _waterLayer)
        {
            Debug.Log("water");

            RippleGenerator generator = other.gameObject.GetComponent<RippleGenerator>();
            Debug.LogError(new Vector2(
                other.contacts[other.contactCount - 1].point.x, other.contacts[other.contactCount - 1].point.z));
            StartCoroutine(generator.GenerateRipple(new Vector2(
                other.contacts[other.contactCount-1].point.x, other.contacts[other.contactCount-1].point.z), 
                _rippleVolume, _rippleRadius, _timeDelay));
        }
    }
}
