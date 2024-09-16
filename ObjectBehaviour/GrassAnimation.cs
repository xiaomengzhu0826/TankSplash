
using System.Collections;
using UnityEngine;

public class GrassAnimation : MonoBehaviour
{
    
    private float enterduration = 0.1f; 
    private float exitduration = 0.4f;
    private float initYScale;
    private void Start()
    {
        initYScale=transform.localScale.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ScaleOverTime(enterduration,true));
    }

    private void OnTriggerExit(Collider other)
    {
       StartCoroutine(ScaleOverTime(exitduration,false));
    }

    private IEnumerator ScaleOverTime(float duration,bool isEnter)
    {
   
        // 获取当前缩放
        Vector3 initialScale = transform.localScale;
        // 计算目标缩放值
        Vector3 targetScale = isEnter ? new Vector3(initialScale.x, 0.001f, initialScale.z) : new Vector3(initialScale.x, initYScale, initialScale.z);

        // 当前时间
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 计算经过的时间比例
            float t = elapsedTime / duration;
            // 使用Lerp计算新的缩放值
            float newYScale = Mathf.Lerp(initialScale.y, targetScale.y, t);
            // 设置新的缩放值（只改变 Y 轴）
            transform.localScale = new Vector3(initialScale.x, newYScale, initialScale.z);

            // 更新经过的时间
            elapsedTime += Time.deltaTime;
            // 等待下一帧
            yield return null;
        }

        // 确保最后完全达到目标值
        transform.localScale = targetScale;
       
    }

}
