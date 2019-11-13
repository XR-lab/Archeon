using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pek : MonoBehaviour
{
    private List<GameObject> _attachedGO = new List<GameObject>();
    private List<FixedJoint> _joints = new List<FixedJoint>();

    private bool _heating = false;
    private bool _stick = false;
    private bool _hard = true;
    [SerializeField]
    private float _heatingTime = 0;
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
        else if (_heatingTime > 30 && _heatingTime < 70)
        {
            _hard = false;
            _sticked = false;
            UnStick();

            if(_attachedGO.Count != 0)
            {
                float p = (_heatingTime - 30) / 50;
                this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Rigidbody>().velocity * p;
            }
        }
        else if (_heatingTime > 70)
        {
            _hard = false;
        }
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (!_other.gameObject.CompareTag("Craftable"))
            return;

        
        if(!_hard)
        {
            _attachedGO.Add(_other.gameObject);
            Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>());
        }
    }

    private void OnCollisionExit(Collision _other)
    {
        if (_other.gameObject.CompareTag("Craftable"))
            return;

        _attachedGO.Remove(_other.gameObject);
        Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>(), false);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Fire"))
        {
            _heating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            _heating = false;
        }
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
            _heatingTime -= .2f;
        }
    }

    private void Stick()
    {
        foreach(GameObject _GO in _attachedGO)
        {
            FixedJoint _joint = this.gameObject.AddComponent<FixedJoint>();
            _joint.connectedBody = _GO.GetComponent<Rigidbody>();
            _joints.Add(_joint);
            _GO.transform.SetParent(this.transform);
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
        }
    }
}
