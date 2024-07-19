using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2AReset : Interactive
{
    public override void EmptyClicked()
    {
        GameController.Instance.ResetGame();
    }
}
