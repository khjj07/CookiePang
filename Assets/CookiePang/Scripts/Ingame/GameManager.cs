using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BlockType
{
    DEFAULT,
    TELEPORT,
    BOMB,
    POWER,
    JELLY,
    POISON
}

public class GameManager : Singleton<GameManager>
{
    private bool isPlay = false;
    public float shootPower;
    public float minHeight = -15.0f;
    public int initialBallCount;
    public int ballCount;
    public int[] deadline = new int[3];
    //public float timeScaleUpDelay = 10.0f;

    public Transform screenCordinate;
    public Vector3[,] gridPosition;
    private float marginLeft = -30;
    private float marginTop = 110;
    public Block[,] blocks;
    public int _column = 9;
    public int _row = 9;

    public bool isTimeScaleUp = false;
    private float offset = 115;

    [SerializeField]
    public Ball ball;
    [SerializeField]
    private Block[] blockPrefabs;
    [SerializeField, Range(5.0f, 10.0f)]
    private float reflectDotLength;

    [SerializeField]
    public StarSlider starSlider;
    private DotLine _dotLine;
    private float _ballRadius;
    [SerializeField]
    private GameObject _dummyBall;

    [Header("추가")]
    public GameObject successPanel;
    public GameObject failPanel;
    [SerializeField]
    private Text ballCntTxt;
    public bool isClear = false;


    public List<Block> _breakableBlocks;
    private IEnumerator _timeScaleUpRoutine = null;

    private float _currentTimeScale=1.0f;


    public void TimeScaleUp()
    {
        if(!isTimeScaleUp)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 3.0f;
        }
    }
    public void PlayGame()
    {
        isPlay = true;
        Time.timeScale = _currentTimeScale;
    }
    public void PauseGame()
    {
        isPlay = false;
        _currentTimeScale=Time.timeScale;
        Time.timeScale = 0;
    }
    public void NextGame()
    {
        Time.timeScale = 1;
        StageManager.instance.currentIndex++;
        SceneFlowManager.ChangeScene("Stage");
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneFlowManager.ChangeScene("StageSelect");
        SoundManager.instance.PlaySound(0, "MainSound");

    }
    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneFlowManager.ChangeScene("Stage");
    }

