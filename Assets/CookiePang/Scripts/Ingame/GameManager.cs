using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System.Linq;

public class GameManager : Singleton<GameManager>
{

    public Block[,] blocks;
    public Vector3[,] blocksPosition;
    public int _column = 9;
    public int _row = 9;

    public bool isPlay;
    public float shootPower;
    public float minHeight = -15.0f;
    public int ballCount;

    [SerializeField]
    public Ball ball;
    [SerializeField]
    private Block[] blockPrefabs;
    [SerializeField, Range(5.0f, 10.0f)]
    private float reflectDotLength;
    private DotLine _dotLine;

    [SerializeField]
    private float offset = 110;

    public void Start()
    {
        blocks = new Block[_row, _column];
        blocksPosition = new Vector3[_row, _column];
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                blocksPosition[i,j] = Camera.main.ScreenToWorldPoint(new Vector3((i + 1) * offset - Screen.width / 2, Screen.height / 2 - (j + 1) * offset, 0));
                blocksPosition[i, j].z=0;
            }
        }
        _dotLine = GetComponent<DotLine>();
        this.UpdateAsObservable()
            .Where(_ => isPlay)
             .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y > minHeight)
             .Where(_ =>
             {
                 var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 mousePos.z = 0;
                 var ballPos = ball.transform.position;
                 var direction = Vector3.Normalize(mousePos - ballPos);
                 return Input.GetMouseButton(0) && ball.isFloor && Physics.Raycast(ballPos, direction);
             }).Select(_ =>
             {
                 var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 mousePos.z = 0;
                 var ballPos = ball.transform.position;
                 var direction = Vector3.Normalize(mousePos - ballPos);
                 RaycastHit hit;
                 Physics.Raycast(ballPos, direction, out hit);
                 return hit;
             }).Subscribe(hit =>
             {
                 _dotLine.points.Clear();
                 _dotLine.points.Add(ball.transform.position);
                 _dotLine.points.Add(hit.point);

                 var direction = Vector3.Normalize(hit.point - ball.transform.position);
                 var endPos = Vector3.Reflect(direction, hit.normal) * reflectDotLength;
                 _dotLine.points.Add(hit.point + endPos);

                 _dotLine.DrawDotLine();
             });

        this.UpdateAsObservable()
            .Where(_ => isPlay)
            .Where(_ => Input.GetMouseButtonUp(0) && ball.isFloor) //마우스 업 && ball이 땅에 있다면
            .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y > minHeight)
            .Select(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition)) //마우스 위치를 필터링
            .Subscribe(mousePos =>
            {
                mousePos.z = 0;
                var direction = Vector3.Normalize(mousePos - ball.transform.position);
                ball.Shoot(direction * shootPower);//Shoot
            });

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
                    return;
                }
            }
        }
    }

    public void ClearBlock()
    {
        foreach (var block in blocks)
        {
            Destroy(block.gameObject);
        }
        blocks = new Block[_row, _column];
    }

    public Block CreateBlock(BlockType x, int row, int column)
    {
        var instance = Instantiate(blockPrefabs[(int)x - 1].gameObject);
        blocks[row, column] = instance.GetComponent<Block>();
        instance.transform.position = blocksPosition[row,column];
        return instance.GetComponent<Block>();
    }
}
