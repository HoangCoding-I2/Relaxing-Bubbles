using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject fx_Dead;
    [SerializeField] private GameObject fx_ColorChange;
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private int ySpeedMax;
    [SerializeField] private int yAccelerationForce;
    [SerializeField] private int yDecelerationForce;
    private float angle = 0;
    private float mapWidth;
    private bool isDead = false;
    private Rigidbody2D rb;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        mapWidth = gameManager.GetComponent<DisplayManager>().GetWidth();
    }

    private void Update()
    {
        if (isDead) return;
        
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 position = transform.position;
        
        position.x = Mathf.Cos(angle) * (mapWidth * 0.45f);
        
        position.y += ySpeed * Time.deltaTime;
        
        transform.position = position;
        
        angle += Time.deltaTime * xSpeed;

        if (Input.GetMouseButton(0))
        {
            if (rb.velocity.y < ySpeedMax)
            {
                rb.AddForce(new Vector2(0, yAccelerationForce));
            }
        }
        else 
        {
            if (rb.velocity.y > 0)
            {
                rb.AddForce(new Vector2(0, -yDecelerationForce));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item_ColorChange"))
        {
            GameObject itemFxGameObject = Instantiate(fx_ColorChange, other.gameObject.transform.position, Quaternion.identity); 
            Destroy(itemFxGameObject, 0.5f);
            
            Destroy(other.gameObject.transform.parent.gameObject);
            
            ColorManager.Instance.ChangeBackgroundColor();
            ScoreManager.Instance.AddScore();
            AudioManager.Instance.PlayItemCollectSound();
        }

        if (other.gameObject.CompareTag("Obstacle") && isDead == false)
        {
            isDead = true;
            
            GameObject deadFx = Instantiate(fx_Dead, transform.position, Quaternion.identity); 
            Destroy(deadFx, 0.5f);
            
            rb.velocity = new Vector2(0, 0);  
            rb.isKinematic = true;  

            AudioManager.Instance.StopBackgroundMusic();
            gameManager.GetComponent<GameManager>().GameOver();
            AudioManager.Instance.PlayDeadSound();
        }
    }
}
