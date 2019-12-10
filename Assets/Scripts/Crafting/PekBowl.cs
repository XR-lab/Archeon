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
    [SerializeField]
    private GameObject _gFluid;
    private Fluid _fluid;

    [SerializeField]
    private GameObject _pek;

    private void Start() { _fluid = _gFluid.GetComponent<Fluid>(); }

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

        ManageFluid();
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
            Physics.IgnoreCollision(this.gameObject.transform.parent.gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>());
            if (_other.transform.childCount <= 0 && !_other.gameObject.CompareTag("PekPoint"))
            {
                GameObject _p = Instantiate(_pek, _other.transform.position, Quaternion.identity);
                _p.transform.SetParent(_other.transform);
            }
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

        Physics.IgnoreCollision(this.gameObject.transform.parent.gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>(), false);
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

    private void ManageFluid()
    {
        _fluid._sloshSpeed = Mathf.RoundToInt(_heatingTime * 10 / 100);
        _fluid._rotateSpeed = Mathf.RoundToInt(_heatingTime * 60 / 100);
    }
}
