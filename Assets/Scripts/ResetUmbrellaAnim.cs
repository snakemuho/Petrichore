using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUmbrellaAnim : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ResetAnim()
    {
        anim.SetBool("attack", false);
        anim.SetBool("air", false);

    }
}
