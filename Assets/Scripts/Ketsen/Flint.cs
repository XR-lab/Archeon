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
    public List<GameObject> _interactable = new List<GameObject>();

    private bool _cd;
    private float _cdTime = 20;

    private Rigidbody _RG;

    private void Start()
    {
        _RG = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_cd)
        {
            _cdTime--;
        }
        if(_cdTime <= 0)
        {
            _cd = false;
        }
    }

    private void OnCollisionEnter(Collision _col)
    {
        if (!_col.gameObject.CompareTag("Stone"))
            return;
        if (_RG.velocity.magnitude > _breakPower || _col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > _breakPower )
        {
            if(!_cd)
            {
                //Instantiate(_spark, (this.transform.position + _col.transform.position) / 2, Quaternion.identity);
                int _randomShard = Random.Range(0, _flintShard.Count);
                GameObject _splinter = _flintShard[_randomShard];
                _flintShard.Remove(_splinter);
                GameObject _g = Instantiate(_interactable[_randomShard], _splinter.transform.position, Quaternion.identity);
                _splinter.transform.SetParent(_g.transform);
                _cd = true;
                _cdTime = 20;
            }
        }
    }


}
