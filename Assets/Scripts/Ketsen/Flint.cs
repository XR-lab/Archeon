using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flint : MonoBehaviour
{
    [SerializeField]
    private float _breakPower;
    [SerializeField]
    private GameObject _spark;
    [SerializeField]
    public List<GameObject> _flintShard = new List<GameObject>();

    private Rigidbody _RG;

    private void Start()
    {
        _RG = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision _col)
    {
        if (!_col.gameObject.CompareTag("Stone"))
            return;
        //print(_RG.velocity.magnitude);
        if (_RG.velocity.magnitude > _breakPower || _col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > _breakPower)
        {
            print(_RG.velocity.magnitude);
            //Instantiate(_spark, (this.transform.position + _col.transform.position) / 2, Quaternion.identity);
            Instantiate(_flintShard[Random.Range(0, _flintShard.Count)], (this.transform.position + _col.transform.position), Quaternion.identity);
        }
    }

    private void UpdateFlintModel()
    {

    }
}
