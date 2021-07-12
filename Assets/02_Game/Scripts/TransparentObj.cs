using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//透明にするスクリプト
public class TransparentObj : MonoBehaviour
{
    public enum TransparentTypeObj　//透明にするタイプ
    {
        alwaysTransparent,          //通れない壁
        sometimesTransparent,       //透明時に接触可能
        sometimesTransparentcollider//透明時に接触不可
    }
    //　透明オブジェクトのタイプ
    [SerializeField]
    private TransparentTypeObj transparenttypeobj;
    //　見た目表示コンポーネント
    private MeshRenderer mesh;

    //透明にする間隔が欲しい場合はこれ↓
    [SerializeField]
    private float transparentTime = 2.0f;//二秒毎
    //透明になってからの時間
    private float nowTime = 0.0f;
    //時間ごとに透明にするオブジェクトコライダー
    private Collider col;

    // プレイヤーが落ちてから最初の場所に戻るまでの時間
    [SerializeField] private float _WaitSeconds = 0;

    // カメラオブジェクト取得用
    private GameObject _Camera;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();//メッシュ取得
        col = GetComponent<Collider>();//コライダー取得
        if (transparenttypeobj == TransparentTypeObj.alwaysTransparent)
        {        
            //透明なら表示をしない
            //当たり判定は欲しい為コリダーはのこす
            mesh.enabled = false;
        }

        _Camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        

        //常に透明な時は何もしない
        if (transparenttypeobj == TransparentTypeObj.alwaysTransparent)
        {
            return;
        }
        //時間測定
        nowTime += Time.deltaTime;

        if (nowTime >= transparentTime)
        {
            mesh.enabled = !mesh.enabled;

            if (transparenttypeobj == TransparentTypeObj.sometimesTransparentcollider)
            {
                col.enabled = !col.enabled;
            }
            nowTime = 0.0f;
        }
    }

    private IEnumerator DelayWarp(float waitSeconds, Collider col)
    {
        yield return new WaitForSeconds(waitSeconds);
        GameObject.Find("WorldMgr").GetComponent<WorldMgr>().SetWorldState(WorldMgr.WorldState.STATE_FRONT);    // 表ステージに変更
        col.GetComponent<PlayerController>().SetState(PlayerController.CharactorState.STATE_WARP);              // プレイヤーをワープ状態にする
        _Camera.GetComponent<CameraSetting>().FollowEnable(true);                                               // カメラの追従を有効化
    }


    // 衝突判定
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")// 衝突したオブジェクトが"Player"タグの着いたオブジェクトだった場合
        {
            _Camera.GetComponent<CameraSetting>().FollowEnable(false);  // カメラの追従を無効化
            StartCoroutine(DelayWarp(_WaitSeconds, col));
        }
    }
}
