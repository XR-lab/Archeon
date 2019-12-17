using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGhostRange : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _onRange = 1f;
    private bool _bool = true;

    private void Update() 
    {
        float _dist = Vector3.Distance(this.transform.position, _player.transform.position);
        if(_dist < _onRange) 
        {
            this.GetComponent<AnimateShader>().StartRoutineDown();
        }
        else 
        {
            this.GetComponent<AnimateShader>().StartRoutineUp();
        }
    }
}
