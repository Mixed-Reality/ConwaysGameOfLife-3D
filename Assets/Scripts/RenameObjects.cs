using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RenameObjects : MonoBehaviour
{
    private char zVal = '4';
    // Start is called before the first frame update
    void Start()
    {

        string name = gameObject.name;
        string[] strArr = name.Split(' ');
       
        System.Text.StringBuilder str = new System.Text.StringBuilder(strArr[0]);
       // Debug.Log("str: " + str);

        str.Remove(3, 1);
        str.Append(zVal);
        //Debug.Log("str replaced: " + str);

        //Debug.Log(str.ToString());
        gameObject.name = str.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
