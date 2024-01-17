using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject ChefePrefab;
    private ControlaInterface scriptControlaInterface;
    public Transform[] PosicoesPossiveisDeGeracao;
    private Transform jogador;

    private void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindAnyObjectByType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            Vector3 posicaoDeCriacaoChefe = PosicaoMaisDistanteDoJogador();
            Instantiate(ChefePrefab, posicaoDeCriacaoChefe, Quaternion.identity);
            scriptControlaInterface.AparicaoChefe();
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    Vector3 PosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoMaisDistante = Vector3.zero;
        float maiorDistancia = 0;
        foreach (Transform posicao in PosicoesPossiveisDeGeracao)
        {
            float distanciaDoJogador = Vector3.Distance(posicao.position, jogador.position);
            if (distanciaDoJogador > maiorDistancia)
            {
                maiorDistancia = distanciaDoJogador;
                posicaoMaisDistante = posicao.position;
            }

        }

        return posicaoMaisDistante;
    }

}
