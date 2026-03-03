using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;

    public Transform[] enemyRoad; //エネミーが通る場所
    private int currentPoint = 0; //今何番目の折り返し地点か
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; //目的地に近づいても速度を落とさない
        GotoNextPoint();
    }

    public void GotoNextPoint()
    {
        //地点がなにも設定されていないときにreturn
        if (enemyRoad.Length == 0)
        {
            return;
        }

        //agentが現在設定された目的地へ行くよう設定
        agent.destination = enemyRoad[currentPoint].position;

        //配列内の次の位置を目標地点に設定
        //必要ならば出発地点に戻る
        currentPoint = (currentPoint + 1) % enemyRoad.Length;
    }

    private void Update()
    {
        //道順の計算はしてない && あと少しで着きそうなら
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        //反転処理
        if (agent.velocity.x > 0.1f)
        {
            enemyObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (agent.velocity.x < -0.1f)
        {
            enemyObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
