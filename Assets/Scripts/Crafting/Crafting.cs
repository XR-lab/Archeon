using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField]
    private GameObject _speer;

    private bool _hardend = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(!_hardend)
        {

        }
    }

    private void OnCollision(Collision other)
    {
        if(!_hardend && other.gameObject.layer == 10)
        {
            other.gameObject.transform.SetParent(_speer.transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!_hardend && other.gameObject.layer == 10)
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
