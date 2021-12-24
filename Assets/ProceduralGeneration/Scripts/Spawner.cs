using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] Planets;

    private void Start() {
        int index = Random.Range(0, Planets.Length - 1);
        GameObject planet = Instantiate(Planets[index], new Vector3(0, 0, 0), Quaternion.identity);
    }
}
