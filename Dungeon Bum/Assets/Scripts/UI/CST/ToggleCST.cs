using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;
using Assets.Scripts.Entity.Items;

public class ToggleCST : MonoBehaviour
{

    public GameObject CST;
    public AudioClip MenuOpen;
    public InventoryScreen Screen;
    public AudioClip MenuClosed;
    public Inventory Inv;

	void Update()
    {
        if(Input.GetButtonDown("Menu"))
        {
            if(CST.activeSelf)
            {
                Screen.Reset();
                CST.SetActive(false);
                AudioSource.PlayClipAtPoint(MenuClosed, Camera.main.transform.position);
            }
            else
            {
                CST.SetActive(true);
                AudioSource.PlayClipAtPoint(MenuOpen, Camera.main.transform.position);
                if (GameObject.FindGameObjectWithTag("Equipper").GetComponent<InventoryScreen>())
                {
                    InventoryScreen screen = GameObject.FindGameObjectWithTag("Equipper").GetComponent<InventoryScreen>();
                    screen.SetInventoryVisually(Inv);
                }
            }
        }
    }
}
