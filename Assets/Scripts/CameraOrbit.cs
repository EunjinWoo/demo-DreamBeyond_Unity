using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // 회전할 중심이 되는 오브젝트
    public float rotationSpeed = 10f; // 회전 속도
    public float distance = 5f; // 타겟과의 거리
    public float verticalAngle = 45f; // 타겟을 내려다보는 각도

    private Vector3 offset; // 초기 오프셋을 저장할 벡터

    void Start()
    {
        // 초기 오프셋 설정: 타겟과의 거리 및 내려다보는 각도 적용
        if (target != null)
        {
            // 수평 거리와 수직 거리 계산
            float horizontalDistance = distance * Mathf.Cos(verticalAngle * Mathf.Deg2Rad);
            float height = distance * Mathf.Sin(verticalAngle * Mathf.Deg2Rad);
            
            offset = new Vector3(0, height, -horizontalDistance); // 타겟을 내려다보는 위치로 설정
        }
    }

    void Update()
    {
        if (target != null)
        {
            // 타겟을 기준으로 회전
            transform.position = target.position + offset;
            transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);

            // 현재 위치를 기준으로 다시 offset을 업데이트하여 각도를 유지
            offset = transform.position - target.position;
            
            // 타겟을 바라보도록 설정
            transform.LookAt(target);
        }
    }
}
