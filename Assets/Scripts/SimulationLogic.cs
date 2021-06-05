using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimulationLogic : MonoBehaviour
{

    private List<Transform> Positions;       // Cell Spawn points list
    public int Dimension;
    public int ZDim = 5;

    private List<List<List<GameObject>>> CellMatrix = new List<List<List<GameObject>>>();

    //private static List<List<GameObject>> CellMatrix = new List<List<GameObject>>();

    // Alive cell prefab
    public GameObject cell;

    private Dictionary<string, bool> StateChanges = new Dictionary<string, bool>();


    private int[][] moves = new int[][] {
        new int[]{0, -1, 0 },
        new int[]{1, -1, 0 },
        new int[]{1, 0, 0 },
        new int[]{1, 1, 0 },
        new int[]{0, 1, 0 },
        new int[]{-1, 1, 0 },
        new int[]{-1, 0, 0 },
        new int[]{-1, -1, 0 },

        new int[]{0, -1, 1 },
        new int[]{1, -1, 1 },
        new int[]{1, 0, 1 },
        new int[]{1, 1, 1 },
        new int[]{0, 1, 1 },
        new int[]{-1, 1, 1 },
        new int[]{-1, 0, 1 },
        new int[]{-1, -1, 1 },

        new int[]{0, -1, -1},
        new int[]{1, -1, -1},
        new int[]{1, 0, -1},
        new int[]{1, 1, -1},
        new int[]{0, 1, -1},
        new int[]{-1, 1, -1},
        new int[]{-1, 0, -1},
        new int[]{-1, -1, -1},

        new int[]{0, 0, 1},
        new int[]{0, 0, -1},

    };

    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 10;
    }


    // Start is called before the first frame update
    void Start()
    {
        // load cell matrix
       // int z = 0;
        Debug.Log("Initializing Cell Matrix");
        for(int z=0; z < ZDim; z++){
            List<List<GameObject>> CellMatrix2D = new List<List<GameObject>>();
            for (int y = Dimension - 1; y >= 0; y--){
                List<GameObject> CellColumn = new List<GameObject>();
                for (int x = 0; x < Dimension; x++){
                    string cellLocationStr = "p" + x.ToString() + y.ToString() + z.ToString();
                    GameObject cellLocation = GameObject.Find(cellLocationStr);
                    // CellMatrix[x][y] = cellLocation;
                    CellColumn.Add(cellLocation);

                }
                CellMatrix2D.Add(CellColumn);
            }
            CellMatrix.Add(CellMatrix2D);
        }

    }

    // Update is called once per frame
    void Update()
    {

            // Traversing CellMatrix
            for (int k = 0; k < ZDim; k++)
            {
                // looping z axis
                for (int r = 0; r < Dimension; r++)
                {
                    for (int c = 0; c < Dimension; c++)
                    {
        
                        int neighbors = 0;
                        GameObject currentCell = CellMatrix[k][r][c];
                        //Debug.Log("Object: " + currentCell.name);

                        //currentCell.GetComponent<Cell>().CellBirth();


                        //Debug.Log("Current Cell is " + currentCell.name + ", Alive:  " + currentCell.GetComponent<Cell>().isAlive);
                        // count alive neighbours 
                        foreach (int[] m in moves)
                        {
                            int newK = k + m[2];
                            int newR = r + m[0];
                            int newC = c + m[1];
                         
                            if (IsValidPos(newR, newC, newK) && CellMatrix[newK][newR][newC].GetComponent<Cell>().isAlive == true)
                                neighbors++;
                        }

                        // Death by solitude or overpopulation
                        if (neighbors < 2 || neighbors > 3 && currentCell.GetComponent<Cell>().isAlive == true)
                        {
                            StateChanges.Add(currentCell.name, false);
                            //currentCell.GetComponent<Cell>().KillCell();
                        }
                        // cell is born
                        else if (currentCell.GetComponent<Cell>().isAlive == false && neighbors == 3)
                        {
                            StateChanges.Add(currentCell.name, true);
                            //currentCell.GetComponent<Cell>().CellBirth();
                        }
                    }
                }
            }


            // apply changes
            foreach (var state in StateChanges)
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


    public bool IsValidPos(int x, int y, int k)
    {
        return x >= 0 && x < Dimension && y >= 0 && y < Dimension && k >=0 && k < ZDim;
    }

}
