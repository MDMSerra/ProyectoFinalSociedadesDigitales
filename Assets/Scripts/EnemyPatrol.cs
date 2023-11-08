using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPatrol : MonoBehaviour
{
    public ParticleSystem dust;
    public float speed = 1f;
    public float wallAware = 0.5f;
    public LayerMask groundLayer;
    public float playerAware = 3f;
    public float aimingTime = 0.5f;
    public float shootingTime = 1.5f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Weapon _weapon;
    private AudioSource _audio;

    private Vector2 _movement;
    private bool _facingRight;

    private bool _isAttacking;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (transform.localScale.x < 0f)
        {
            _facingRight = false;
        }
        else if (transform.localScale.x > 0f)
        {
            _facingRight = true;
        }
    }

    private void Update()
    {

        Vector2 direction = Vector2.right;

        if (_facingRight == false)
        {
            direction = Vector2.left;
        }

        if (_isAttacking == false)
        {
            if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
            {
                Flip();
                CreateDust();
            }
        }
    }

    private void FixedUpdate()
    {
        float horizontalVelocity = speed;
        if (_facingRight == false)
        {
            horizontalVelocity = horizontalVelocity * -1f;
        }

        if (_isAttacking)
        {
            horizontalVelocity = 0f;
        }


        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", _rigidbody.velocity == Vector2.zero);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isAttacking == false && collision.CompareTag("Player"))
        {
            StartCoroutine(AimAndShoot());
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    private IEnumerator AimAndShoot()
    {

        _isAttacking = true;

        yield return new WaitForSeconds(aimingTime);

        _animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(shootingTime);

        _isAttacking = false;
        //speed = speedBackup; //esta linea tampoco, desde que añadimos que el enemigo pueda ser eliminado
    }

    void CanShoot()
    {
        if (_weapon != null)
        {
            _weapon.Shoot();
            _audio.Play();
        }
    }


    void CreateDust()
    {
        dust.Play();
    }

    private void OnEnable()
    {
        _isAttacking = false;
    }

    private void OnDisable()
    {
        StopCoroutine(AimAndShoot());
        _isAttacking = false;
    }
}
