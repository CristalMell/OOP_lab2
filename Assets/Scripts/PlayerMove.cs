using TMPro;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMove : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public TextMeshProUGUI scoreText;
    public int collected = 0;
    public float speed = 5f;
    private Vector2 direction;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
        if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
        if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
        if (Input.GetKey(KeyCode.D)) direction += Vector2.right;
        //transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            collected++;
            Debug.Log($"Собрано предметов: {collected} ");
            scoreText.text = $"Score: {collected}";
            //Destroy(other.gameObject);

            if (collected >= 7)
                spriteRenderer.color = Color.blue;
            else if (collected >= 6)
                spriteRenderer.color = Color.cyan;
            else if (collected >= 3)
                spriteRenderer.color = Color.magenta;
        }
        else if (other.CompareTag("BadCollected"))
        {
            collected = 0;
            scoreText.text = "Score: 0";
            Debug.Log("Ты всё потерял.");
            Destroy(other.gameObject);
        }
    }

}
