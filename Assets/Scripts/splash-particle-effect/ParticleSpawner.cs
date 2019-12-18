using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _particleEmitter;
    [SerializeField] private float _particleMaxDuration = 1f;
    [SerializeField] private LayerMask _detectedLayers;

    private List<GameObject> _particlePool;
    private ParticleSystem[] _particleSystems;

    void Start() {
        _particlePool = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other) {
        if (_detectedLayers == (_detectedLayers | (1 << other.gameObject.layer))) {
            Vector3 otherPosition = other.ClosestPoint(other.transform.position);
            SpawnSplashParticles(new Vector3(otherPosition.x, transform.position.y, otherPosition.z));
        }
    }

    public void SpawnSplashParticles(Vector3 position) {
        int c = _particlePool.Count;
        GameObject obj = null;
        if (c > 0) {
            for (int i = 0; i < c; i++) {
                if (!_particlePool[i].activeSelf) {
                    obj = _particlePool[i];
                    break;
                }
            }
        }
        if (obj == null) {
            obj = Instantiate(_particleEmitter, position, Quaternion.identity);
            _particlePool.Add(obj);
            _particleSystems = obj.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem p in _particleSystems) {
                p.Play();
            }
            GetComponentInChildren<Animator>()?.StartPlayback();
            StartCoroutine(DisableAfter(obj, _particleMaxDuration));
        }
    }

    public IEnumerator DisableAfter(GameObject obj, float delay) {
        yield return new WaitForSeconds(delay);
        GetComponentInChildren<Animator>()?.StopPlayback();
        obj.SetActive(false);
    }
}