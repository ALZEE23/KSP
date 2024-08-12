using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;


public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    // public Rigidbody rb;
    public int combo;
    public bool serang;
    public float comboResetTime = 1.0f; // Waktu tunggu untuk reset combo
    private float lastAttackTime; // Waktu serangan terakhir
    private StarterAssets.StarterAssetsInputs _input;

    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _input = GetComponent<StarterAssets.StarterAssetsInputs>();
        combo = 0; // Mulai dari combo 0
    }

    // Update is called once per frame
    void Update()
    {
        Combos();
        ResetComboWithTime();
    }

    public void StartCombo()
    {
        serang = false;
        if (combo < 3)
        {
            combo++;

        }
    }

    public void FinishCombo()
    {
        serang = false;
        combo = 0; // Reset combo setelah stage terakhir

        // Pastikan semua trigger di-reset setelah combo selesai
        animator.ResetTrigger("1");
        animator.ResetTrigger("2");
        animator.ResetTrigger("3");
    }

    private void Combos()
    {
        if (_input.attack1 && !serang)
        {
            serang = true;
            lastAttackTime = Time.time;

            // Increment combo stage up to 3
            if (combo < 3)
            {
                combo++;
            }

            animator.SetTrigger("" + combo);

            // Coroutine untuk menunggu sebelum combo berikutnya
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        // Menunggu sampai animasi saat ini selesai
        yield return new WaitForSeconds(0.1f);

        _input.attack1 = false;

        // Jika combo telah mencapai tahap ketiga
        if (combo == 3)
        {
            FinishCombo();
        }
        else
        {
            serang = false; // Reset serangan agar bisa menerima input lagi
        }
    }

    private void ResetComboWithTime()
    {
        if (Time.time - lastAttackTime > comboResetTime && combo > 0)
        {
            combo = 0; // Reset combo setelah waktu tertentu
            serang = false;
        }
    }

}
