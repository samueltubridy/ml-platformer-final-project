using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject blockPrefab;
    public GameObject spikePrefab;
    public List<Vector2> objectPlaces;
    public List<GameObject> objects;
    public List<int> objectTypes;

    void Start() {
        // objectPlaces = new List<Vector2> {new Vector2(4f, 0f), new Vector2(5f, 0f), new Vector2(6f, 0f), new Vector2(7f, 0f), new Vector2(8f, 0f), new Vector2(8f, 1f)};
        // objects = new List<GameObject>();
        // objectTypes = new List<int> {0, 0, 0, 0, 0, 0};

        for (int obj = 0; obj < objectPlaces.Count; obj++) {
            int currentType = objectTypes[obj];
            Vector2 currentPos = objectPlaces[obj];
            if (currentType == 0)
                objects.Add(Instantiate(blockPrefab, new Vector3(currentPos.x, currentPos.y, 0f), Quaternion.identity, transform));
            else if (currentType == 1)
                objects.Add(Instantiate(spikePrefab, new Vector3(currentPos.x, currentPos.y - 0.16f, 0f), Quaternion.identity, transform));
        }
    }
}