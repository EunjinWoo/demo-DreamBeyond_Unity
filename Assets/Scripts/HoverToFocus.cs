using UnityEngine;

public class HoverToFocus : MonoBehaviour
{
    public Transform cameraTransform; // 이동할 카메라의 Transform
    public Transform focusPoint; // 이동할 시점의 위치
    public float transitionSpeed = 2f; // 카메라 이동 속도
    private bool isHoveringTriggered = false; // 한번이라도 호버되었는지 확인하는 플래그
    private Vector3 originalPosition; // 초기 카메라 위치 저장
    private Quaternion originalRotation; // 초기 카메라 회전 저장
    private CameraOrbit cameraOrbitScript;

    void Start()
    {
        // 카메라의 원래 위치와 회전 값 저장
        originalPosition = cameraTransform.position;
        originalRotation = cameraTransform.rotation;

        // CameraOrbit 스크립트 가져오기
        cameraOrbitScript = cameraTransform.GetComponent<CameraOrbit>();
        if (cameraOrbitScript != null)
        {
            cameraOrbitScript.enabled = true; // 초기에는 CameraOrbit이 활성화되도록 설정
        }
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse entered the object.");
        isHoveringTriggered = true; // 한번이라도 호버되면 true로 설정
        if (cameraOrbitScript != null)
        {
            cameraOrbitScript.enabled = false; // Hover 시작 시 CameraOrbit 비활성화
        }
    }

    void Update()
    {
        if (isHoveringTriggered)
        {
            // 카메라를 focusPoint 위치로 서서히 이동
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, focusPoint.position, transitionSpeed * Time.deltaTime);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, focusPoint.rotation, transitionSpeed * Time.deltaTime);
        }
        else
        {
            // CameraOrbit이 작동 중일 때 원래 위치로 돌아갈 필요 없음
            // CameraOrbit이 초기부터 작동하도록 설정됨
        }
    }
}
