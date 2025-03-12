using UnityEngine;

public class MeleeSwing : Hazard
{
    [SerializeField] Collider swingCollider;
    /// <summary>
    /// 0 results in a one frame swing.
    /// </summary>
    public float swingTime = 0;
    float swingTimer = 0;
    public void StartSwing()
    {
        gameObject.SetActive(true);
        swingCollider.enabled = true;
        swingTimer = 0;
    }
    
    private void FixedUpdate()
    {
        if(swingTimer > swingTime)
        {
            gameObject.SetActive(false);
            return;
        }
        swingTimer += Time.fixedDeltaTime;
    }
    
    protected void OnTriggerStay(Collider other)
    {
        pushForce = transform.forward * push;
        base.OnTriggerEnter(other);
        //swingCollider.enabled = false;
    }
    protected override void OnTriggerEnter(Collider other) {}
}
