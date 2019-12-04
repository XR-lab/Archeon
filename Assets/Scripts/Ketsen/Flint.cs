using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using Valve.VR.InteractionSystem;
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834

public class Flint : MonoBehaviour
{
    [SerializeField]
    private float _breakPower;
    [SerializeField]
    private GameObject _spark;
    [SerializeField]
    public List<GameObject> _flintShard = new List<GameObject>();
<<<<<<< HEAD
=======
    public GameObject _interactable;

    private bool _cd;
    private float _cdTime = 20;
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834

    private Rigidbody _RG;

    private void Start()
    {
        _RG = GetComponent<Rigidbody>();
    }

<<<<<<< HEAD
=======
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

>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
    private void OnCollisionEnter(Collision _col)
    {
        if (!_col.gameObject.CompareTag("Stone"))
            return;
<<<<<<< HEAD
        //print(_RG.velocity.magnitude);
        if (_RG.velocity.magnitude > _breakPower || _col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > _breakPower)
        {
            print(_RG.velocity.magnitude);
            //Instantiate(_spark, (this.transform.position + _col.transform.position) / 2, Quaternion.identity);
            Instantiate(_flintShard[Random.Range(0, _flintShard.Count)], (this.transform.position + _col.transform.position)/2, Quaternion.identity);
        }
    }

    private void UpdateFlintModel()
    {

    }
=======
        if (_RG.velocity.magnitude > _breakPower || _col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > _breakPower )
        {
            if(!_cd)
            {
                //Instantiate(_spark, (this.transform.position + _col.transform.position) / 2, Quaternion.identity);
                GameObject _splinter = _flintShard[Random.Range(0, _flintShard.Count)];
                _flintShard.Remove(_splinter);
                GameObject _g = Instantiate(_interactable, _splinter.transform.position, Quaternion.identity);
                _splinter.transform.SetParent(_g.transform);
                _cd = true;
                _cdTime = 20;
            }
        }
    }


>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
}
