using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTExplode : MonoBehaviour
{
    
    public float explodeRad;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;
    private ItemScript itemScript;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemScript = GetComponent<ItemScript>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!HookScript.fishNet)
        {
            if (collision.CompareTag(Tags.READY))
            {
                spriteRenderer.enabled = false;
                circleCollider2D.radius = explodeRad;
                Invoke(nameof(Return), 0.02f);
            }
            if (collision.CompareTag(Tags.EXPLODE))
            {
                itemScript.scoreValue = 1;
                gameObject.AddComponent<Rigidbody2D>();
                spriteRenderer.enabled = false;
                circleCollider2D.radius = explodeRad;
                Invoke(nameof(Return), 0.02f);
                Invoke(nameof(DisableExplodeChainTNT), 0.02f);
            }
        }

        
    }

    private void Return()
    {
        circleCollider2D.enabled = false;
    }
    private void DisableExplodeChainTNT()
    {
        this.gameObject.SetActive(false);
    }
}
