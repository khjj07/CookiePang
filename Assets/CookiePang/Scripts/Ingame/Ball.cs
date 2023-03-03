using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int damage=1;
    public bool isFloor=true;

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.collider.CompareTag("Floor"))
       {
            isFloor = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero; 
       }
    }

    public void Shoot(Vector3 shootForce)
    {
        GetComponent<Rigidbody>().AddForce(shootForce, ForceMode.Impulse);
        isFloor = false;
    }
}
