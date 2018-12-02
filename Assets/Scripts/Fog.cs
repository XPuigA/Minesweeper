using UnityEngine;

public class Fog : MonoBehaviour {

    void OnMouseOver() {
        Cell cell = GetComponentInParent<Cell>();
        if (Input.GetMouseButtonDown(0)) {
            if (!cell.visible && !cell.flagged) {
                cell.Reveal();
                Destroy(this.gameObject);
            }
        }
        else if (Input.GetMouseButtonDown(1)) {
            if (!cell.visible) {
                if (!cell.flagged) {
                    cell.Flag();
                }
                else if (cell.flagged) {
                    cell.Unflag();
                }
            }
        }
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }
}
