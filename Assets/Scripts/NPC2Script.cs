using UnityEngine;
using Pathfinding;

public class NPC2Script : MonoBehaviour
{
    public AIPath aiPath;
    public Animator animator;

    void Start()
    {
        aiPath = GetComponentInParent<AIPath>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if(aiPath.reachedEndOfPath) {
            animator.SetBool("IsOnPhone", true);
        }
    }
}
