using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DetectionHandler(IDetectable detectedObject);

public class RadarScan : MonoBehaviour
{
    public event DetectionHandler OnDetection;
    [SerializeField]
    float _intervalBetweenScans = 0.25f;
    [SerializeField]
    float _scanSpeed = 10f;

    public Transform CurrentDetectable { get; private set; }
    void Start()
    {
        StartCoroutine(ScanForPlayer());
    }

    private void Update()
    {
        transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f) * _scanSpeed * Time.deltaTime;
    }

    IEnumerator ScanForPlayer()
    {
        CurrentDetectable = null;
        transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(_intervalBetweenScans);
        if (CurrentDetectable == null)
            OnDetection?.Invoke(null);
        StartCoroutine(ScanForPlayer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDetectable _detectable))
        {
            Debug.Log("Detected");
            CurrentDetectable = other.transform;
            OnDetection?.Invoke(_detectable);
        }
    }
}
