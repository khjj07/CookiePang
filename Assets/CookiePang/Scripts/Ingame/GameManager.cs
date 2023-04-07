using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public enum BlockType
{
    DEFAULT,
    TELEPORT,
    BOMB,
    POWER,
    JELLY,
    POISON,
    HOLE,
    CANDY,
    BUTTON,
    MACAROON
}

public class GameManager : Singleton<GameManager>
{
    private bool isPlay = false;
    public float shootPower;
    public float minHeight = -15.0f;
    public int initialBallCount;
    public int ballCount;
    public int[] stars = new int[3];
    public float timeScaleUpDelay = 10.0f;

    public Transform screenCordinate;
    public Vector3[,] gridPosition;
    private float marginLeft = 90;
    private float marginTop = 360;
    public Block[,] blocks;
    public int _column = 9;
    public int _row = 9;

    public bool isTimeScaleUp = false;
    private float offset = 90;

    [SerializeField]
    public Ball ball;
    public Vector3 firstBallPos;
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

    [Header("�߰�")]
    public GameObject successPanel;
    public GameObject failPanel;
    [SerializeField]
    private Text ballCntTxt;
    [SerializeField]
    private Text ballPowerTxt;
    [SerializeField]
    private Image ballGaugeImage;
    [SerializeField]
    private Text currentStageTxt;
    public bool isClear = false;
    [SerializeField]
    private GameObject[] StarsImage;
    [SerializeField]
    private GameObject[] successPanelStarsImage;
    public int deadLineBallCount;
    public int deadLindMaxBallCount;
    public List<Block> _breakableBlocks;
    public List<HoleBlock> _holes;
    public List<CandyBlock> _candies;
    public List<ButtonBlock> _buttons;
    public List<MacaroonBlock> _macaroon;
    public string _letters;

    private float _currentTimeScale=1.0f;

    [SerializeField] private Slider[] slider; //Setting Panel volume //���Ŀ� title�� �ִ°ɷ� �����̴� �� ������ (�ӽ�)