/*    private IEnumerator TimeScaleUp()
    {
        yield return new WaitForSeconds(timeScaleUpDelay);
        Time.timeScale = 3.0f;
        Debug.Log("speedup");
        yield return null;
    }
*/
    public void Awake()
    {
        blocks = new Block[_row, _column];
        _breakableBlocks = new List<Block>();
        gridPosition = new Vector3[_row, _column];
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                gridPosition[i, j] = new Vector3(marginLeft + (j + 1) * offset - Screen.width / 2, -marginTop + Screen.height / 2 - (i + 1) * offset, 0);
            }
        }//블록생성
    }
    public void Start()
    {
        _dotLine = GetComponent<DotLine>();
        _ballRadius = ball.GetComponent<CircleCollider2D>().radius;
        _dummyBall.SetActive(false);

        this.UpdateAsObservable().Subscribe(_ =>
        {
            starSlider.SetFillArea(ballCount);
        });//달성률 표시


        this.UpdateAsObservable().
            Subscribe(_ =>
            {
                for (int i = 0; i < 3; i++)
                {
                    starSlider.SetStar(i, deadline[i]);
                }
            });//별 표시

        this.UpdateAsObservable()
            .Where(_ => isPlay)
             .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y > minHeight)
             .Where(_ =>
             {
                 var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 mousePos.z = 0;
                 var ballPos = ball.transform.position;
                 var direction = Vector3.Normalize(mousePos - ballPos);
                 return Input.GetMouseButton(0) && ball.isFloor && Physics2D.CircleCast(ballPos, _ballRadius, direction);
             }).Select(_ =>
             {
                 var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 mousePos.z = 0;
                 var ballPos = ball.transform.position;
                 var direction = Vector3.Normalize(mousePos - ballPos);
                 return Physics2D.CircleCast(ballPos, _ballRadius, direction);
             }).Subscribe(hit =>
             {
                 _dotLine.points.Clear();
                 _dotLine.points.Add(ball.transform.position);
                 _dotLine.points.Add(hit.centroid);
                 var direction = Vector3.Normalize(new Vector3(hit.centroid.x, hit.centroid.y, 0) - ball.transform.position);
                 var endPos = Vector3.Reflect(direction, hit.normal) * reflectDotLength;
                 _dotLine.points.Add(new Vector3(hit.centroid.x, hit.centroid.y, 0) + endPos);
                 
                 //투명하게 공표시
                 if(EventSystem.current.IsPointerOverGameObject() == false) //ui가 위에 있으면 도트라인&dummyBall 안나오게
                 {
                     _dotLine.DrawDotLine();
                     _dummyBall.SetActive(true);
                     _dummyBall.transform.position = hit.centroid;
                 }
                 else
                 {
                     _dummyBall.SetActive(false);
                 }
             });//조준점 생성

        var ballShootStream = this.UpdateAsObservable()
            .Where(_ => isPlay)
            .Where(_ => ballCount > 0)
            .Where(_ => Input.GetMouseButtonUp(0) && ball.isFloor) //마우스 업 && ball이 땅에 있다면
            .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y > minHeight)
            .Where(_ => EventSystem.current.IsPointerOverGameObject() == false) //ui위에 있으면 슈팅못하게
            .Select(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition)); //마우스 위치를 필터링

        ballShootStream.Subscribe(mousePos =>
            {
                mousePos.z = 0;
                var direction = Vector3.Normalize(mousePos - ball.transform.position);
                ball.Shoot(direction * shootPower);//Shoot
                _dummyBall.SetActive(false);
                ballCount--;
                //_timeScaleUpRoutine = TimeScaleUp();
                //StartCoroutine(_timeScaleUpRoutine);
            });//공 발사

        
        //this.UpdateAsObservable()
        //     .Where(_ => ball.isFloor)
        //     .Subscribe(_ =>
        //     {
        //         Time.timeScale = 1;
        //         if(_timeScaleUpRoutine != null)
        //         {
        //             //StopCoroutine(_timeScaleUpRoutine);
        //         }
        //     });

        this.UpdateAsObservable()
            .Where(_ => isPlay)
            .Where(_ => ball.isFloor)
            .Where(_ => isClear)
            .Subscribe(_ =>
            {
                StageClear();
            });//스테이지 클리어

        this.UpdateAsObservable()
            .Where(_ => isPlay)
            .Where(_ => ball.isFloor)
            .Where(_ => ballCount <= 0)
            .Subscribe(_ =>
            {
                GameOver();
            });//게임오버

    }

    public void StageClear()
    {
        successPanel.SetActive(true);
        PauseGame();
        SoundManager.instance.PlaySound(1, "StageClearSound");
    }

    public void GameOver()
    {
        failPanel.SetActive(true);
        PauseGame();
        SoundManager.instance.PlaySound(1, "StageFailSound");
    }
    public Block CreateBlock(BlockType x, int r, int c)
    {
        var instance = Instantiate(blockPrefabs[(int)x].gameObject);
        instance.transform.parent = screenCordinate;
        instance.transform.localPosition = gridPosition[r, c];
        blocks[r, c] = instance.GetComponent<Block>();
        if(x==BlockType.DEFAULT)
        {
            _breakableBlocks.Add(instance.GetComponent<Block>());
        }
        return instance.GetComponent<Block>();
    }

    public void DeleteBlock(Block x)
    {
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (blocks[i, j] == x)
                {
                    blocks[i, j] = null;
                    Destroy(x.gameObject);
                    _breakableBlocks.Remove(x);
                }
            }
        }
    }

    public void ClearBlocks()
    {
        foreach (var block in blocks)
        {
            if (block != null)
                Destroy(block.gameObject);
        }
        blocks = new Block[_row, _column];
        _breakableBlocks.Clear(); 
    }
   

    public Vector2Int GetBlockIndex(Block x)
    {
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (blocks[i, j] == x)
                {
                    return new Vector2Int(i, j);
                }
            }
        }
        return new Vector2Int(-1, -1); ;
    }

    public void Explode(Block x)
    {
        int explosionX = -10;
        int explosionY = -10;
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (blocks[i, j] == x)
                {
                    explosionX = j;
                    explosionY = i;
                    break;
                }
            }
        }

        DeleteBlock(x);

        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (explosionY >= 0 && explosionX >= 0 && explosionY < _row && explosionX < _column)
                {
                    if (i >= explosionY - 1 && i <= explosionY + 1 && j >= explosionX - 1 && j <= explosionX + 1)
                    {
                        if (blocks[i, j])
                        {
                            blocks[i, j].Hit(1);
                        }
                    }
                }

            }
        }
        
    }
    private void LateUpdate()
    {
        ballCntTxt.text = "남은 공 : " + ballCount;
    }
}
