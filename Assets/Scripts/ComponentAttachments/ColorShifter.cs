using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorShifter : MonoBehaviour
{
    SpriteRenderer renderer;
    MaterialPropertyBlock blk;
    public Color ColorA, ColorB;

    private void Awake()
    {
        blk = new MaterialPropertyBlock();
    }
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.GetPropertyBlock(blk);
    }

    // Update is called once per frame
    void Update()
    {
        if (blk == null)
            return;
        renderer.GetPropertyBlock(blk, 0);
        blk.SetTexture("_MainTex", renderer.sprite.texture);
        blk.SetColor("_ColorA", ColorA);
        blk.SetColor("_ColorB", ColorB);
        renderer.SetPropertyBlock(blk, 0);
    }
}
