using UnityEngine;

public class Fog : MonoBehaviour {

    void OnMouseOver() {
        Cell cell = GetComponentInParent<Cell>();
        if (Input.GetMouseButtonDown(0)) {
            bool clickResult = cell.LeftClicked();
            if (clickResult) Destroy(this.gameObject);
        }
        else if (Input.GetMouseButtonDown(1)) {
            cell.RightClicked();
        }
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }
}
