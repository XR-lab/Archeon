using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PekTest : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _attachedGO = new List<GameObject>();
    private List<FixedJoint> _joints = new List<FixedJoint>();

    private bool _heating = false;
    private bool _hard = true;
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

        if (_heatingTime < 30)
        {
            _hard = true;
        }
        else if (_heatingTime > 30 && _heatingTime < 100)
        {
            _hard = false;
        }
        else if (_heatingTime > 100)
        {
            _hard = false;
            UnStick();
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
            if (_other.gameObject.CompareTag("Craftable") || _other.gameObject.CompareTag("Handles"))
            {
                Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>());
            }
            _attachedGO.Add(_other.gameObject.transform.parent.gameObject);
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

        _attachedGO.Remove(_other.gameObject.transform.parent.gameObject);
        Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>(), false);
    }

    private void Heating()
    {
        if (_heatingTime < 150)
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

    private void UnStick()
    {
        foreach (FixedJoint _joint in _joints)
        {
            Destroy(_joint);
        }
        foreach (GameObject _GO in _attachedGO)
        {
            _GO.transform.SetParent(null);
            /*if (_GO.gameObject.GetComponent<Interactable>().enabled == false)
            {
                _GO.gameObject.GetComponent<Interactable>().enabled = true;
            }*/
        }
    }
    public void Stick(GameObject _g)
    {
        if(_attachedGO.Contains(_g))
        {
            FixedJoint _joint = this.gameObject.AddComponent<FixedJoint>();
            _joint.connectedBody = _g.GetComponent<Rigidbody>();
            _joints.Add(_joint);
            _g.transform.SetParent(this.transform);
        }
    }
}

