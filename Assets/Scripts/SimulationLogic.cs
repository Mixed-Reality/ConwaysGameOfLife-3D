using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimulationLogic : MonoBehaviour
{

    private List<Transform> Positions;       // Cell Spawn points list
    public int Dimension;
    public int ZDim = 5;

    //private List<List<List<GameObject>>> CellMatrix;

    private static List<List<GameObject>> CellMatrix = new List<List<GameObject>>();

    // Alive cell prefab
    public GameObject cell;

    private Dictionary<string, bool> StateChanges = new Dictionary<string, bool>();

    private int[][] moves = new int[][] {
        new int[]{0, 1},
        new int []{ 0, -1},
        new int[]{ 1, -1},
        new int[]{ -1, 1},
        new int[]{ 1, 1},
        new int[]{ -1, -1},
        new int[]{ 1, 0},
        new int[]{ -1, 0},
    };

    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 20;
    }


    // Start is called before the first frame update
    void Start()
    {
        // load cell matrix
        int z = 0;
        Debug.Log("Initializing Cell Matrix");
       for(int x = Dimension - 1; x >= 0 ; x--){
            List<GameObject> CellColumn = new List<GameObject>();
            for(int y = 0; y < Dimension; y++) {
                string cellLocationStr = "p"  + y.ToString() + x.ToString() + z.ToString();
                GameObject cellLocation = GameObject.Find(cellLocationStr);
                // CellMatrix[x][y] = cellLocation;
                CellColumn.Add(cellLocation);

            }
            CellMatrix.Add(CellColumn);
        }
    }

    // Update is called once per frame
    void Update()
    { 
        // Traversing CellMatrix
        for(int r = 0; r < Dimension; r++)
        {
            for(int c = 0; c < Dimension; c++)
            {
                int neighbors = 0;
                GameObject currentCell = CellMatrix[r][c];
                //Debug.Log("Current Cell is " + currentCell.name + ", Alive:  " + currentCell.GetComponent<Cell>().isAlive);
                // count alive neighbours 
                foreach(int[] m in moves){
                    int newR = r + m[0];
                    int newC = c + m[1];

                    if ( IsValidPos(newR, newC) && CellMatrix[newR][newC].GetComponent<Cell>().isAlive == true)
                        neighbors++;
                }

                // Death by solitude or overpopulation
                if(neighbors < 2 || neighbors > 3 && currentCell.GetComponent<Cell>().isAlive == true)
                {
                   StateChanges.Add(currentCell.name, false);
                  //currentCell.GetComponent<Cell>().KillCell();
                }
                // cell is born
                else if(currentCell.GetComponent<Cell>().isAlive == false && neighbors == 3)
                {
                    StateChanges.Add(currentCell.name, true);
                   //currentCell.GetComponent<Cell>().CellBirth();
                }
            }
        }

        // apply changes
        foreach(var state in StateChanges)
        {
            GameObject cell = GameObject.Find(state.Key);
            bool willBeAlive = state.Value;
            if (willBeAlive)
            {
                cell.GetComponent<Cell>().CellBirth();
            }
            else
                cell.GetComponent<Cell>().KillCell();
        }
        StateChanges.Clear();
    }

    public void DisplayCellMatrix()
    {
        foreach(List<GameObject> col in CellMatrix)
        {
            foreach(GameObject cell in col)
            {
                Debug.Log(cell.name + " " + cell.GetComponent<Cell>().isAlive + " ");
            }
            Debug.Log("");
        }
    }

    public bool IsValidPos(int x, int y)
    {
        return x >= 0 && x < Dimension && y >= 0 && y < Dimension;
    }

}
