using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBlock : Block
{
    //재구현 필요
    public int damage = 99;
    Vector3 currentSize;
    
    void Awake()
    {
        currentSize = this.gameObject.GetComponent<BoxCollider>().size;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            StartCoroutine(Bomb());
        }
    }

    IEnumerator Bomb()
    {
        this.gameObject.GetComponent<BoxCollider>().size = new Vector3(5, 5, 1);
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<BoxCollider>().size = currentSize;
        GameManager.instance.DeleteBlock(this);
    }
}
