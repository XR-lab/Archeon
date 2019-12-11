using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Pek : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _attachedGO = new List<GameObject>();
    private List<FixedJoint> _joints = new List<FixedJoint>();

    private bool _heating = false;
    private bool _stick = false;
    [SerializeField]
    private bool _hard = true;
    [SerializeField]
    private float _heatingTime = 69;
    private bool _sticked = false;

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

        if (_heatingTime < 30)
        {
            _hard = true;
            if(!_sticked)
            {
                Stick();
                _sticked = true;
            } 
        }
        else if (_heatingTime > 30 && _heatingTime < 100)
        {
            _hard = false;
            _sticked = false;
            UnStick();
        }
        else if (_heatingTime > 100)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Fire"))
        {
            _heating = true;
        }

        if (_other.gameObject.layer != 11)
            return;

        if (!_hard)
        {
            _attachedGO.Add(_other.gameObject.transform.parent.gameObject);
            if(_other.gameObject.CompareTag("Craftable")) 
            {
                Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>());
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.CompareTag("Fire"))
        {
            _heating = false;
        }

        if (_other.gameObject.layer != 11)
            return;

        _attachedGO.Remove(_other.gameObject.transform.parent.gameObject);
        Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>(), false);
    }

    private void Heating()
    {
        if (_heatingTime < 120)
        {
            _heatingTime += .15f;
        }
    }

    private void Cooling()
    {
        if (_heatingTime > 0)
        {
            _heatingTime -= .15f;
        }
    }

    private void Stick()
    {
        foreach(GameObject _GO in _attachedGO)
        {
            _GO.transform.SetParent(this.transform);
        }
    }

    private void UnStick()
    {
        foreach (GameObject _GO in _attachedGO)
        {
            _GO.transform.SetParent(null);
        }
    }
}
