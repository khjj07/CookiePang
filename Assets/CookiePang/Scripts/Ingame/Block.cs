using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Range(1, 1000)]
    public int hp;

    private TextMeshPro _textMeshPro;

    protected void Start()
    {
        _textMeshPro=GetComponentInChildren<TextMeshPro>();
    }

    protected void Update()
    {
        _textMeshPro.SetText(hp.ToString());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        hp -= collision.collider.GetComponent<Ball>().damage;
        if(hp<=0)
        {
            Destroy(gameObject);
        }
    }
}
