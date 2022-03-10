using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SwitchBounds : MonoBehaviour
{
    CinemachineConfiner confiner;

    //TODO: �г���ʱ����
    private void Start()
    {
        SwitchConfinerShape();
    }
    private void SwitchConfinerShape()
    {
        PolygonCollider2D  confiner2D=GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        confiner =GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = confiner2D;
        confiner.InvalidatePathCache();
    } 
}
