using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    private IState curState;

    public void Update()  //monobehavior의 update가 아님 (상속하지 않음 )
    {
        curState.Update();
    }
    public void SetInitState(IState initState)
    {
        curState = initState;
        curState.Enter();
    }

    public void ChangeState(IState nextState) //상태 변경 
    {
        curState.Exit();
        curState= nextState;
        curState.Enter();
    }
}
