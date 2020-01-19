using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damager))]
public class Projectile : MonoBehaviour
{
    public Vector2 dir;
    public float speed;

    public float lifetime = 4.0f;
    public float startTime;

    public Damager damager;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    private void OnEnable()
    {
        damager.OnParry += OnParry;
    }

    private void OnDisable()
    {
        damager.OnParry -= OnParry;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        Vector2 displacement = dir.normalized * Time.deltaTime * speed;

        transform.position = pos + displacement;

        if (Time.time > (startTime + lifetime))
        {
            Destroy(gameObject);
        }
    }

    void OnParry(Vector2 aDir)
    {
        float dirY = dir.y;
        dir = Vector2.Reflect(dir, aDir);
        dir.y = dirY;
    }
}
