using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenProxy : MonoBehaviour, IToken
{
    EntityToken refToken;

    // Start is called before the first frame update
    void Start()
    {
        refToken = transform.parent.GetComponentInChildren<EntityToken>();
    }

    public EntityToken GetToken()
    { return refToken; }
}
