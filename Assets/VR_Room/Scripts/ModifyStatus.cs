using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyStatus : MonoBehaviour
{
    public bool selected = false;

    public void ChangeTo_Selected()
    {
        selected = true;
    }

    public void ChangeTo_DeSelected()
    {
        selected = false;
    }
}
