using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    Transform tran;
    ParticleSystem.MainModule partical;
    public Vector3 MovementDirection = new Vector3(0.07f, 0f, 0f);
    void Start()
    {
        tran = GetComponent<Transform>();
        partical = GetComponentInChildren<ParticleSystem>().main;
        partical.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
    }

    void Update()
    {
        tran.Translate(MovementDirection);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
