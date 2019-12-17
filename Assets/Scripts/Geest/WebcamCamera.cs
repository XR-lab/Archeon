using UnityEngine;

public class WebcamCamera : MonoBehaviour
{
    private bool _camAvailable;
    private WebCamTexture _webcamTexture;
    [SerializeField]
    private GameObject _target;
    

    private void Start()
    {
        WebCamDevice[] _devices = WebCamTexture.devices;

        

        if(_devices.Length == 0)
        {
            Debug.Log("No camera detected");
            _camAvailable = false;
        }

        for(int i = 0; i < _devices.Length; i++)
        {
            if (i == 1)
                return;
            _webcamTexture = new WebCamTexture(_devices[i].name);
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.SetTexture("_mainTexture",_webcamTexture);
            _webcamTexture.Play();
        }
    }

    private void Update()
    {
        Vector3 difference = _target.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90f, 0.0f, rotationZ + 90);
    } 
}
