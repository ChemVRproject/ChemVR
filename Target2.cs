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

public class Target2 : MonoBehaviour, IPointerEnterHandler,  IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Renderer myrenderer;
	private Material Norm;	
	public Material FresnelM;
	private GameObject Inform;
	//private TMPro.TMP_Text newInform;

	private void Start()
    {
        myrenderer = GetComponent<Renderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
	Norm = myrenderer.material;
	myrenderer.material = FresnelM;
	
    }

	public void OnPointerDown(PointerEventData eventData)
    {
		//newInform = Instantiate<TMPro.TMP_Text>(Inform);
		Norm = myrenderer.material;
		myrenderer.material = FresnelM;
		Inform = GameObject.Find("InformText");
		Inform.GetComponent<TMP_Text>().text = "Атом:" + '\n' + myrenderer.name + '\n' + " Координаты:" + '\n' + myrenderer.transform.position;
		
	}
	
   public void OnPointerUp(PointerEventData eventData)
    {
        myrenderer.material = Norm;
		//Destroy(newInform);
    }
	
	public void OnPointerExit(PointerEventData eventData)
    {
        myrenderer.material = Norm;
		//Destroy(newInform);
    }
}