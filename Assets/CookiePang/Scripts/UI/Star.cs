using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Star : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        //_textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetValue(int val)
    {
        value = val;
        //_textMeshPro.SetText(value.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
