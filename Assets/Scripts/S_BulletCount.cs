using UnityEngine;

public class S_BulletCount : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject lastBulletObject;
    private int bulletCount = 0;
    private Vector2 startingPos;

    private void Start()
    {
        lastBulletObject = transform.GetChild(0).gameObject;
        startingPos = transform.GetChild(0).position;
    }

    // Adds a new bullet sprite to the left of the last one (if there is one)
    public void AddBullet()
    {
        bulletCount ++;

        // Use the starting position if there is no bullets left to reference
        Vector2 newPos = startingPos;

        // Go to the left of the last bullet in the magazine
        if (lastBulletObject != null)
        {
            newPos = new Vector2(lastBulletObject.transform.position.x - 70, lastBulletObject.transform.position.y);
        }

        lastBulletObject = Instantiate(bulletPrefab, newPos, transform.rotation, this.transform);
    }

    // Remove the last bullet if there is one
    public void RemoveBullet()
    {
        if (bulletCount >= 0)
        {
            Destroy(transform.GetChild(bulletCount).gameObject);
            bulletCount--;
        }
    }
}
