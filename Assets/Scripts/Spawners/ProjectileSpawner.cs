using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Transform spawnPos;
    public Projectile projectileToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnProjectile());
    }

    IEnumerator SpawnProjectile()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            projectileToSpawn.dir = transform.right;
            GameObject g = GameObject.Instantiate(projectileToSpawn.gameObject, null);
            g.transform.position = spawnPos.position;
        }
    }
}
