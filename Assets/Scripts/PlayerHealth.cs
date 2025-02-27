using System;
using UnityEngine;

public class PlayerHealth : HealthSystem
{
    public override void OnDamageTaken()
    {
        if (!GetComponent<PlayerController>().isInvincible)
        {
            Die();
        }

    }

    private void Die()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.playerDie);
        GameManager.instance.OnGameEnd();

        GetComponent<Rigidbody2D>().useFullKinematicContacts = true;
        GetComponent<Collider2D>().isTrigger = true;

        Invoke("DelayShowlevelFail", .5f);
    }

    void DelayShowlevelFail()
    {
        UiManager.instance.ShowLevelFail();
    }
}
