using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Expansion 
{
    public static bool Contain(this LayerMask layerMask,int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
 
    // bool ret=mask.Contain(6); ������ ����ϴ� Ȯ��޼���
    
    


}
