using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Entity.Items;

namespace Assets.Scripts.Character
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> CurrentInventory = new List<Item>();
        public int MaxInventory = 16;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns>The amount not able to fit in the inventory.</returns>
        public int AddItemIfAble(Item i)
        {
            if(i.MaxStacks > 1)
            {
                List<Item> finds = CurrentInventory.FindAll(x => x.ImpericalName == i.ImpericalName);
                if(finds.Count >= 1)
                {
                    foreach(Item f in finds)
                    {
                        if(f.Count + i.Count < i.MaxStacks)
                        {
                            //all can fit in a single stack
                            f.Count++;
                            UpdateInventory();
                            return 0;
                        }
                        else if (f.Count < i.MaxStacks)
                        {
                            //the stack is not full, we need to make another!
                            //set the i stack to the difference
                            i.Count = (i.MaxStacks - f.Count);
                            //fill the first stack
                            f.Count = i.MaxStacks;
                            if(CurrentInventory.Count < MaxInventory)
                            {
                                //we can add another
                                CurrentInventory.Add(i);
                                i.InventoryPosition = CurrentInventory.Count - 1;
                                UpdateInventory();
                                return 0;
                            }
                            else
                            {
                                //partial add, the difference must be thrown out
                                UpdateInventory();
                                return (i.MaxStacks - f.Count);
                            }
                        }
                    }
                    //couldnt add, all stacks full
                    if(CurrentInventory.Count < MaxInventory)
                    {
                        CurrentInventory.Add(i); //needed to add a new stack
                        i.InventoryPosition = CurrentInventory.Count - 1;
                        UpdateInventory();
                        return 0;
                    }
                    else
                    {
                        //inv full
                        UpdateInventory();
                        return i.Count;
                    }
                }
                else
                {
                    //no other stacks
                    CurrentInventory.Add(i);
                    i.InventoryPosition = CurrentInventory.Count - 1;
                    UpdateInventory();
                    return 0;
                }
            }
            else
            {
                //not stackable
                if(CurrentInventory.Count < MaxInventory)
                {
                    CurrentInventory.Add(i);
                    i.InventoryPosition = CurrentInventory.Count - 1;
                    UpdateInventory();
                    return 0;
                }
                else
                {
                    //no room
                    UpdateInventory();
                    return i.Count;
                }
            }
        }

        public Item RemoveItem(Item i)
        {
            Item item = CurrentInventory[CurrentInventory.IndexOf(i)];
            CurrentInventory.RemoveAt(CurrentInventory.IndexOf(i));
            UpdateInventory();
            return item;
        }

        public void UpdateInventory()
        {
            GameObject equip = GameObject.FindGameObjectWithTag("Equipper");
            if (equip)
            {
                InventoryScreen screen = equip.GetComponent<InventoryScreen>();
                if (screen)
                {
                    screen.SetInventoryVisually(this);
                }
            }
        }
    }
}
