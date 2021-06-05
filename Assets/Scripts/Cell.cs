using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public bool isAlive = false;

    private Color[] Colors = new Color[] { Color.black, Color.red, Color.blue, Color.white, Color.yellow, Color.cyan, Color.magenta };

    // Alive cell prefab
    public GameObject cellCube;
    public GameObject AliveCell;

   // public Material CellMaterial;

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
        int clrIndex = Random.Range(0, Colors.Length-1);
        Color cellColor = Colors[clrIndex];
        isAlive = true;
        AliveCell = Instantiate(cellCube, gameObject.transform.position, gameObject.transform.rotation);
        AliveCell.GetComponent<MeshRenderer>().material.color = cellColor;
    }

    public void KillCell()
    {
        isAlive = false;
        //Debug.Log("Killed: " + gameObject.name);
        Destroy(AliveCell);
    }

}
