    // MoveTo.cs
    using UnityEngine;
    using System.Collections;
    
    public class MoveTo : MonoBehaviour {
       
       public Transform goal;
       
		void Start () {
          	NavMeshAgent agent = GetComponent<NavMeshAgent>();
          	agent.destination = goal.position;
			agent.autoBraking = false;
       	}

<<<<<<< HEAD
	void Update() {
	  	NavMeshAgent agent = GetComponent<NavMeshAgent>();
      	agent.destination = goal.position; 
=======
		void Update() {
	  		NavMeshAgent agent = GetComponent<NavMeshAgent>();
          	agent.destination = goal.position; 
>>>>>>> efaab94f0d6a0bc9b8a15661bdc0a40b4913edd1
		}
    }