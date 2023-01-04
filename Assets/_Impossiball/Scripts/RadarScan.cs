using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DetectionHandler(IDetectable detectedObject, bool detected);

public class RadarScan : MonoBehaviour
{
    public event DetectionHandler OnDetection;
    [SerializeField]
    float _intervalBetweenScans = 0.25f;
    void Start()
    {
        StartCoroutine(ScanForPlayer());
    }

    private void Update()
    {
        transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
    }

    IEnumerator ScanForPlayer()
    {
        transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(_intervalBetweenScans);
        StartCoroutine(ScanForPlayer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDetectable _detectable))
        {
            OnDetection?.Invoke(_detectable, true);
        }
    }
}