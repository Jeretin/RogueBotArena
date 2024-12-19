using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BaseAIScript : MonoBehaviour
{
    private Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            //anim.SetTrigger("Jump");
        }
    }
}
