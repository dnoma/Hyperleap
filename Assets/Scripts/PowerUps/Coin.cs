using Unity.VisualScripting;
using UnityEngine;

public class Coin : PowerUp
{

    private void Awake()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.coinCollect);
    }

    protected override void Update()
    {
        base.Update();
        if (isMoving)
        {
            GameManager.instance.CoinCollected();
            Destroy(gameObject);
        }
    }

}
