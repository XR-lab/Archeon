using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPek : MonoBehaviour
{
    [SerializeField]
    private GameObject _pek;

    private void OnTriggerEnter(Collider _other)
    {
        if (!_other.gameObject.CompareTag("PekPoint"))
            return;

        if(_other.transform.childCount <= 0)
        {
            GameObject _p = Instantiate(_pek, _other.transform.position, Quaternion.identity);
            _p.transform.SetParent(_other.transform);
        }
    }
}
