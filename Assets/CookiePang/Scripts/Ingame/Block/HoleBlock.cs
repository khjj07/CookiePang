using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleBlock : Block
{
    public bool holeIn = false;

    protected override void Update()
    {
        //_textMeshPro.SetText(hp.ToString());
    }
    public override void Hit(int damage)
    {
        if(!holeIn)
        {
            holeIn = true;
        }
    }
    public override void Shock(int damage)
    {

    }
    public override BlockData ToData(int row, int column)
    {
        HoleBlockData data = new HoleBlockData(row, column);
        //data.position = transform.position;
        return data;
    }
    public override void GetData(BlockData data)
    {
        HoleBlockData tel = data as HoleBlockData;
    }
}