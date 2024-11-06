using UnityEngine;

public class HoverToFocus : MonoBehaviour
{
    public Transform cameraTransform; // 이동할 카메라의 Transform
    public Transform focusPoint; // 이동할 시점의 위치
    public Transform laptopObject; // 노트북 오브젝트의 Transform
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

            // 화면 밖을 클릭하면 카메라가 원래 상태로 돌아가도록 설정
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) // 마우스 왼쪽 클릭 또는 터치 입력
            {
                Debug.Log("Click or touch detected."); // 클릭 또는 터치가 감지되었음을 로그로 확인
                // 클릭한 위치가 노트북 화면이 아닌 경우 되돌아가기
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log("Raycast hit: " + hit.transform.name); // Raycast가 충돌한 오브젝트 로그 출력
                    if (hit.transform != laptopObject) // 노트북이 아닌 다른 오브젝트 클릭 시
                    {
                        Debug.Log("Outside laptop detected, resetting camera."); // 노트북 바깥을 클릭했음을 로그로 확인
                        ResetCamera();
                    }
                }
                else
                {
                    Debug.Log("Raycast did not hit any object."); // Ray가 어떤 오브젝트도 감지하지 못했을 때
                    Debug.Log("Outside laptop detected, resetting camera."); // 노트북 바깥을 클릭했음을 로그로 확인
                        ResetCamera();
                }
            }
        }
    }

    void ResetCamera()
    {
        isHoveringTriggered = false; // 다시 원래 상태로 설정
        if (cameraOrbitScript != null)
        {
            cameraOrbitScript.enabled = true; // CameraOrbit 활성화
        }
    }
}
