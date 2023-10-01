using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationHook : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartWalk() { 
        anim.SetBool("Walk", true); 
    }
    public void StopWalk() { anim.SetBool("Walk", false); }
    public void Fight() { anim.SetTrigger("Fight"); }

    private void Update()
    {
        anim.SetInteger("RandomState", Random.Range(0, 10));
    }
}
