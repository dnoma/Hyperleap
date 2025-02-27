using UnityEngine;
using UnityEngine.U2D.Animation;

public class Goomba : MonoBehaviour
{
    public SpriteLibraryAsset enemySprite;
    public SpriteLibrary bodySprite;

    private void Awake()
    {
        bodySprite.spriteLibraryAsset = enemySprite;
    }
}