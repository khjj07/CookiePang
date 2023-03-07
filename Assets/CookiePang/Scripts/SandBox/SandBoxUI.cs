using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public enum BlockType
{
    NONE,
    DEFAULT
}

public class SandBoxUI : Singleton<SandBoxUI>
{
    [SerializeField]
    private SandBoxGrid sandBoxGridPrefab;
    private SandBoxGrid[,] _sandBoxGrids;
    private int _column = 9;
    private int _row = 9;

    [SerializeField]
    private Block[] blockPrefabs;

    public SandBoxGrid current=null;

    [SerializeField]
    private float offset = 1;

    // Start is called before the first frame update
    void Start()
    {
        _sandBoxGrids=new SandBoxGrid[_row, _column];
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                var instance = Instantiate(sandBoxGridPrefab.gameObject);
                instance.transform.SetParent(transform, false);
                instance.transform.position = new Vector3((i+1)*offset, Screen.height - (j+1)*offset,0);
                _sandBoxGrids[i,j] = instance.GetComponent<SandBoxGrid>();
            }
        }
        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0) && current)
            .Subscribe(_ =>
            {
                if (current.target)
                {
                    Destroy(current.target.gameObject);
                    current.target = null;
                }
                current.targetType =(BlockType)((int)++current.targetType % (int)(BlockType.DEFAULT+1));
                if(current.targetType!=BlockType.NONE)
                {
                    var instance = Instantiate(blockPrefabs[(int)current.targetType-1]);
                    var blockPos =  Camera.main.ScreenToWorldPoint(current.transform.position);
                    blockPos.z = 0;
                    instance.transform.position = blockPos;
                   current.target = instance.GetComponent<Block>();
                }
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
