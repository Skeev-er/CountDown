using UnityEngine;

public class S_LevelSetup : MonoBehaviour
{
    public GameObject npcPrefab;
    public int numberOfNpcs = 100;

    void Start()
    {
         RandomiseNpcPositions();
    }

    // Spawn NPCs at random positions
    private void RandomiseNpcPositions()
    {
        for (int i = 0; i < numberOfNpcs; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-8.39f, 8.39f), Random.Range(4.37f, -4.37f));
            Instantiate(npcPrefab, randomPos, transform.rotation);
        }
    }
}
