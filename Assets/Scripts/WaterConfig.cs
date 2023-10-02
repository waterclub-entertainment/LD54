using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class WaterConfig : MonoBehaviour
{
    Renderer r;
    MaterialPropertyBlock blk;

    public Color BaseColor;
    public Color RippleColor;
    [Range(0f, 1f)]
    public float Alpha;

    public float Speed;
    public float NoiseSpeed;

    public float RippleIntensity;
    public float DisolveStrengh;
    [Range(0f, 1f)]
    public float NormalNoiseRatio;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        blk = new MaterialPropertyBlock();
        r.GetPropertyBlock(blk);
    }

    // Update is called once per frame
    void Update()
    {
        r.GetPropertyBlock(blk, 0);
        blk.SetColor("_BaseColor", BaseColor);
        blk.SetColor("_RippleColor", RippleColor);
        blk.SetFloat("_Alpha", Alpha);
        blk.SetFloat("_Speed", Speed);
        blk.SetFloat("_NoiseSpeed", NoiseSpeed);
        blk.SetFloat("_RippleIntensity", RippleIntensity);
        blk.SetFloat("_Disolve", DisolveStrengh);
        blk.SetFloat("_NormalStrength", NormalNoiseRatio);
        r.SetPropertyBlock(blk, 0);
    }
}
