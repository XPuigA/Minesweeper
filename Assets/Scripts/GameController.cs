using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)           
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public GameObject cell;
    public GameObject mine;
    public GameObject fog;
    public int numberOfRows = 10;
    public int numberOfColumns = 10;
    public int numberOfMines = 10;

    private int flaggedCells;
    private int totalCells;
    private int revealedCells;

    private bool gameOver;
    private bool boardRevealed;
    private bool gameWon;

    private Dictionary<string, Cell> map;

	// Use this for initialization
	void Start () {
        GameObject boardContainer = Generator.Generate(cell, numberOfRows, numberOfColumns);
        GameObject[] cellList = FindByTag("Cell");
        map = fillMap(cellList);
        Placer.PlaceEnemies(cellList, numberOfMines, mine);
        Labeller.Label(map, numberOfRows, numberOfColumns);
        flaggedCells = 0;
        totalCells = numberOfRows * numberOfColumns;
        revealedCells = 0;
        gameOver = false;
        boardRevealed = false;
        gameWon = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (gameOver && !boardRevealed) {
            foreach (GameObject cell in FindByTag("Cell")) {
                cell.GetComponent<Cell>().GameOverReveal();
            }
            boardRevealed = true;
            GameUIController.instance.Show("Game Lost", Color.red);
        }
        else if (gameWon) {
            GameUIController.instance.Show("Game Won", Color.green);
        }
	}

    GameObject[] FindByTag(string tag) {
        return GameObject.FindGameObjectsWithTag(tag);
    }

    private Dictionary<string, Cell> fillMap(GameObject[] cellList) {
        Dictionary<string, Cell> result = new Dictionary<string, Cell>();
        foreach (GameObject cellGO in cellList) {
            Cell cell = cellGO.GetComponent<Cell>();
            result.Add(cell.row + "_" + cell.column, cell);
        }
        return result;
    }

    public void cellFlagged() {
        flaggedCells++;
        CheckGameWon();
    }

    public void cellUnflagged() {
        flaggedCells--;
        CheckGameWon();
    }

    public void cellRevealed(Cell cell) {
        revealedCells++;
        gameOver = cell.dangerous;
        CheckGameWon();
        if (!gameOver && cell.numberOfSurroundingDangers == 0) {
            RevealSurroundingZeroes(cell);
        }
    }

    public void CheckGameWon() {
        Debug.Log(flaggedCells);
        gameWon = !gameOver && revealedCells + flaggedCells == totalCells;
    }

    private void RevealSurroundingZeroes(Cell originCell) {      
        for (int i = 0; i < Directions.row.Length; ++i) {
            Cell nextCell;
            string cellId = (originCell.row + Directions.row[i]) + "_" + (originCell.column + Directions.column[i]);
            if (map.TryGetValue(cellId, out nextCell)) {
                if (!nextCell.visible) {
                    nextCell.Reveal();
                }
            }
        }
    }
}
