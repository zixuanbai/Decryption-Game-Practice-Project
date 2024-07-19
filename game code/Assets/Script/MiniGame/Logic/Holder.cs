﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{
    public BallName matchBall;

    private Ball currentBall;

    public HashSet<Holder> linkHolders = new HashSet<Holder>();
    
    public bool isEmpty = true;

    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if (ball.ballDetails.ballName == matchBall)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
        isEmpty = false;
    }

    public override void EmptyClicked()
    {
        foreach (var holder in linkHolders)
        {
            if (holder.isEmpty)
            {
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                holder.CheckBall(currentBall);
                this.currentBall = null;

                this.isEmpty = true;
                holder.isEmpty = false;


                EventHandler.CallCheckGameStateEvent();

                break;
            }
        }
    }
}
