using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacaroonBlock : Block
{
    public virtual void Hit(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            SoundManager.instance.PlaySound(1, "MacaroonBlockSound");
            GameManager.instance.DeleteBlock(this);
        }
    }
    public override BlockData ToData(int row, int column)
    {
        MacaroonBlockData data = new MacaroonBlockData(hp,row, column);
        //data.position = transform.position;
        return data;
    }
}
