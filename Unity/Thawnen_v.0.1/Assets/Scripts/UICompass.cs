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
    float compassUnit;

    public UIQuestMarker mon0, mon1, mon2, mon3, mon4, mon5, estatua0, estatua1;


    //Para la variacion de la escala de los iconos en funcion de la distancia
    public float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f; //A cuantas unidades en la imagen de la brújula equivale un grado (creo)

        //Markers de monolitos
        AddQuestMarker(mon0);
        AddQuestMarker(mon1);
        AddQuestMarker(mon2);
        AddQuestMarker(mon3);
        AddQuestMarker(mon4);
        AddQuestMarker(mon5);

        //Markers de estatuas
        AddQuestMarker(estatua0);
        AddQuestMarker(estatua1);

        //Maxima distancia a la que detecta QuestMarkers
        maxDistance = 500f;
    }

    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(neerah.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach(UIQuestMarker marker in questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            //variacion de la escala de los iconos con la distancia
            float dist = Vector2.Distance(new Vector2(neerah.transform.position.x, neerah.transform.position.z), marker.position);
            float scale = 0f;

            if(dist < maxDistance)
            
                scale = 1f - (dist / maxDistance);
            
            marker.image.rectTransform.localScale = Vector3.one * scale;

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

        return new Vector2(compassUnit * angle, 0f);
    }
    
}
