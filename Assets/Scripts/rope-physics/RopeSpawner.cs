using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawner : MonoBehaviour {
    [Space]
    [Header("Do NOT rescale the rope after it has been created.")]
    [Header("After making a rope, turn it into a prefab to save it.")]
    [Header("Use this tool to create ropes.")]
    [SerializeField] private GameObject _partPrefab;
    [SerializeField] private GameObject _parentObject;

    [SerializeField] private int _ropeLength;

    [SerializeField] private float _separation = 0.21f;

    [SerializeField] private bool _snapFirst;

    private Transform[] _children;

    private Vector3[] _connectedAnchors;
    private Vector3[] _anchors;

    private int _count;

    private void Start() {
        SpawnRope();
    }

    public void SpawnRope() {
        _count = (int)(_ropeLength / _separation);
        for (int i = 0; i < _count; i++) {
            GameObject obj = Instantiate(_partPrefab, new Vector3(transform.position.x, transform.position.y + (_parentObject.transform.localScale.y * (_separation * (i + 1))), transform.position.z), Quaternion.identity, _parentObject.transform);
            obj.transform.eulerAngles = new Vector3(180, 0, 0);
            obj.name = _parentObject.transform.childCount.ToString();
        }
        StartCoroutine(AddJointsToRope());
    }

    IEnumerator AddJointsToRope() {
        yield return new WaitForEndOfFrame();
        GameObject obj;
        for (int i = 0; i < _count; i++) {
            obj = _parentObject.transform.Find((i+1).ToString()).gameObject;
            CharacterJoint joint = obj.AddComponent<CharacterJoint>();
            SetJointValues(joint);
            if (i == 0) {
                Destroy(joint);
            }
            else {
                joint.connectedBody = _parentObject.transform.Find(i.ToString()).GetComponent<Rigidbody>();
            }
        }
        if (_snapFirst) {
            _parentObject.transform.Find((_parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void SetJointValues(CharacterJoint joint) {

        joint.autoConfigureConnectedAnchor = true;

        // setting joint twist and swing limit springs

        SoftJointLimitSpring spring = new SoftJointLimitSpring();
        spring.spring = 1;
        spring.damper = 1;
        joint.twistLimitSpring = spring;
        joint.swingLimitSpring = spring;

        // setting joint twist and swing limits
        SoftJointLimit limit = new SoftJointLimit();
        limit.limit = -45;
        limit.bounciness = 0;
        limit.contactDistance = 5;
        joint.lowTwistLimit = limit;
        limit.limit = 45;
        joint.highTwistLimit = limit;
        limit.limit = 140;
        joint.swing1Limit = limit;
        joint.swing2Limit = limit;

        // enabling and setting projection
        joint.enableProjection = true;
        joint.projectionDistance = 0.1f;
        joint.projectionAngle = 180;

        joint.enableCollision = true;

        joint.massScale = 0.1f;
        joint.connectedMassScale = 0.1f;
    }
}