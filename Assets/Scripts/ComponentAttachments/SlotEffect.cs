using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotEffect : MonoBehaviour
{
    Slot parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(parent != null && parent.IsActive);
        }
    }
}
