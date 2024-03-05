using UnityEngine;
 using System.Collections;
 
 public class ParticleAttractor : MonoBehaviour {
     
     [SerializeField]
     private Transform attractorTransform;
 
     private ParticleSystem particleSystem;
     private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];
 
     public void Start ()
     {
         particleSystem = GetComponent<ParticleSystem> ();
     }
 
     public void Update()
     {
         if (GetComponent<ParticleSystem>().isPlaying) {
             int length = particleSystem.GetParticles (particles);
             Vector3 attractorPosition = attractorTransform.position;
 
             for (int i=0; i < length; i++) {
                     particles [i].position = particles [i].position + (attractorPosition - particles [i].position)* 2*Time.deltaTime;
             }
             particleSystem.SetParticles (particles, length);
         }
 
     }
 }