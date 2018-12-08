using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labeller : MonoBehaviour {

    static int[] rowDirection = { -1, -1, -1, 0, 0, 1, 1, 1 };
    static int[] columnDirection = { -1, 0, 1, -1, 1, -1, 0, 1 };

    public static void Label(string tag, int numberOfRows, int numberOfColumns) {
        for (int row = 0; row < numberOfRows; ++row) {
            for (int column = 0; column < numberOfColumns; ++column) {
                int numberOfDangers = GetNumberOfDangers(numberOfRows, numberOfColumns, row, column);
                SetLabel(GetCell(row, column), numberOfDangers);
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

    private static int GetNumberOfDangers(int numberOfRows, int numberOfColumns, int row, int column) {
        int numberOfDangers = 0;
        for (int direction = 0; direction < 8; ++direction) {
            int newRow = row + rowDirection[direction];
            int newColumn = column + columnDirection[direction];
            if (newRow >= 0 && newRow < numberOfRows && newColumn >= 0 && newColumn < numberOfColumns) {
                Cell cell = GetCell(newRow, newColumn).GetComponent<Cell>();
                if (cell.dangerous) numberOfDangers++;
            }
        }
        return numberOfDangers;
    }

    private static GameObject GetCell(int row, int column) {
        return GameObject.Find("Cell_" + row + "_" + column);
    }
}
