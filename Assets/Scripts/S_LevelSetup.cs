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
            SpawnAtRandomPosition();
        }
    }

    private void SpawnAtRandomPosition()
    {
        Vector2 randomPos = new Vector2(Random.Range(-12.44f, 12.44f), Random.Range(6.65f, -6.65f));
        RaycastHit2D hit = Physics2D.Raycast(randomPos, Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Border"));

        if (hit)
        {
            SpawnAtRandomPosition();
        }
        else
        {
            Instantiate(npcPrefab, randomPos, transform.rotation);
        }
    }
}

