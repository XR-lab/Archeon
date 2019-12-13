using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Pek : MonoBehaviour {
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

    private void Update() {
        if (_heating) {
            Heating();
        } else {
            Cooling();
        }

        if (_heatingTime < 30) {
            _hard = true;
            if (!_sticked) {
                Stick();
                _sticked = true;
            }
        } else if (_heatingTime > 30 && _heatingTime < 100) {
            _hard = false;
            _sticked = false;
        } else if (_heatingTime > 100) {
            Destroy(this.gameObject);
            UnStick();
        }
    }

    private void OnTriggerEnter(Collider _other) {
        if (_other.gameObject.CompareTag("Fire")) {
            _heating = true;
        }

        if (_other.gameObject.layer != 11)
            return;

        if (!_hard) {
            _attachedGO.Add(_other.gameObject.transform.parent.gameObject);
            if (_other.gameObject.CompareTag("Craftable")) {
                Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>());
            }
        }
    }

    private void OnTriggerExit(Collider _other) {
        if (_other.gameObject.CompareTag("Fire")) {
            _heating = false;
        }

        if (_other.gameObject.layer != 11)
            return;

        _attachedGO.Remove(_other.gameObject.transform.parent.gameObject);
        Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>(), false);
    }

    private void Heating() {
        if (_heatingTime < 120) {
            _heatingTime += .15f;
        }
    }

    private void Cooling() {
        if (_heatingTime > 0) {
            _heatingTime -= .15f;
        }
    }

    private void Stick() {
        foreach (GameObject _GO in _attachedGO) {
            //FixedJoint _joint = this.gameObject.AddComponent<FixedJoint>();
            //_joint.connectedBody = _GO.GetComponent<Rigidbody>();
            //_joints.Add(_joint);
            _GO.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().isTrigger = true;
            //_GO.GetComponent<Hand>().Drop();

            //Destroy(_GO.GetComponent<Rigidbody>());
            _GO.GetComponent<Rigidbody>().isKinematic = true;
            _GO.transform.SetParent(this.transform);
            _GO.GetComponentInChildren<SpearPointCollision>()?.StickToPek(transform, GetComponentInParent<SpearPointCollision>());
            Destroy(_GO?.GetComponent<Throwable>());
            Destroy(_GO?.GetComponent<Valve.VR.InteractionSystem.Interactable>());
        }
    }

    private void UnStick() {
        /*foreach (FixedJoint _joint in _joints)
        {
            Destroy(_joint);
        }*/
        foreach (GameObject _GO in _attachedGO) {
            _attachedGO.Remove(_GO);
            Destroy(_GO);
            //_GO.GetComponent<Rigidbody>().isKinematic = false;
            //_GO.GetComponent<Rigidbody>().mass = 1;
            /*if (_GO.gameObject.GetComponent<Interactable>().enabled == false)
            {
                _GO.gameObject.GetComponent<Interactable>().enabled = true;
            }*/
        }
    }
}