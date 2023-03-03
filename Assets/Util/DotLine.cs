using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class DotLine : MonoBehaviour
{
    public List<Vector3> points;

    public Sprite dot;
    [Range(0.01f, 1f)]
    public float size;
    [Range(0.1f, 2f)]
    public float delta;

    private List<Vector2> _positions = new List<Vector2>();
    private List<GameObject> _dots = new List<GameObject>();

    void FixedUpdate()
    {
        if (_positions.Count > 0)
        {
            DestroyAllDots();
            _positions.Clear();
        }
    }

    private void DestroyAllDots()
    {
        foreach (var dot in _dots)
        {
            Destroy(dot);
        }
        _dots.Clear();
    }

    GameObject GetOneDot()
    {
        var gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * size;
        gameObject.transform.parent = transform;

        var sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = dot;
        return gameObject;
    }

    public void DrawDotLine()
    {
        DestroyAllDots();
        if(points.Count>1)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector3 point = points[i];
                Vector3 direction = (points[i + 1] - points[i]).normalized;

                while ((points[i+1] - points[i]).magnitude > (point - points[i]).magnitude)
                {
                    _positions.Add(point);
                    point += (direction * delta);
                }
            }
        }
        Render();
    }

    private void Render()
    {
        foreach (var position in _positions)
        {
            var g = GetOneDot();
            g.transform.position = position;
            _dots.Add(g);
        }
    }
}
