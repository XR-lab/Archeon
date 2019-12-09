using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractable : MonoBehaviour
{

    [SerializeField]
    private LayerMask _waterLayer;

    [SerializeField]
    private float _rippleVolume;

    [SerializeField]
    private float _timeDelay;

    [SerializeField]
    private int _rippleRadius;

    private Vector3 origin = new Vector3();
    private Vector3 direction = new Vector3();

    

    void Start()
    {
        _rippleRadius = _rippleRadius != 0 && _rippleRadius > 0 ? _rippleRadius : 50;

        _rippleVolume = _rippleVolume > 1 ? _rippleVolume : 1; 
        
    }

    private void Update()
    {
        Debug.DrawLine(origin, origin+direction, Color.red);
    }

    //Trigger when water got touched
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == ~_waterLayer.value)
        {
            Debug.Log("water");
            int layer = 1 << _waterLayer;

            RippleGenerator generator = other.gameObject.GetComponent<RippleGenerator>();
            Vector2 pos = new Vector2();
            Ray ray = new Ray(other.contacts[other.contactCount - 1].point + Vector3.up, Vector3.down);
            RaycastHit hit;
            origin = ray.origin;
            direction = ray.direction*5;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.LogError(hit.collider.name);
                Debug.LogError(hit.collider.transform.gameObject.layer + " == " + ~_waterLayer);
                if (hit.collider.gameObject.layer == ~_waterLayer)
                {
                    pos = new Vector2(hit.textureCoord.x,hit.textureCoord.y);
                    Debug.LogError("hit");
                    Debug.LogError(pos);
                }
            }

            //StartCoroutine(generator.GenerateRipple(new Vector2(
            //    pos.x, 
            //    pos.y), 
            //    _rippleVolume, _rippleRadius, _timeDelay));
        }
    }
}
