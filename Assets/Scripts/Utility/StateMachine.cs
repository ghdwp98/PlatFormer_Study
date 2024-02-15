using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    private IState curState;

    public void Update()  //monobehavior�� update�� �ƴ� (������� ���� )
    {
        curState.Update();
    }
    public void SetInitState(IState initState)
    {
        curState = initState;
        curState.Enter();
    }

    public void ChangeState(IState nextState) //���� ���� 
    {
        curState.Exit();
        curState= nextState;
        curState.Enter();
    }
}
