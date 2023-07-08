using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float dieForce = 10f;
    public float maxHealth;
    public float currentHealth;
    public float blinkDuration;
    
    
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Ragdoll ragdoll;
    void Start()
    {
        currentHealth = maxHealth;
        ragdoll = GetComponent<Ragdoll>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        var rigiBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigid in rigiBodies)
        {
            Hitbox hitbox = rigid.gameObject.AddComponent<Hitbox>();
            hitbox.health = this;
            hitbox.rb = rigid;
        }
    }

   public void TakeDamage(float amount, Vector3 direction, Rigidbody rb)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die(direction, rb);
        }
    }
    private IEnumerator EnemyFlash()
    {
        skinnedMeshRenderer.material.EnableKeyword("_EMISSTION");
        yield return new WaitForSeconds(blinkDuration);
        skinnedMeshRenderer.material.DisableKeyword("_EMISSTION");
        StopCoroutine(nameof(EnemyFlash));
    }
    private void Die(Vector3 direction, Rigidbody rb)
    {
        ragdoll.ActiveRaggdoll();
        direction.y = 1f;
        ragdoll.ApplyForce(direction * dieForce, rb);
        Destroy(this.gameObject, 3f);
    }
}
