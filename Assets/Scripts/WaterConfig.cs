using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class WaterConfig : MonoBehaviour
{
    SpriteRenderer r;
    MaterialPropertyBlock blk;

    public Vector2 BackgroundSize;
    public Vector2 BackgroundOffset;

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
        r = GetComponent<SpriteRenderer>();
        blk = new MaterialPropertyBlock();
        r.GetPropertyBlock(blk);
    }

    // Update is called once per frame
    void Update()
    {
        r.GetPropertyBlock(blk, 0);
        blk.SetTexture("_MainTex", r.sprite.texture);
        blk.SetVector("_BGSize", BackgroundSize);
        blk.SetVector("_BGOffset", BackgroundOffset);
        blk.SetColor("_WaterColor", BaseColor);
        blk.SetColor("_RippleColor", RippleColor);
        blk.SetFloat("_WaterAlpha", Alpha);
        blk.SetFloat("_Speed", Speed);
        blk.SetFloat("_NoiseSpeed", NoiseSpeed);
        blk.SetFloat("_RippleIntensity", RippleIntensity);
        blk.SetFloat("_Disolve", DisolveStrengh);
        blk.SetFloat("_NormalStrength", NormalNoiseRatio);
        r.SetPropertyBlock(blk, 0);
    }
}
