using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarefa : MonoBehaviour
{
    // Pega os waypoints e coloca eles em uma lista (ARRAY)
    public GameObject[] waypoints;
    int currentWP = 0;
    // Atribuição das variáveis que irão influenciar na movimentação
    float speed = 1.0f;
    float accuracy = 1.0f;
    float rotSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        // Dinamicamente irá puxar todos os gameobjects da cena para a lista, com um requirimento : possuir a tag waypoint no gameobject
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }
    // Um update que será chamado após a conclusão dos 60 ticks do Update normal, o Late update tambem funciona como um update, porém os seus 60 ticks só funcionam
    // após o update.
    void LateUpdate()
    {
        //Se a lista estiver vazia, retorne ela
        if (waypoints.Length == 0) return;
        // LookAtGoal é uma variável do tipo vector 3, que irá receber como valor a POSIÇÃO do primeiro waypoint(currentWP)
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x,this.transform.position.y,waypoints[currentWP].transform.position.z);
        // Direction é uma variável do tipo vector 3, que irá receber como valor a DIREÇÃO do primeiro waypoint, a sua posição é dada através do LookAtGoal
        Vector3 direction = lookAtGoal - this.transform.position;
        // Esse método rotation, faz a rotação do nosso objeto através de um sphereLerp que baseado no time.deltatime ele irá rotacionar até a direção inserida no método.
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * rotSpeed);
        // Se o comprimento do vetor direção for menor que precisão... = ele chegou no ponto
        if (direction.magnitude < accuracy)
        {
            // Como ele chega no ponto, ele deve atualizar o waypoint, neste caso incrementando 1 no index contador
            currentWP++;
            // Caso o index contador alcançar o limite da lista, ele deve zerar o index, fazendo com que recomeçe
            if (currentWP >= waypoints.Length)
            {
                // reinicia o index contador
                currentWP = 0;
            }
        }
        // Método que irá fazer a movimentação do nosso gameobject baseado em sua velocidade e baseado no tempo.
        // Neste caso irá movimentar através do transform, utilizando o método translate.
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}