using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1,1,1,1);
    [SerializeField] Color32 noPackageColor = new Color32(1,1,1,1);

    
    [SerializeField] float destroyDelay = 0.5f;
    bool hasPackage;

    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   void OnCollisionEnter2D(Collision2D other) {
       Debug.Log("Oops");
   }

   private void OnTriggerEnter2D(Collider2D other) {
       if(other.tag == "Package" && !hasPackage){
           Debug.Log("Package Up");
           hasPackage = true;   
           spriteRenderer.color = hasPackageColor;
           Destroy(other.gameObject, destroyDelay); // Destroy удаляет полностью игровой объект если мы с ним взаимподействуем.
       }
       else if(other.tag == "Customer" && hasPackage){
           Debug.Log("Customer Up");
           hasPackage = false;
           spriteRenderer.color = noPackageColor;
       }
   }
}
