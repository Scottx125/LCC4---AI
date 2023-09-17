using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private int _hitPoints = 50;

    public bool TakeHit(int damage){
        _hitPoints -= damage;
        bool isDead = _hitPoints <= 0;
        if (isDead) Die();
        return isDead;
    }

    private void Die() { 
        Destroy(gameObject);
    }
}
