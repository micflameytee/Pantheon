using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public PlayerController owner { get; set; }
    public int damage = 3;    
    private RaycastHit2D[] _hits = new RaycastHit2D[20];
    public float radius = 1;
    [SerializeField] private SpriteAnimation Exsplosion;

    public Vector2 direction;
    
    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = this.transform.rotation;
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != owner.gameObject)
        {
            Debug.Log($"enemy", other.gameObject);
            Explode();
        }
        else
        {
            Debug.Log($"player owned");
        }
    }
    
    public void Explode()
    {
        
        var newInstance = Instantiate(Exsplosion, transform.position, transform.rotation);
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        int numHits = Physics2D.CircleCastNonAlloc(position, radius, Vector2.zero, _hits, 0f);
        for (int i = 0; i < numHits; i++)
        {
            RaycastHit2D hit = _hits[i];
            HealthSystem targetSystem = hit.collider.GetComponent<HealthSystem>(); 
            targetSystem?.TakeDamage(damage, owner);
        } 
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
