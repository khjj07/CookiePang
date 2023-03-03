using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float shootPower;
    public float minHeight=-15.0f;

    [SerializeField]
    private Ball _ball;
    private DotLine _dotLine;

    [SerializeField, Range(5.0f,10.0f)]
    private float reflectDotLength;
    public void Start()
    {
        _dotLine = GetComponent<DotLine>();
        this.UpdateAsObservable()
             .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y > minHeight)
             .Where(_ => {
                 var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 mousePos.z = 0;
                 var ballPos = _ball.transform.position;
                 var direction = Vector3.Normalize(mousePos - ballPos);
                 return Input.GetMouseButton(0) && _ball.isFloor && Physics.Raycast(ballPos, direction);
               }).Select(_ => {
                var mousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                var ballPos = _ball.transform.position;
                var direction = Vector3.Normalize(mousePos - ballPos);
                RaycastHit hit;
                Physics.Raycast(ballPos, direction, out hit);
                return hit;
            }).Subscribe(hit =>
            {
                _dotLine.points.Clear();
                _dotLine.points.Add(_ball.transform.position);
                _dotLine.points.Add(hit.point);
         
                var direction = Vector3.Normalize(hit.point - _ball.transform.position);
                var endPos = Vector3.Reflect(direction, hit.normal) * reflectDotLength;
                _dotLine.points.Add(hit.point+endPos);

                _dotLine.DrawDotLine();
            });

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0) && _ball.isFloor) //마우스 업 && ball이 땅에 있다면
            .Where(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition).y>minHeight) 
            .Select(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition)) //마우스 위치를 필터링
            .Subscribe(mousePos =>
            {
                mousePos.z = 0;
                var direction = Vector3.Normalize(mousePos - _ball.transform.position);
                _ball.Shoot(direction* shootPower);//Shoot
            });
    }
}
