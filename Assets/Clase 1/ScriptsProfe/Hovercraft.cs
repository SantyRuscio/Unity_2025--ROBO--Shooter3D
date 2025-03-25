using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovercraft : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Move(float h, float v)
    {
        transform.position += transform.forward * speed * v * Time.deltaTime;
        transform.Rotate(transform.up * h * rotSpeed * 360 * Time.deltaTime);
    }
}
