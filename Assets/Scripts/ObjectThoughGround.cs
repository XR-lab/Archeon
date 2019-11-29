using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThoughGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider _col)
    {
        _col.transform.position = new Vector3(_col.transform.position.x, 1, _col.transform.position.z);
    }
}
