using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICompass : MonoBehaviour
{

    public RawImage compassImage;
    public Transform neerah;

    public GameObject iconPrefab;

    List<UIQuestMarker> questMarkers = new List<UIQuestMarker>();  //Entiendo que recoge en questMarquers una especie de array con todos los elementos que tienen la clase UIQuestMarker
    float compassUnit;  //No tengo claro que es pero su valor es 2,90 y pico

    public UIQuestMarker one, two, three;

    // Start is called before the first frame update
    void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        
        AddQuestMarker(one);
        AddQuestMarker(two);
        AddQuestMarker(three);

    }

    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(neerah.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach(UIQuestMarker marker in questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
        }
    }

    public void AddQuestMarker(UIQuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform); //instancio el icono como hijo de la imagen que contiene las letras
        marker.image = newMarker.GetComponent<Image>();  //llamo al componente image
        marker.image.sprite = marker.icon;  //Pongo la imagen que corresponde


        questMarkers.Add(marker);
    }
    
    Vector2 GetPosOnCompass (UIQuestMarker marker)
    {
        Vector2 neerahPos = new Vector2(neerah.transform.position.x, neerah.transform.position.z); //La posicion de Neerah
        Vector2 neerahFwd = new Vector2(neerah.transform.forward.x, neerah.transform.forward.z);  //No tengo claro que es

        float angle = Vector2.SignedAngle(marker.position - neerahPos, neerahFwd);

        return new Vector2(compassUnit + angle, 0f);
    }
    
}
