using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    Vector2 StartPos;
    [SerializeField] private float _parallaxStrength = 100f;
    [SerializeField] private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, StartPos.x + (pz.x * _parallaxStrength), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, StartPos.y + (pz.y * _parallaxStrength), 2f * Time.deltaTime);

        transform.position = new Vector3(posX, posY, 0);
    }
}
