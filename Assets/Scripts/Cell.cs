using UnityEngine;

public class Cell : MonoBehaviour {
    public int row;
    public int column;
    public int numberOfSurroundingDangers;
    public bool flagged = false;
    public bool visible = false;
    public bool dangerous = false;
    private GameObject flag;
    private GameController controller;

    public void Init(int row, int column) {
        this.row = row;
        this.column = column;
        this.name = "Cell_" + row + "_" + column;
        this.flag = Instantiate(Resources.Load("Flag") as GameObject, transform.position, Quaternion.identity, transform);
        this.flag.SetActive(false);
        this.controller = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void Reveal() {
        visible = true;
        controller.cellRevealed(dangerous);
    }

    public void Flag() {
        flagged = true;
        flag.SetActive(true);
        controller.cellFlagged();
    }

    public void Unflag() {
        flagged = false;
        flag.SetActive(false);
        controller.cellUnflagged();
    }

    public void GameOverReveal() {
        Debug.Log("Reveal");
        if (!visible && !flagged) {
            Fog fog = GetComponentInChildren<Fog>();
            if (fog != null) {
                fog.Destroy();
            }
        }
    }
}
