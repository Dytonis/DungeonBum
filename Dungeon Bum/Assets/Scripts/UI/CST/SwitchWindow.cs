using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace Assets.Scripts.UI.CST
{
    public class SwitchWindow : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public bool SwitchesToEquip;
        public GameObject EquipScreen;
        public GameObject EquipDot;
        public GameObject TreeScreen;
        public GameObject TreeDot;

        public void OnPointerUp(PointerEventData eventData)
        {
            if (SwitchesToEquip)
            {
                if (!EquipScreen.activeSelf)
                {
                    EquipDot.SetActive(true);
                    EquipScreen.SetActive(true);
                    TreeDot.SetActive(false);
                    TreeScreen.SetActive(false);
                    AudioSource.PlayClipAtPoint(SoundList.UI.MenuOpenLow, Camera.main.transform.position);
                }
            }
            else
            {
                if (!TreeScreen.activeSelf)
                {
                    TreeDot.SetActive(true);
                    TreeScreen.SetActive(true);
                    EquipDot.SetActive(false);
                    EquipScreen.SetActive(false);
                    AudioSource.PlayClipAtPoint(SoundList.UI.MenuOpenLow, Camera.main.transform.position);
                }
            }
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //throw new NotImplementedException();
        }
    }
}
