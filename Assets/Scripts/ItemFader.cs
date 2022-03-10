using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    /// <summary>
    /// ²»Í¸Ã÷
    /// </summary>
    public void FadeIn()
    {
        Color targetColor = new Color(1, 1, 1, 1);
        spriteRenderer.DOColor(targetColor, Settings.fadeDuration);
    }

    /// <summary>
    /// °ëÍ¸Ã÷
    /// </summary>
    public void FadeOut()
    {
        Color targetColor = new Color(1, 1, 1, Settings.targetAlpha);
        spriteRenderer.DOColor(targetColor,Settings.fadeDuration);
            
    }
}
