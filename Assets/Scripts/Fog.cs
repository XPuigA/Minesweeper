using UnityEngine;

public class Fog : MonoBehaviour {

    void OnMouseOver() {
        Cell cell = GetComponentInParent<Cell>();
        if (Input.GetMouseButtonDown(0)) {
            cell.LeftClicked();
        }
        else if (Input.GetMouseButtonDown(1)) {
            cell.RightClicked();
        }
    }
}
