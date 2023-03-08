using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
public enum BlockType
{
    NONE,
    DEFAULT,
    TELEPORT,
    BOMB
    
}

public class GameManager : Singleton<GameManager>
{
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

    public List<Block> blocks = new List<Block>();

    private DotLine _dotLine;

    public void Start()
    {
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
        blocks.Remove(x);
        Destroy(x.gameObject);
    }
    public Block CreateBlock(BlockType x)
    {
        var instance = Instantiate(blockPrefabs[(int)x - 1].gameObject);
        blocks.Add(instance.GetComponent<Block>());
        return instance.GetComponent<Block>();
    }
}
