using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Mario : MonoBehaviour
{
    public SpriteLibraryAsset playerSprite;
    public SpriteLibrary bodySprite;

    private void Awake()
    {
        bodySprite.spriteLibraryAsset = playerSprite;
    }

}