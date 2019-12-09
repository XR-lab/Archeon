using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField]
    private float _sparkPower = 5;
    [SerializeField]
    private GameObject _spark;

    private Rigidbody _RG; 

    private void Start()
    {
        _RG = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision _col)
    {
        if (!_col.gameObject.CompareTag("Stone") || !_col.gameObject.CompareTag("Flint"))
            return;

        if(_RG.velocity.magnitude > _sparkPower || _col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > _sparkPower)
        {
            //Instantiate(_spark, (this.transform.position + _col.transform.position) / 2, Quaternion.identity);
        }
    }
}
