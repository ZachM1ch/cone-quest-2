using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayClickAnim()
    {
        animator.Play("ClickButton");
    }
}
