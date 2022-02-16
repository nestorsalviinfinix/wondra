using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCard : MonoBehaviour
{
    public Animator anim;
    private bool _zoom = false;
    public void Zoom()
    {
        _zoom = !_zoom;
        anim.SetBool("zoom", _zoom);
    }
}
