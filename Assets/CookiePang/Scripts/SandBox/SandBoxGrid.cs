using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    None,

}

public class SandBoxGrid : MonoBehaviour
{
    Block target;
    private void OnMouseOver()
    {
        SandBoxUI.current = this;
    }
}
