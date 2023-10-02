using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Navigator))]
public class FlipHandler : MonoBehaviour
{
    Navigator nav;

    public enum Heading {
        LEFT = 0,
        RIGHT = 1
    };
    public Heading InitialHeading;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<Navigator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((InitialHeading == 0) != (nav.Heading.x < 0))
        {
            if (transform.localScale.x < 0)
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1f, 1f, 1f));
        }
        else if (transform.localScale.x > 0)
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1f, 1f, 1f));
    }
}
