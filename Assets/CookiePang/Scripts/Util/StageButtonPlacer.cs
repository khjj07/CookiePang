using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using PathCreation;
using UniRx;
using UniRx.Triggers;
using UnityEditor.PackageManager;
using System;
using DG.Tweening;
using Unity.VisualScripting;

namespace PathCreation.Examples
{
    public class StageButtonPlacer: PathSceneTool
    {
        public GameObject holder;
        public float spacing = 3;

        const float minSpacing = .1f;
        const float tick = 1.0f;

        void Generate()
        {
            if (pathCreator != null && holder != null)
            {
                DestroyObjects();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;
                int count = 0;
                while (dst < path.length)
                {
                    Vector3 point = path.GetPointAtDistance(dst);
                    StageManager.instance.CreateSelectButton(point, holder.transform, count);
                    dst += spacing;
                    count++;
                }
            }
        }

        void DestroyObjects()
        {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(holder.transform.GetChild(i).gameObject, false);
            }
        }

        protected void Start()
        {
            if (pathCreator != null)
            {
                Generate();
            }


            var downStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
            var upStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0));
            var dragStream = this.UpdateAsObservable()
                .SkipUntil(downStream)
                .TakeUntil(upStream)
                .Select(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition))
                .RepeatUntilDestroy(this);

           var dragUpStream = dragStream
                .Buffer(2,10)
                .Where(g => { return g[0].y > g[1].y; })
                .Subscribe(_ =>
                {
                    holder.transform.DORewind();
                    holder.transform.DOMoveY(tick, 0.1f).SetRelative(true);
                });


            var dragDownStream = dragStream
                  .Buffer(2,10)
                  .Where(g => { return g[0].y < g[1].y; })
                  .Subscribe(_ =>
                  {
                      holder.transform.DORewind();
                      holder.transform.DOMoveY(-tick, 0.1f).SetRelative(true);
                  });

        }
        protected override void PathUpdated()
        {

        }

        
    }
}
