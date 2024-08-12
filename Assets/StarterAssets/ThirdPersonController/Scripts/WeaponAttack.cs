using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private Collider col;
    public Animator EnemyAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Enemy")){
            EnemyAi enemy = other.GetComponent<EnemyAi>();
            EnemyAnimator = other.GetComponent<Animator>();

            if(EnemyAnimator){
                EnemyAnimator.SetTrigger("hit");
                enemy.Health -= 1;
                StartCoroutine(End());
            }

            Debug.Log("kena");
        }
    }

    IEnumerator End(){
        yield return null;
        EnemyAnimator.ResetTrigger("hit");
    }
}
