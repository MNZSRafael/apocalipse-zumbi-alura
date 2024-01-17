using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradordeZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaGerador = 3;
    private float distanciaDoJogadorParaGeracao = 25;
    private GameObject jogador;
    private int quantidadeMaximaZumbis = 3;
    private int quantidadeZumbisAtual;
    private float tempoProximoAumentoDeDificuldade = 45;
    private float contadorDeAumentarDificuldade;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;
        for (int i = 0; i < 1; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }
    void Update()
    {
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao;

        if (possoGerarZumbisPelaDistancia == true && quantidadeZumbisAtual< quantidadeMaximaZumbis)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        if(Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
        {
            quantidadeMaximaZumbis++;
            contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaGerador);
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while (colisores.Length > 0 )
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
        MovimentaZumbi zumbi =Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<MovimentaZumbi>();
        zumbi.meuGerador = this;
        quantidadeZumbisAtual++;

    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaGerador;
        posicao += transform.position;
        posicao.y = 0;
        return posicao;

    }

    public void DiminuirQtdaZumbiJogo ()
    {
        quantidadeZumbisAtual--;
    }
}
