using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbody;

    void Awake()
    {
        meuRigidbody = GetComponent<Rigidbody>();
    }
    public void Movimentar(Vector3 direcao, float velocidade)
    {
     meuRigidbody.MovePosition
     (meuRigidbody.position
     + direcao.normalized* velocidade * Time.deltaTime);
    }

    public void Rotacionar (Vector3 direcao)
    {
        Quaternion novarotacao = Quaternion.LookRotation(direcao);
        meuRigidbody.MoveRotation(novarotacao);
    }

    public void Morrer()
    {
        meuRigidbody.constraints = RigidbodyConstraints.None;
        meuRigidbody.velocity = Vector3.zero;
        //meuRigidbody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }

}
