using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationBlock : Block
{
    public TeleportationBlock destination;
    private bool _ballEntered = false;


    protected override void Update()
    {

    }
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Ball") && !_ballEntered)
//         {
//             if(destination)
//             {
//                 destination._ballEntered = true;
//                 other.transform.position = destination.transform.position;
//             }
//         }
//     }
// 
//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Ball") && _ballEntered)
//         {
//             _ballEntered = false;
//         }
//     }

    public override BlockData ToData()
    {
        var data = base.ToData();
        data.type = BlockType.TELEPORT;
        return data;
    }
}