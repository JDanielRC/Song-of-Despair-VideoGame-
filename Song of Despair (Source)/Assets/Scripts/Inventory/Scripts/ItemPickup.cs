using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public Item item;   // Item to put in the inventory if picked up
    private ReproductorExterno sonidoManager;


    private void Start()
    {
        sonidoManager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<ReproductorExterno>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PickUp();
        }
    }
    // Pick up the item
    public void PickUp()
    {
        //Debug.Log("Picking up " + item.name);
        bool loRecogio = Inventory.instance.Add(item);   // Add to inventory
        if (loRecogio)
        {
            sonidoManager.PlayPickupSound();
            Destroy(gameObject);
        }
    }

}
