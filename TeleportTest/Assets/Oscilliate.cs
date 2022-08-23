using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Oscilliate : MonoBehaviour
{
    Rigidbody body;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public float coeff = 0.1f;
    private void FixedUpdate()
    {
        body.AddForce(Vector3.left * coeff * transform.position.x, ForceMode.Acceleration);
    }
}
