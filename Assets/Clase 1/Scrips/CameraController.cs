using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float horizontalOffset;
    [SerializeField] float verticalOffset;
    [SerializeField] float slerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward = Vector3.Slerp(transform.forward, target.transform.forward, Time.deltaTime * slerpSpeed);

        Vector3 nextPos = target.position + Vector3.up * horizontalOffset + transform.forward * horizontalOffset;
        transform.position = nextPos;
    }
}
