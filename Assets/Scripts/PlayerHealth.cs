using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth = 5;
    public RectTransform heartUI;
    public RectTransform gameOverMenu;
    public GameObject hordes;

    public RectTransform ScoreHUD;

    private int health;
    private float heartSize = 16f;
    private Animator _animator;
    private PlayerController _controller;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        health = totalHealth;
        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);
    }

    public void AddDamage(int amount)
    {
        health -= amount;

        StartCoroutine(VisualFeedback());

        if (health <= 0)
        {
            health = 0;
            gameObject.SetActive(false);
        }

        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        Debug.Log("Player got Damaged. His current health is " + health);
    }

    public void AddHealth(int amount)
    {
        health += amount;
        if (health > totalHealth)
        {
            health = totalHealth;
        }
        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        Debug.Log("Player got some life. His current health is " + health);
    }

    private IEnumerator VisualFeedback()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }

    private void OnEnable()
    {
        health = totalHealth;
        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        _renderer.color = Color.white;
    }
    private void OnDisable()
    {
        gameOverMenu.gameObject.SetActive(true);
        hordes.SetActive(false);
        _animator.enabled = false;
        _controller.enabled = false;
        _renderer.color = Color.white;

    }
}
