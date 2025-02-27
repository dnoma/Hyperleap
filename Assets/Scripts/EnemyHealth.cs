using System;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    bool isDead;

    public override void OnDamageTaken()
    {
        Die();
    }

    private void Die()
    {
        if(isDead) return;

        SoundManager.instance.PlaySound(SoundManager.instance.enemyDie);

        GetComponent<EnemyController>().isDead = true;
        GetComponent<EnemyController>()._animation.Die();


        isDead = true;

        GameManager.instance.CoinCollected();

        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());
        Invoke("DisableSelf", 1f);
    }

    void DisableSelf() => Destroy(gameObject);
}
