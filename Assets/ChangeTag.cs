using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStatus : MonoBehaviour
{
    public bool selected = false;

    public void changeTo_Selected()
    {
        selected = true;
    }

    public void changeTo_DeSelected()
    {
        selected = false;
    }
}

