using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopupUI : BaseUI
{
    public override void Show()
    {
        base.Show();
        transform.SetAsLastSibling();
    }
}
