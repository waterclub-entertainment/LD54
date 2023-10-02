using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispSpawner : MonoBehaviour
{
    public Sprite wisp;
    public DeathWisp wispPrefab;

    void Spawn()
    {
        if (wisp != null)
        {
            var res = Instantiate(wispPrefab, transform.position, Quaternion.identity);
            res.Remderer.sprite = wisp;
        }
    }

    private void OnDestroy()
    {
        Invoke("Spawn", 0.1f);
    }
}
