using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacaroonBlock : Block
{
    public override BlockData ToData(int row, int column)
    {
        MacaroonBlockData data = new MacaroonBlockData(hp,row, column);
        //data.position = transform.position;
        return data;
    }
}
