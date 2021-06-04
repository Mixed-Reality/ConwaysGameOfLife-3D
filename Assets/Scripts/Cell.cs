using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public bool isAlive = false;

    // Alive cell prefab
    public GameObject cellCube;
    public GameObject AliveCell;

    // Start is called before the first frame update
    void Start()
    {
        if (isAlive)
            CellBirth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CellBirth()
    {
        isAlive = true;
        AliveCell = Instantiate(cellCube, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void KillCell()
    {
        isAlive = false;
        //Debug.Log("Killed: " + gameObject.name);
        Destroy(AliveCell);
    }

}
