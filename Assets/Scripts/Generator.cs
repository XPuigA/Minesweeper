using UnityEngine;

public class Generator : MonoBehaviour {

	public static GameObject Generate(GameObject toInstantiate, int numberOfRows, int numberOfColumns) {
        GameObject parent = new GameObject("Board");
        
        for (int row = 0; row < numberOfRows; ++row) {
            for (int column = 0; column < numberOfColumns; ++column) {
                GameObject cell = Instantiate(toInstantiate, new Vector2(column, row), Quaternion.identity, parent.transform);
                cell.GetComponent<Cell>().Init(row, column);
            }
        }

        return parent;
    }
	
}
