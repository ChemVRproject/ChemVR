using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.EventSystems;

public class Target2 : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private Renderer myrenderer;
	private GameObject selected;
	private GameObject lastsel;

	private void Start()
    {
        myrenderer = GetComponent<Renderer>();
		selected = GameObject.Find("selectedatom");
		lastsel = GameObject.Find("lastselectedatom");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
		lastsel.GetComponent<TMP_Text>().text = myrenderer.name;
    }

	public void OnPointerClick(PointerEventData eventData)
    {
		int a = int.Parse(selected.GetComponent<TMP_Text>().text)+1;
		selected.GetComponent<TMP_Text>().text = a.ToString();	
	}
}
