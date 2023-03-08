using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationBlock : Block
{
    public GameObject targetPos;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.transform.position = targetPos.transform.position;
            StartCoroutine(BoxOFF());
        }
    }

    IEnumerator BoxOFF()
    {
        targetPos.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        targetPos.GetComponent<BoxCollider>().enabled = true;
    }
}
