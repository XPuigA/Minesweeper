using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labeller : MonoBehaviour {

    public static void Label(Dictionary<string, Cell> map, int numberOfRows, int numberOfColumns) {
        for (int row = 0; row < numberOfRows; ++row) {
            for (int column = 0; column < numberOfColumns; ++column) {
                int numberOfDangers = GetNumberOfDangers(map, numberOfRows, numberOfColumns, row, column);
                SetLabel(GetCell(map, row, column).gameObject, numberOfDangers);
            }
        }
    }

    private static void SetLabel(GameObject cell, int numberOfDangers) {
        Cell cellScript = cell.GetComponent<Cell>();
        cellScript.numberOfSurroundingDangers = numberOfDangers;
        if (!cellScript.dangerous && numberOfDangers > 0) {
            GameObject number = Instantiate(Resources.Load("numbers/" + numberOfDangers.ToString()) as GameObject, cell.transform.position, Quaternion.identity, cell.transform);
        }
    }

    private static int GetNumberOfDangers(Dictionary<string, Cell> map, int numberOfRows, int numberOfColumns, int row, int column) {
        int numberOfDangers = 0;
        for (int direction = 0; direction < 8; ++direction) {
            int newRow = row + Directions.row[direction];
            int newColumn = column + Directions.column[direction];
            if (newRow >= 0 && newRow < numberOfRows && newColumn >= 0 && newColumn < numberOfColumns) {
                Cell cell = GetCell(map, newRow, newColumn);
                if (cell.dangerous) numberOfDangers++;
            }
        }
        return numberOfDangers;
    }

    private static Cell GetCell(Dictionary<string, Cell> map, int row, int column) {
        Cell value;
        map.TryGetValue(row + "_" + column, out value);
        return value;
    }
}
