using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PekBowl : MonoBehaviour
{
    private bool _heating = false;
    [SerializeField]
    private bool _hard = true;
    [SerializeField]
    private float _heatingTime = 0;

    private void Update()
    {
        if (_heating)
        {
            Heating();
        }
        else
        {
            Cooling();
        }

        if (_heatingTime < 50)
        {
            _hard = true;
        }
        else
        {
            _hard = false;
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Fire"))
        {
            _heating = true;
        }

        if (_other.gameObject.layer != 10)
            return;

        if (!_hard)
        {
            Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>());
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.CompareTag("Fire"))
        {
            _heating = false;
        }

        if (_other.gameObject.layer != 10)
            return;

        Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>(), false);
    }

    private void Heating()
    {
        if (_heatingTime < 100)
        {
            _heatingTime += .2f;
        }
    }

    private void Cooling()
    {
        if (_heatingTime > 0)
        {
            _heatingTime -= .15f;
        }
    }
}
