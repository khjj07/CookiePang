using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using PathCreation;
using UniRx;
using UniRx.Triggers;
using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

namespace PathCreation.Examples
{
    public class StageButtonPlacer: PathSceneTool
    {
        public GameObject holder;
        public GameObject Mover;
        public float spacing = 3;

        const float minSpacing = .1f;
        const float minHeight = -30000.0f;
        const float tick = 300.0f;

        void Generate()
        {
            if (pathCreator != null && holder != null)
            {
                DestroyObjects();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;
                int count = 0;
                while (dst < path.length && count < StageManager.instance.stageAssets.Count)
                {
                    Vector3 point = path.GetPointAtDistance(dst);
                    var obj = StageManager.instance.CreateSelectButton(point, holder.transform, count);
                    if(StageManager.instance.currentIndex==count)
                    {
                        Mover.transform.DOLocalMoveY(100-obj.transform.localPosition.y,0.1f);
                    }

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

            ;

            var downStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
            var upStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0));
            var dragStream = this.UpdateAsObservable()
                .SkipUntil(downStream)
                .TakeUntil(upStream)
                .Select(_ => Camera.main.ScreenToWorldPoint(Input.mousePosition))
                .RepeatUntilDestroy(this);

           var dragUpStream = dragStream
                .Buffer(2,10)
                .Where(_ => EventSystem.current.IsPointerOverGameObject() == false)
                .Where(g => { return g[0].y > g[1].y; })
                .Subscribe(_ =>
                {
                    Mover.transform.DORewind();
                    if(Mover.transform.localPosition.y > 100 )
                    {
                        Mover.transform.DOLocalMoveY(100, 0.01f);
                    }
                    else
                    {
                        Mover.transform.DOLocalMoveY(tick, 0.01f).SetRelative(true);
                    }
                });


            var dragDownStream = dragStream
                  .Buffer(2,10)
                  .Where(_ => EventSystem.current.IsPointerOverGameObject() == false)
                  .Where(g => { return g[0].y < g[1].y; })
                  .Subscribe(_ =>
                  {
                      Mover.transform.DORewind();
                      
                      if (Mover.transform.localPosition.y < minHeight)
                      {
                          Mover.transform.DOLocalMoveY(minHeight, 0.01f);
                      }
                      else
                      {
                          Mover.transform.DOLocalMoveY(-tick, 0.01f).SetRelative(true);
                      }
                  });

        }
        protected override void PathUpdated()
        {

        }

        
    }
}
