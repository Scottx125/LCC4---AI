using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private int _hitPoints = 50;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _sheepAudio;
    [SerializeField]
    private AudioClip _bleat;

    public bool TakeHit(int damage){
        _hitPoints -= damage;
        _animator.SetBool("Attacked", true);
        _sheepAudio.PlayOneShot(_bleat);
        _animator.SetBool("Attacked", false);
        bool isDead = _hitPoints <= 0;
        if (isDead) {
            _animator.SetBool("Dead", true);
            Die();
        }
        return isDead;
    }

    private void Die() { 
        Destroy(gameObject);
    }
}
