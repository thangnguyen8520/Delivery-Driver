﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] float boostSpeed = 8f;
    [SerializeField] float slowSpeed = 4f;

    private bool isBoosted = false;
    private bool isSlowed = false;

    private void Update()
    {
        float currentMoveSpeed = CalculateCurrentMoveSpeed();

        float rotateInput = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        float moveInput = Input.GetAxis("Vertical") * currentMoveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -rotateInput);
        transform.Translate(0, moveInput, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedUp") && !isBoosted)
        {
            ActivateBoost();
            Debug.Log("Boost");
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("SlowDown") && !isSlowed)
        {
            SlowDown();
            Debug.Log("SlowDown");
            Destroy(collision.gameObject);
        }
    }

    private void ActivateBoost()
    {
        isBoosted = true;
        StartCoroutine(ResetBoost());
    }

    private void SlowDown()
    {
        isSlowed = true;
        StartCoroutine(ResetSlow());
    }

    private float CalculateCurrentMoveSpeed()
    {
        if (isBoosted && !isSlowed)
        {
            return boostSpeed;
        }
        else if (isSlowed && !isBoosted)
        {
            return slowSpeed;
        }
        else
        {
            return moveSpeed;
        }
    }

    private IEnumerator ResetBoost()
    {
        yield return new WaitForSeconds(4f); 
        isBoosted = false;
    }

    private IEnumerator ResetSlow()
    {
        yield return new WaitForSeconds(4f); 
        isSlowed = false;
    }
}
