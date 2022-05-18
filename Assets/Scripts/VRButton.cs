using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
[RequireComponent(typeof(BoxCollider))]

public class VRButton : MonoBehaviour
{
    public Image buttonImage;
    public TMP_Text buttonText;

    private Color normalImageColor;
    [SerializeField]
    private Color pressedImageColor;
    private Color normalTextColor;
    [SerializeField]
    private Color pressedTextColor;
    [SerializeField]
    private bool ChangeColorOnPress;

    public UnityEvent ButtonPressed;
    public UnityEvent ButtonReleased;

    private Collider buttonCollider;

    private bool isPressed;

    public bool IsPresssed { get => isPressed; }

    private void Awake()
    {
        buttonCollider = GetComponent<Collider>();
        buttonCollider.isTrigger = true;
        if (ChangeColorOnPress == true)
        {
            if (buttonImage != null)
                normalImageColor = buttonImage.color;
            if (buttonText != null)
                normalTextColor = buttonText.color;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 HandDirection = other.bounds.center - transform.position;
        float angleDiff = Vector3.Angle(HandDirection, -transform.forward);
        if (angleDiff > 90f)
        {
            return;
        }
        isPressed = true;
        if (ChangeColorOnPress == true)
        {
            if (buttonImage != null)
                buttonImage.color = pressedImageColor;

            if (buttonText != null)
                buttonText.color = pressedTextColor;
        }
        ButtonPressed?.Invoke();



    }
    private void OnTriggerExit(Collider other)
    {
        isPressed = false;
        if (ChangeColorOnPress == true)
        {
            if (buttonImage != null)
                buttonImage.color = normalImageColor;

            if (buttonText != null)
                buttonText.color = normalTextColor;
        }


        ButtonReleased?.Invoke();
    }
}
