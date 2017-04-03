using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[Range(0,100)]
	public float Health = 100;

	public float Speed = 0.2f;
	public float SeeRange = 3f;
    public float RotationSpeed = 10f;

    public GameObject ExplosionPrefab;
	private Transform TargetDestination = null;

	private NavMeshAgent agent;
    private Animator animator;

	void Awake(){
		this.agent = GetComponent<NavMeshAgent>();
        this.animator = GetComponent<Animator>();	
	}
	
	void Update () {      
        if(TargetDestination != null){
            MoveTowards(TargetDestination);
            
            if (IsInMeleeRangeOf(TargetDestination)) {
                RotateTowards(TargetDestination);
            }
        }  
        
     }

    public void SetTargetDestination(Transform destination){
        this.TargetDestination = destination;
        this.animator.SetBool("walking", true);
    }
	public void TakeDamage(float amount){
		this.Health = this.Health > 0 ? this.Health -= amount : 0;
	}

	private bool IsInMeleeRangeOf (Transform target) {
         float distance = Vector3.Distance(transform.position, target.position);
         return distance < SeeRange;
     }
     
     private void MoveTowards (Transform target) {
         if(target != null){
            agent.SetDestination(target.position);
		    agent.speed = this.Speed;
         }
     }
         
     private void RotateTowards (Transform target) {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
     }

     public void Die(){
        if(ExplosionPrefab != null){
            var explosion = (GameObject)Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(explosion, 15.0f);
        }
         Destroy(this.gameObject);
     }

     public void OnCollisionEnter(Collision collision){
         var other = collision.contacts[0].otherCollider;
         if(other.gameObject.tag.Equals("Bullet")){
             this.TakeDamage(50);
         }
     }
}
