using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour {

    public static void PlaceEnemies(GameObject[] cellList, int numberOfObjectsToPlace, GameObject objectToInstantiate) {
        int size = cellList.Length;
        Random.Range(0, size);
        HashSet<int> placed = new HashSet<int>();
        while (numberOfObjectsToPlace > 0) {
            int n = Random.Range(0, size);
            if (!placed.Contains(n)) {
                GameObject cell = cellList[n];
                cell.GetComponent<Cell>().dangerous = true;
                Instantiate(objectToInstantiate, cell.transform.position, Quaternion.identity, cell.transform);
                placed.Add(n);
                --numberOfObjectsToPlace;
            }
            else {
            }
        }
    }
}
