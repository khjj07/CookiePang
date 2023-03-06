using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class SandBoxUI : MonoBehaviour
{
    [SerializeField]
    private SandBoxGrid sandBoxGridPrefab;
    private SandBoxGrid[,] _sandBoxGrids;
    private int _column = 9;
    private int _row = 9;

    static public SandBoxGrid current=null;

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
        this.UpdateAsObservable().Where(_=>Input.GetMouseButtonDown(0) && current)
            .Subscribe(_=> { 
                current.target
                
            })
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
