using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class WaterConfig : MonoBehaviour
{
    SpriteRenderer r;
    MaterialPropertyBlock blk;

    public Sprite Mask;

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
    private void Awake()
    {
        blk = new MaterialPropertyBlock();
    }
    void Start()
    {
        r = GetComponent<SpriteRenderer>();
        r.GetPropertyBlock(blk);
    }

    // Update is called once per frame
    void Update()
    {
        if (blk == null)
            return;
        r.GetPropertyBlock(blk, 0);
        blk.SetTexture("_MainTex", r.sprite.texture);
        if (Mask == null)
            blk.SetTexture("_Mask", r.sprite.texture);
        else
            blk.SetTexture("_Mask", Mask.texture);
        blk.SetInt("_HasMask", Mask == null ? 0 : 1);
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
