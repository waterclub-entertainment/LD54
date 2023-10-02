using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorShifter : MonoBehaviour
{
    SpriteRenderer r;
    MaterialPropertyBlock blk;
    public Color ColorA, ColorB;

    private void Awake()
    {
        blk = new MaterialPropertyBlock();
    }
    // Start is called before the first frame update
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
        blk.SetColor("_ColorA", ColorA);
        blk.SetColor("_ColorB", ColorB);
        r.SetPropertyBlock(blk, 0);
    }
}
