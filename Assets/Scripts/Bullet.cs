using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public int damage = 1;

    public float livingTime = 3f;
    public Color initialColor = Color.white;
    public Color finalColor = Color.red;

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;
    private float _startingTime;
    private bool _returning;

    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //iniciar temporizador para cambiar color sprite desde su instancia durante su living time hasta su destroy
        _startingTime = Time.time;

        //destruir un objeto
        Destroy(this.gameObject, livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;

        _renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }

    private void FixedUpdate()
    {
        Vector2 movement = direction.normalized * speed;
        _rigidbody.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (_returning == true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("AddDamage");
            Destroy(this.gameObject);
        }

        if (_returning == false && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("AddDamage", damage);
            Destroy(this.gameObject);
        }
    }

    public void AddDamage()
    {
        _returning = true;
        direction = direction * -1f;
    }

}
