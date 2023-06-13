using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
}

class A
{
    protected int a,b;
    public float F1, F2;
}

class B : A
{
    void checkData()
	{
        Debug.Log("");
	}
}