using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource GameBG, SfxSource;

    [SerializeField] internal AudioClip buttonClick, coinCollect, enemyDie, playerDie, playerJump, levelComplete, playerPowerUp;


    protected override void Awake()
    {
        base.Awake();
    }

    public void PlaySound(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }

    public void PlayBG()
    {
        GameBG.Play();
    }
    
    public void PasueBg()
    {
        GameBG.Pause();
    }
}