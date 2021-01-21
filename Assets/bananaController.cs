using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaController : MonoBehaviour
{
    public float despawnFlashSpeed;
    private Rigidbody2D banana;
    private CapsuleCollider2D bananaCollider;
    private SpriteRenderer bananaSprite;
    public bool gameActive;

    // Start is called before the first frame update
    void Start()
    {
        banana = GetComponent<Rigidbody2D>();
        bananaCollider = GetComponent<CapsuleCollider2D>();
        bananaSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        gameActive = GameObject.Find("gameManager").GetComponent<gameManager>().gameActive;

        if (gameActive == false)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(despawnFrames(despawnFlashSpeed));
            IEnumerator despawnFrames(float x)
            {
                Physics2D.IgnoreLayerCollision(2, 8, true);
                for (int i = 0; i < 4; i++)
                {
                    banana.tag = "EditorOnly";
                    bananaSprite.enabled = false;
                    yield return new WaitForSecondsRealtime(x);
                    bananaSprite.enabled = true;
                    yield return new WaitForSecondsRealtime(x);
                }
                Physics2D.IgnoreLayerCollision(2, 8, false);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
