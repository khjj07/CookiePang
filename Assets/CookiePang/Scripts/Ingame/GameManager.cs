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
using Unity.VisualScripting;
using TMPro;

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
    MACAROON,
    DEFAULTTRIANGLE
}

public class GameManager : Singleton<GameManager>
{
    private bool isPlay = false;
    public float shootPower;
    public float minHeight = 1.0f;
    public int initialBallCount;
    public int ballCount;
    public int[] stars = new int[3];
    public int starCount = 0;
    public float timeScaleUpDelay = 10.0f;

    public Transform screenCordinate;
    public Vector3[,] gridPosition;
    private float marginLeft = 40;
    private float marginTop = 400;
    public Block[,] blocks;
    public int _column = 9;
    public int _row = 9;

    public bool isTimeScaleUp = false;
    private float offset = 100;

    [SerializeField]
    public Ball ball;
    public Vector3 lastBallPos;
    [SerializeField]
    private Block[] blockPrefabs;
    [SerializeField, Range(5.0f, 10.0f)]
    public float reflectDotLength;

    [SerializeField]
    public StarSlider starSlider;
    private DotLine _dotLine;
    public float _ballRadius;
    [SerializeField]
    public GameObject _dummyBall;

    [Header("추가")]
    public GameObject ballCollectButton;
    [SerializeField]
    private float ballCollectActiveTime = 5;
    private float resetballCollectActiveTime = 5;
    [SerializeField]
    private bool isBallCollect = false;
    public GameObject successPanel;
    public GameObject failPanel;
    public Text ballCntTxt;
    [SerializeField]
    private Text ballPowerTxt;
    [SerializeField]
    private Image ballGaugeImage;
    public Sprite[] onOffParticleImage;
    public GameObject onOffParticleButton;
    [SerializeField]
    private Text currentStageTxt;
    public bool isClear = false;
    public bool isOver = false;
    [SerializeField]
    private GameObject[] StarsImage;
    [SerializeField]
    private GameObject[] successPanelStarsImage;
    public GameObject heartFailImage;
    [SerializeField]
    private TextMeshProUGUI heartText;
    public int deadLineBallCount;
    public int deadLineMaxBallCount;
    public FailAddMob FailaddMob;

    public List<Block> _breakableBlocks;
    public List<HoleBlock> _holes;
    public List<CandyBlock> _candies;
    public List<ButtonBlock> _buttons;
    public List<MacaroonBlock> _macaroon;
    public string _letters;
    public string goal;

    private float _currentTimeScale = 1.0f;

    [SerializeField] private Slider[] slider; //Setting Panel volume //추후에 title에 있는걸로 슬라이더 다 쓸거임 (임시)
    
