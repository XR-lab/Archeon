using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Flint : MonoBehaviour
{
    [SerializeField]
    private float _breakPower;
    [SerializeField]
    private GameObject _spark;
    [SerializeField]
    public List<GameObject> _flintShard = new List<GameObject>();
    public GameObject _interactable;

    private Rigidbody _RG;

    private void Start()
    {
        _RG = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision _col)
    {
        if (!_col.gameObject.CompareTag("Stone"))
            return;
        if (_RG.velocity.magnitude > _breakPower || _col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > _breakPower)
        {
            //Instantiate(_spark, (this.transform.position + _col.transform.position) / 2, Quaternion.identity);
            GameObject _splinter = _flintShard[Random.Range(0, _flintShard.Count)];
            _flintShard.Remove(_splinter);
            GameObject _g = Instantiate(_interactable, _splinter.transform.position, Quaternion.identity);
            _splinter.transform.SetParent(_g.transform);
        }
    }
}
