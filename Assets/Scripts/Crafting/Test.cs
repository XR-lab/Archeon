using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider c)
    {
        print(c.gameObject.name + " in");
    }
    private void OnTriggerExit(Collider c)
    {
        print(c.gameObject.name + " out");
    }
}