    public void ReturnBall()
    {
        ball.transform.position = lastBallPos;
        ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ball.isFloor = true;
    }
    public void ReturnBallActive()
    {
        
            ballCollectActiveTime -= Time.deltaTime;
            if(ballCollectActiveTime < 0)
            {
                isBallCollect = true;
            }
        
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
        SoundManager.instance.PlaySound(1, "UiClickSound");
        heartText.SetText(HeartManager.instance.currentHeart.ToString());
        HeartManager.instance.UseHeartNoReset();
    }
    public void PauseGame()
    {
        isPlay = false;
        _currentTimeScale = Time.timeScale;
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
        SoundManager.instance.PlaySound(1, "UiClickSound");
        SoundManager.instance.PlaySound(0, "MainSound");
    }
    public void ResetGame()
    {
        if (HeartManager.instance.currentHeart <= 0)
        {
            StartCoroutine(HeartFailImage());
        }
        else
        {
            Time.timeScale = 1;
            //SoundManager.instance.PlaySound(1, "UiClickSound");
            SceneFlowManager.ChangeScene("Stage");
        }
    }
    private IEnumerator HeartFailImage()
    {
        heartFailImage.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        heartFailImage.SetActive(false);
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
        }//블록생성
    }
    public void Start()
    {
        if (EffectManager.instance.isParticle)
            onOffParticleButton.GetComponent<Image>().sprite = onOffParticleImage[0];
        else
            onOffParticleButton.GetComponent<Image>().sprite = onOffParticleImage[1];
        if (StageManager.instance) //게임시작시 스테이지 표기
        {
            currentStageTxt.text = StageManager.instance.currentIndex.ToString();
        }
        _dotLine = GetComponent<DotLine>();
        _ballRadius = ball.GetComponent<CircleCollider2D>().radius;
        _dummyBall.SetActive(false);

        if (SoundManager.instance)
        {
            slider[0].value = SoundManager.instance.Player[0].Volume;
            slider[1].value = SoundManager.instance.Player[1].Volume;
        }
        lastBallPos = ball.transform.position; //마지막위치 생성

        this.UpdateAsObservable().Subscribe(_ =>
        {
            starSlider.SetFillArea(ballCount);
        });//달성률 표시


        this.UpdateAsObservable().
            Subscribe(_ =>
            {
                for (int i = 0; i < 3; i++)
                {
                    starSlider.SetStar(i, stars[i]);
                }
            });//별 표시
        this.UpdateAsObservable()
            .Where(_ => isPlay)
             .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y > ball.transform.position.y + minHeight)
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
                 if (!hit.collider.GetComponent<TeleportationBlock>())
                 { 
                     var endPos = Vector3.Reflect(direction, hit.normal) * reflectDotLength;
                     _dotLine.points.Add(new Vector3(hit.centroid.x, hit.centroid.y, 0) + endPos);
                     _dummyBall.transform.position = hit.centroid;
                 }
                 else
                 {
                     hit.collider.GetComponent<TeleportationBlock>().DrawDotLineFromDestination(direction);
                 }
               

                 //투명하게 공표시
                 if (EventSystem.current.IsPointerOverGameObject() == false)
                 {
                     _dotLine.DrawDotLine();
                     _dummyBall.SetActive(true);
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
            .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y > ball.transform.position.y + minHeight)
            .Where(_ => EventSystem.current.IsPointerOverGameObject() == false) //ui위에 있으면 슈팅못하게
            .Select(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition)); //마우스 위치를 필터링

        ballShootStream.Subscribe(mousePos =>
            {
                _dummyBall.SetActive(false);
                mousePos.z = 0;
                var direction = Vector3.Normalize(mousePos - ball.transform.position);
                ball.Shoot(direction * shootPower);//Shoot
                ballCount--;
               
                DeadLineCount();
                isBallCollect = false;
                ballCollectActiveTime = resetballCollectActiveTime;
                //_timeScaleUpRoutine = TimeScaleUp();
                //StartCoroutine(_timeScaleUpRoutine);
            });//공

        this.UpdateAsObservable()
          .Where(_ => isPlay)
          .Where(_ => ballCount > 0)
          .Where(_ => Input.GetMouseButtonUp(0) && ball.isFloor).Subscribe(_ =>
          {
              _dummyBall.SetActive(false);
          });

        this.UpdateAsObservable()
           .Where(_ => isPlay)
            .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y <= ball.transform.position.y + minHeight)
            .Subscribe(_ =>
            {
                _dummyBall.SetActive(false);
            });
      

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
        this.UpdateAsObservable().Select(_ => ball.isFloor)
            .DistinctUntilChanged()
            .Where(x => x == true)
            .Subscribe(_ =>
            {
                
                ballCollectButton.SetActive(false);  //공버튼 안보이게
                StarsCountImage();
                DeadLineCount();
                lastBallPos = ball.transform.position;
            });

        this.UpdateAsObservable()
            .Where(_ => isPlay)
            //.Where(_ => ball.isFloor)
            .Where(_ => isClear)
            .Subscribe(_ =>
            {
                StageClear();
            });//스테이지 클리어
        this.UpdateAsObservable()
            .Where(_ => isPlay)
            .Where(_ => isOver)
            .Subscribe(_ => {
                GameOver();
            });


        this.UpdateAsObservable()
            .Where(_ => isPlay)
            .Where(_ => ball.isFloor)
            .Where(_ => ballCount <= 0)
            .Subscribe(_ =>
            {
                GameOver();
            });


       
        this.UpdateAsObservable()
            .Where(_ => !ball.isFloor)
            .Subscribe(_ =>
            {
                ReturnBallActive();
                if (isBallCollect)
                {
                    ballCollectButton.SetActive(true); //공버튼 보이게
                }
            });
    }

    public void StageClear()
    {
        ball.transform.position = lastBallPos; //원래 위치로
        successPanel.SetActive(true);
        StageManager.instance.LastStageUp();
        StageManager.instance.SetCurrentStageAchievementRate(starCount);
        HeartManager.instance.AddHeartNoReset(1);
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Effect");
        for(int i=0; i<temp.Length; i++)
        {
            temp[i].SetActive(false);
        }
            
        
        EffectManager.instance.UiEffect(0, successPanel.transform.position + new Vector3(0,15));
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
        if(failPanel.active && HeartManager.instance.addMobCount <= 0)
        {
            FailaddMob.ShowAd();//광고를 시작해야 할 때 함수
            HeartManager.instance.addMobCount = 2;
        }      
        PauseGame();
        SoundManager.instance.PlaySound(1, "StageFailSound");
    }

    public Block CreateBlock(BlockType x, int r, int c)
    {
        var instance = Instantiate(blockPrefabs[(int)x]);
        instance.transform.parent = screenCordinate;
        instance.transform.localPosition = gridPosition[r, c];
        blocks[r, c] = instance;
        if (x == BlockType.DEFAULT)
        {
            _breakableBlocks.Add(instance);
        }
        else if (x == BlockType.HOLE)
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
            
            blocks[Math.Clamp(r + 1, 0, _row), c] = instance;
            blocks[r, Math.Clamp(c + 1, 0, _column)] = instance;
            blocks[Math.Clamp(r + 1, 0, _row), Math.Clamp(c + 1, 0, _column)] = instance;
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
                }
            }
        }
        Destroy(x.gameObject);
        _breakableBlocks.Remove(x);
        _holes.Remove(x as HoleBlock);
        _candies.Remove(x as CandyBlock);
        _buttons.Remove(x as ButtonBlock);
        _macaroon.Remove(x as MacaroonBlock);
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
        EffectManager.instance.PlayEffect(2, x.gameObject, 2f);
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
        ballCntTxt.text = deadLineBallCount.ToString();
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
                StarsImage[2].SetActive(false);
                StarsImage[1].SetActive(false);
            });
        }
        else
        {
            StarsImage[2].SetActive(false);
            StarsImage[1].SetActive(false);
            StarsImage[0].SetActive(false);
        }

    }
    public void DeadLineCount()
    {
        if (ballCount > stars[0] && ballCount > stars[1] && ballCount > stars[2])
        {
            deadLineBallCount = ballCount - stars[2];
            deadLineMaxBallCount = initialBallCount - stars[2];
        }
        else if (ballCount > stars[1] && ballCount > stars[0])
        {
            deadLineBallCount = ballCount - stars[1];
            deadLineMaxBallCount = stars[2] - stars[1];
        }
        else if (ballCount >= stars[0])
        {
            deadLineBallCount = ballCount;
            deadLineMaxBallCount = stars[1];
        }

        //ballGaugeImage.fillAmount = (float)deadLineBallCount / deadLindMaxBallCount;
        DOTween.To(() => ballGaugeImage.fillAmount, x => ballGaugeImage.fillAmount = x, (float)deadLineBallCount / (float)deadLineMaxBallCount, 0.2f).SetEase(Ease.InBack);
    }

}
