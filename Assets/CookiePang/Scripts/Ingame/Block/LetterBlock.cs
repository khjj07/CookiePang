using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LetterBlock : Block
{
    char letter;
    public override void Hit(int damage)
    {
        GameManager.instance.DeleteBlock(this);
        GameManager.instance._letters.Append(letter);
    }
}