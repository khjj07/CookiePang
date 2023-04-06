using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using PathCreation;

namespace PathCreation.Examples
{
    public class StageButtonPlacer: PathSceneTool
    {
        public GameObject holder;
        public float spacing = 3;

        const float minSpacing = .1f;

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
        }
        protected override void PathUpdated()
        {

        }
    }
}
