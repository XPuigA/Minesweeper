using UnityEngine;

public class Cell : MonoBehaviour {
    public int row;
    public int column;
    public int numberOfSurroundingDangers;
    public bool flagged = false;
    public bool visible = false;
    public bool dangerous = false;
    private GameObject flag;
    private GameObject fog;
    private GameController controller;
    private bool cellEnabled = true;

    public void Init(int row, int column) {
        this.row = row;
        this.column = column;
        this.name = "Cell_" + row + "_" + column;
        this.flag = Instantiate(Resources.Load("Flag") as GameObject, transform.position, Quaternion.identity, transform);
        this.flag.SetActive(false);
        this.fog = Instantiate(Resources.Load("Fog") as GameObject, transform.position, Quaternion.identity, transform);
        this.fog.SetActive(true);
        this.controller = GameController.instance;
    }

    public void LeftClicked() {
        if (cellEnabled && !visible && !flagged) {
            Reveal();
        }
    }

    public void RightClicked() {
        if (cellEnabled && !visible) {
            if (!flagged) {
                Flag();
            }
            else if (flagged) {
                Unflag();
            }
        }
    }

    public void Reveal() {
        visible = true;
        this.fog.SetActive(false);
        controller.cellRevealed(this);
        if (flagged) Unflag();
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
        if (!visible && !flagged) {
            this.fog.SetActive(false);
        }
        cellEnabled = false;
    }
}
