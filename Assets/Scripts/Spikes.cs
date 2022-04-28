using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
    }

    public void ChangeTriggerEnable()
    {
        boxCollider2D.enabled = !boxCollider2D.enabled;
    }

    public bool IsEnabled()
    {
        return boxCollider2D.enabled;
    }
}
