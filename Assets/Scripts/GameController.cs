using UnityEngine;

public class GameController : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
        GameObject boardContainer = Generator.Generate(cell, numberOfRows, numberOfColumns);
        Placer.PlaceEnemies("Cell", numberOfMines, mine);
        Labeller.Label("Cell", numberOfRows, numberOfColumns);
        Placer.PlaceFog("Cell", fog);
        flaggedCells = 0;
        totalCells = numberOfRows * numberOfColumns;
        revealedCells = 0;
        gameOver = false;
        boardRevealed = false;
        gameWon = false;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("gameOver " + gameOver);
		if (gameOver && !boardRevealed) {
            Debug.Log("entra");
            Debug.Log(GameObject.FindGameObjectsWithTag("Cell"));
            foreach (GameObject cell in GameObject.FindGameObjectsWithTag("Cell")) {
                cell.GetComponent<Cell>().GameOverReveal();
            }
            boardRevealed = true;
        }
        else if (gameWon) {
            Debug.Log("Game Won");
        }
	}

    public void cellFlagged() {
        flaggedCells++;
        CheckGameWon();
    }

    public void cellUnflagged() {
        flaggedCells++;
        CheckGameWon();
    }

    public void cellRevealed(bool wasDanger) {
        Debug.Log(wasDanger);
        revealedCells++;
        gameOver = wasDanger;
        CheckGameWon();
    }

    public void CheckGameWon() {
        gameWon = !gameOver && revealedCells + flaggedCells == totalCells;
    }
}
