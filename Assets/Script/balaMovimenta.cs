using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaMovimenta : MonoBehaviour
{
    public float Velo = 20;
    public AudioClip MorteZumbi;

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition
            (GetComponent<Rigidbody>().position +
                transform.forward * Velo * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoDeColisao)

    {
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);
        switch (objetoDeColisao.tag)

        {
            case "Inimigo":
                MovimentaZumbi inimigo = objetoDeColisao.GetComponent<MovimentaZumbi>();
                inimigo.TomarDano(1);
                inimigo.ParticulaSangueZumbi(transform.position,rotacaoOpostaABala);
            break;

            case "ChefeDeFase":
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(1);
                chefe.ParticulaSangueZumbi(transform.position, rotacaoOpostaABala);
                break;
        }
        Destroy(gameObject);
    }
}
