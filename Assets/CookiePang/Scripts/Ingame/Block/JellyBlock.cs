using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBlock : Block
{
    protected override void Update()
    {
      
    }
    public override void Hit(int damage)
    {
        
    }
    public override BlockData ToData(int row, int column)
    {
        JellyBlockData data = new JellyBlockData(hp, row, column);
        return data;
    }
}
