using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleFileBrowser;

using static UnityEditorInternal.ReorderableList;
using System.Linq;

enum SandBoxMode
{
    EDIT,
    TEST
}

public class SandBoxUI : Singleton<SandBoxUI>
{
    public SandBoxGrid current = null;

    [SerializeField]
    private SandBoxGrid sandBoxGridPrefab;
    [SerializeField]
    private BlockType currentBlockType = BlockType.DEFAULT;

    private SandBoxGrid[,] _sandBoxGrids;

    private int _column = 9;
    private int _row = 9;
    [Header("UI")]
    #region UI
    public int blockDefaultHP;

    public TMP_Dropdown dropdown;
    public TextMeshProUGUI gameMode;
    public Slider blockDefaultHPSlider;
    public Slider ballDeadline1Slider;
    public Slider ballDeadline2Slider;
    public Slider ballDeadline3Slider;
    public Slider initialBallSlider;

    public TextMeshProUGUI blockDefaultHPNumber;
    public TextMeshProUGUI ballDeadLine1Number;
    public TextMeshProUGUI ballDeadLine2Number;
    public TextMeshProUGUI ballDeadLine3Number;
    public TextMeshProUGUI initialBallNumber;

    public VerticalLayoutGroup[] scoreModeGroup;
    #endregion

    [Header("Output Data")]
    #region OutputData
    public int initialBall;
    public int[] ballDeadLine = new int[3];
    #endregion

    [Header("현재 스테이지 에셋")]
    #region OutputData
    [SerializeField]
    private StageAsset currentStageAsset;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        _row = GameManager.instance._row;
        _column = GameManager.instance._column;
        _sandBoxGrids = new SandBoxGrid[_row, _column];
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                var instance = Instantiate(sandBoxGridPrefab.gameObject);
                instance.transform.SetParent(transform, false);
                instance.GetComponent<SandBoxGrid>().row = i;
                instance.GetComponent<SandBoxGrid>().column = j;
                instance.GetComponent<RectTransform>().localPosition = GameManager.instance.gridPosition[i, j];
                _sandBoxGrids[i, j] = instance.GetComponent<SandBoxGrid>();
            }
        }

        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0) && current)
            .Subscribe(_ =>
            {
                if (current.target)
                {
                    GameManager.instance.DeleteBlock(current.target);
                    current.target = null;
                }
                else
                {
                    var block = GameManager.instance.CreateBlock(currentBlockType, current.row,current.column);
                    block.hp = blockDefaultHP;
                    current.target = block;
                }
            });
        this.UpdateAsObservable().Where(_ => Input.GetMouseButton(1) && Input.GetKeyDown(KeyCode.Alpha1) && current)
            .Subscribe(_ =>
            {
                if (current.target)
                {
                   current.target.hp++;
                }
            });
        this.UpdateAsObservable().Where(_ => Input.GetMouseButton(1) && Input.GetKeyDown(KeyCode.Alpha2) && current)
           .Subscribe(_ =>
           {
               if (current.target)
               {
                   if(current.target.hp>0)
                   {
                        current.target.hp--;
                   }
               }
           });
    }
    public void Update()
    {
        currentBlockType = (BlockType)dropdown.value;
        foreach (var group in scoreModeGroup)
        {
            group.gameObject.SetActive(false);
        }
        if (currentStageAsset)
        {
            gameMode.SetText(currentStageAsset.gameMode.ToString());
            if (scoreModeGroup.Length > (int)(currentStageAsset.scoreMode))
            {
                scoreModeGroup[(int)currentStageAsset.scoreMode].gameObject.SetActive(true);
            }
        }
        SetInitialBall((int)initialBallSlider.value);
        SetBlockDefaultHP((int)blockDefaultHPSlider.value);
        SetballDeadLine1((int)ballDeadline1Slider.value);
        SetballDeadLine2((int)ballDeadline2Slider.value);
        SetballDeadLine3((int)ballDeadline3Slider.value);
    }
    private List<BlockData> SaveBlockToData()
    {
        List<BlockData> data = new List<BlockData>();
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if(_sandBoxGrids[i, j].target)
                {
                    var block = _sandBoxGrids[i, j].target.ToData();
                    block.row = i;
                    block.col = j;
                    data.Add(block);
                }
            }
        }
        return data;
    }

    public void SaveStage()
    {
        if(currentStageAsset)
        {
            List<BlockData> blockData = SaveBlockToData();
            currentStageAsset.blocks = blockData;
            switch (currentStageAsset.gameMode)
            {
                case GameMode.Default :
                    DefaultStageAsset asset =  (DefaultStageAsset)currentStageAsset;
                    asset.ballDeadLine[0] = ballDeadLine[0];
                    asset.ballDeadLine[1] = ballDeadLine[1];
                    asset.ballDeadLine[2] = ballDeadLine[2];
                    asset.initailBallCount = initialBall;
                    break;
            }
        }
    }

    public void LoadStage()
    {
        if (currentStageAsset)
        {
          
            GameManager.instance.ClearBlocks();
            foreach (var block in currentStageAsset.blocks)
            {
               var instance= GameManager.instance.CreateBlock(block.type,block.row,block.col);           
                instance.hp = block.hp;
                _sandBoxGrids[block.row, block.col].target = instance;
            }

           
           initialBall = currentStageAsset.initailBallCount;
           initialBallSlider.value = initialBall;

            switch (currentStageAsset.gameMode)
            {
                case GameMode.Default:
                    DefaultStageAsset asset = (DefaultStageAsset)currentStageAsset;
                    ballDeadLine[0]= asset.ballDeadLine[0];
                    ballDeadLine[1]= asset.ballDeadLine[1];
                    ballDeadLine[2]= asset.ballDeadLine[2];
                    ballDeadline1Slider.value = ballDeadLine[0];
                    ballDeadline2Slider.value = ballDeadLine[1];
                    ballDeadline3Slider.value = ballDeadLine[2];
                    break;
            }

        }
    }


    public void SetInitialBall(int value)
    {
        initialBall = value;
        initialBallNumber.SetText(initialBall.ToString());
    }

    public void SetBlockDefaultHP(int value)
    {
        blockDefaultHP = value;
        blockDefaultHPNumber.SetText(blockDefaultHP.ToString());
    }
    public void SetballDeadLine1(int value)
    {
        ballDeadLine[0] = value;
        ballDeadLine1Number.SetText(ballDeadLine[0].ToString());
    }
    public void SetballDeadLine2(int value)
    {
        ballDeadLine[1] = value;
        ballDeadLine2Number.SetText(ballDeadLine[1].ToString());
    }
    public void SetballDeadLine3(int value)
    {
        ballDeadLine[2] = value;
        ballDeadLine3Number.SetText(ballDeadLine[2].ToString());
    }
}