    public void BallCollectButton()
    {
        ball.transform.position = firstBallPos;
        ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ball.isFloor = true;
    }
    public void TimeScaleUp()
    {
        if (!isTimeScaleUp)
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
        }//��ϻ���
    }
    public void Start()
    {
        _dotLine = GetComponent<DotLine>();
        _ballRadius = ball.GetComponent<CircleCollider2D>().radius;
        _dummyBall.SetActive(false);

        if (SoundManager.instance)
        {
            slider[0].value = SoundManager.instance.Player[0].Volume;
            slider[1].value = SoundManager.instance.Player[1].Volume;
        }
        firstBallPos = ball.transform.position; //ù��ġ ����
        
        this.UpdateAsObservable().Subscribe(_ =>
        {
            starSlider.SetFillArea(ballCount);
        });//�޼��� ǥ��


        this.UpdateAsObservable().
            Subscribe(_ =>
            {
                for (int i = 0; i < 3; i++)
                {
                    starSlider.SetStar(i, stars[i]);
                }
            });//�� ǥ��
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

                 //�����ϰ� ��ǥ��
                 if (EventSystem.current.IsPointerOverGameObject() == false) 
                 {
                     _dotLine.DrawDotLine();
                     _dummyBall.SetActive(true);
                     _dummyBall.transform.position = hit.centroid;
                 }
                 else
                 {
                     _dummyBall.SetActive(false);
                 }
             });//������ ����

        var ballShootStream = this.UpdateAsObservable()
            .Where(_ => isPlay)
            .Where(_ => ballCount > 0)
            .Where(_ => Input.GetMouseButtonUp(0) && ball.isFloor) //���콺 �� && ball�� ���� �ִٸ�
            .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y > minHeight)
            .Where(_ => EventSystem.current.IsPointerOverGameObject() == false) //ui���� ������ ���ø��ϰ�
            .Select(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition)); //���콺 ��ġ�� ���͸�

        ballShootStream.Subscribe(mousePos =>
            {
                mousePos.z = 0;
                var direction = Vector3.Normalize(mousePos - ball.transform.position);
                ball.Shoot(direction * shootPower);//Shoot
                _dummyBall.SetActive(false);
                ballCount--;
                //_timeScaleUpRoutine = TimeScaleUp();
                //StartCoroutine(_timeScaleUpRoutine);
            });//�� �߻�

        
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
            //.Where(_ => ball.isFloor)
            .Where(_ => isClear)
            .Subscribe(_ =>
            {
                StageClear();
            });//�������� Ŭ����

        this.UpdateAsObservable()
            .Where(_ => isPlay)
            .Where(_ => ball.isFloor)
            .Where(_ => ballCount <= 0)
            .Subscribe(_ =>
            {
                GameOver();
            });//���ӿ���
        //this.UpdateAsObservable()
        //    .Where(_ => isPlay)
        //    .Subscribe(_ => {
        
        //    });
    }

    public void StageClear()
    {
        ball.transform.position = firstBallPos; //���� ��ġ��
        successPanel.SetActive(true);
        foreach (GameObject clearStars in successPanelStarsImage)
        {
            clearStars.transform.DOShakeScale(0.3f, 3).SetUpdate(true);
            clearStars.transform.DOShakePosition(0.3f, 3).SetUpdate(true);
        }
        PauseGame();
        SoundManager.instance.PlaySound(1, "StageClearSound");
    }

    public void GameOver()
    {
        deadLineBallCount = 0;
        failPanel.SetActive(true);
        PauseGame();
        SoundManager.instance.PlaySound(1, "StageFailSound");
    }

    public Block CreateBlock(BlockType x, int r, int c)
    {
        var instance = Instantiate(blockPrefabs[(int)x]);
        instance.transform.parent = screenCordinate;
        instance.transform.localPosition = gridPosition[r, c];
        blocks[r, c] = instance;
        if(x==BlockType.DEFAULT)
        {
            _breakableBlocks.Add(instance);
        }
        else if(x==BlockType.HOLE)
        {
            _holes.Add(instance as HoleBlock);
        }
        else if (x == BlockType.CANDY)
        {
            _candies.Add(instance as CandyBlock);
        }
        else if (x == BlockType.BUTTON)
        {
            _buttons.Add(instance as ButtonBlock);
        }
        else if (x == BlockType.MACAROON)
        {
            _macaroon.Add(instance as MacaroonBlock);
        }
        return instance;
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
                    _holes.Remove(x as HoleBlock);
                    _candies.Remove(x as CandyBlock);
                    _buttons.Remove(x as ButtonBlock);
                    _macaroon.Remove(x as MacaroonBlock);
                    
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
        _holes.Clear();
        _candies.Clear();
        _buttons.Clear();
        _macaroon.Clear();
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

    public async Task Explode(Block x)
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

        await Task.Delay(100);

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
                           blocks[i, j].Shock(1);
                        }
                    }
                }

            }
        }
        
    }
    private void LateUpdate()
    {
        ballPowerTxt.text = ball.damage.ToString();
        ballGaugeImage.fillAmount = Mathf.Lerp(ballGaugeImage.fillAmount, (float)deadLineBallCount / deadLindMaxBallCount / 1 / 1, Time.deltaTime * 5);
        DeadLineCount();
        ballCntTxt.text = deadLineBallCount.ToString(); //�� �����ε� ������ ������ �ٲ������
        if (StageManager.instance)
        {
            currentStageTxt.text = StageManager.instance.currentIndex.ToString();
        }
        
    }
    public void StarsCountImage()
    {

        if (ballCount > stars[2])
        {
            return;
        }
        else if (ballCount > stars[1])
        {
            successPanelStarsImage[2].SetActive(false);
            StarsImage[2].transform.DOShakeScale(0.3f, 3);
            StarsImage[2].transform.DOShakePosition(0.3f, 3).OnComplete(() =>
            {
                StarsImage[2].SetActive(false);
            });
        }
        else if (ballCount > stars[0])
        {
            successPanelStarsImage[1].SetActive(false);
            StarsImage[1].transform.DOShakeScale(0.3f, 3);
            StarsImage[1].transform.DOShakePosition(0.3f, 3).OnComplete(() =>
            {
                StarsImage[1].SetActive(false); 
            });
        }
        else
        {
            StarsImage[0].SetActive(false);
        }

    }
    public void DeadLineCount()
    {
        if(ballCount > stars[0] && ballCount > stars[1] && ballCount > stars[2])
        {
            deadLineBallCount = ballCount - stars[2];
            deadLindMaxBallCount = initialBallCount - stars[2];
        }
        else if (ballCount > stars[1] && ballCount > stars[0])
        {
            deadLineBallCount = ballCount - stars[1];
        }
        else if (ballCount > stars[0])
        {
            deadLineBallCount = ballCount - stars[0];
        }
        else
        {
            return;
        }
    }

}
