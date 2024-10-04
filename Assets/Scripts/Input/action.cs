using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action : MonoBehaviour
{

    public StaticScript sObject = new StaticScript();

    public void print(string value)
    {
        StaticScript.coins += 1;
        Debug.Log("aaaa" + value.ToString() + "coins " + StaticScript.coins);
    }

    public void printSomethingElse(InputObject objectValue)
    {
        //Debug.Log("bbbb " + items.ToString());
        Debug.Log("aheihaief" + objectValue.keyName);
    }
}
