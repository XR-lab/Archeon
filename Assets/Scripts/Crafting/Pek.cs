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
        }
        else if (_heatingTime > 70)
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

        if (_other.gameObject.layer != 10)
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

        if (_other.gameObject.layer != 10)
            return;

        _attachedGO.Remove(_other.gameObject.transform.parent.gameObject);
        Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>(), false);
    }

    private void Heating()
    {
        if (_heatingTime < 100)
        {
            //_heatingTime += .15f;
        }
    }

    private void Cooling()
    {
        if (_heatingTime > 0)
        {
            //_heatingTime -= .2f;
        }
    }

    private void Stick()
    {
        foreach(GameObject _GO in _attachedGO)
        {
            //FixedJoint _joint = this.gameObject.AddComponent<FixedJoint>();
            //_joint.connectedBody = _GO.GetComponent<Rigidbody>();
            //_joints.Add(_joint);
            _GO.GetComponent<Rigidbody>().isKinematic = true;
            _GO.transform.SetParent(this.transform);
        }
    }

    private void UnStick()
    {
        /*foreach (FixedJoint _joint in _joints)
        {
            Destroy(_joint);
        }*/
        foreach (GameObject _GO in _attachedGO)
        {
            _GO.transform.SetParent(null);
            _GO.GetComponent<Rigidbody>().isKinematic = false;
            /*if (_GO.gameObject.GetComponent<Interactable>().enabled == false)
            {
                _GO.gameObject.GetComponent<Interactable>().enabled = true;
            }*/
        }
    }
}
