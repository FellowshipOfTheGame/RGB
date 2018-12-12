using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAScroller : MonoBehaviour
{
    private BGAObjects acessView;

    private Transform[] views;

    //O Background é composto por três filhos, os chamarei de "views":
    private GameObject mainview;//O player está vendo ou já viu essa view por completo
    private GameObject nextview;//Essa é a proxima view, o player verá metade dela até que se torne a nova main
    private GameObject supportview; //O player nunca verá essa view, nela que as coisas fora do jogo acontecem

    public float scrollSpeed;//Velocidade que o Background corre
    public float limitY;//Limite para que uma view teleporte para cima
   
    private int cont;//contador para o numero do "views" que já passaram

    void Start()
    {
        cont = 1;
        views = transform.GetComponentsInChildren<Transform>();
        //inicializando as views
        mainview = views[1].gameObject;
        nextview = views[2].gameObject;
        supportview = views[3].gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Quando atingit o limite, ou seja, quando o player chegar na METADE da nextview, as coisas acontecem.
        if (transform.position.y < cont * limitY)
        {
            //As views trocarão umas com as outras.
            GameObject aux = mainview;
            mainview = nextview;
            nextview = supportview;
            supportview = aux;

            //devo teleportar a view que passou, ou seja a que o player NÃO ENXERGA mais, para cima
            supportview.transform.position = nextview.transform.position + Vector3.down * limitY;
            
            //Chamarei a funcao responsavel por gerar coisas novas no cenário (buracos na nuvem, nuvens, ...)
            acessView = supportview.GetComponent<BGAObjects>();
            if (acessView != null)
            {
                acessView.OnLoop();
            }
            cont++;
        }

        transform.position += Vector3.up * scrollSpeed;
        
    }
}
