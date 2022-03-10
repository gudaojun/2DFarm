using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerItemFade : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        var itemFaders = collision.GetComponentsInChildren<ItemFader>();
        if (itemFaders.Length > 0)
        {
            foreach (var item in itemFaders)
            {
                item.FadeIn();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var itemFaders=collision.GetComponentsInChildren<ItemFader>();
        if (itemFaders.Length>0)
        {
            foreach (var item in itemFaders)
            {
                item.FadeOut();
            }
        }


    }



}
