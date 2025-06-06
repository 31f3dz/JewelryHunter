using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 3.0f; // X方向への移動距離
    public float moveY = 3.0f; // Y方向への移動距離
    public float times = 3.0f; // 何秒で移動するか
    public float wait = 1.0f; // 到着～折り返しのインターバル(停止時間)
    float distance; // 開始地点と移動予定地点の差
    float secondsDistance; // 1秒あたりの移動予定距離
    float framsDistance; // とある1フレームあたりにおける移動距離
    float movePercentage = 0; // 目的地までの移動進捗(割合)

    bool isCanMove = true; // 動いてOKかどうかのフラグ
    Vector3 startPos; // ブロックの初期位置
    Vector3 endPos; // 移動後の予定位置
    bool isReverse; // 方向反転フラグ

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCanMove)
        {
            return;
        }
        else
        {
            distance = Vector2.Distance(startPos, endPos);
            secondsDistance = distance / times;
            framsDistance = secondsDistance * Time.deltaTime;
            movePercentage += framsDistance / distance;

            if (!isReverse)
            {
                transform.position = Vector2.Lerp(startPos, endPos, movePercentage);
            }
            else
            {
                transform.position = Vector2.Lerp(endPos, startPos, movePercentage);
            }

            if (movePercentage >= 1.0f)
            {
                isCanMove = false;
                isReverse = !isReverse;
                movePercentage = 0;

                Invoke("Move", wait);
            }
        }
    }

    public void Move()
    {
        isCanMove = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

    // 移動範囲表示
    void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        //移動線
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        //スプライトのサイズ
        Vector2 size = GetComponent<SpriteRenderer>().size;
        //初期位置
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        //移動位置
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }
}
