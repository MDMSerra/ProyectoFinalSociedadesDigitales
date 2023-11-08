using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    static public long score = 0;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI scoreGameOver;

    private bool _isAttacking;
    private Animator _animator;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }
        if (score == 0)
        {
            scoreUI.text = "Kill some enemies!";
            scoreGameOver.text = "Score: " + score.ToString();
        }
        else
        {
            scoreUI.text = "Score: "+score.ToString();
            scoreGameOver.text = "Score: " + score.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking)
        {
            if (collision.CompareTag("Enemy"))
            {
                score += 100;
            }
            if (collision.CompareTag("Enemy") || collision.CompareTag("BigBullet"))
            {
                
                collision.SendMessageUpwards("AddDamage");
                
            }
        }
    }

    public void Reset()
    {
        score = 0;
        scoreGameOver.text = "Score: " + score.ToString();
    }
    private void OnEnable()
    {
        Reset();
    }

}
