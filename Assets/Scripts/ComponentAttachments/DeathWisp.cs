using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DeathWisp : MonoBehaviour
{
    public SpriteRenderer Remderer { get { return GetComponent<SpriteRenderer>(); } }

    public float visibility = 1.0f;

    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", visibility);
    }

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
