using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RoomIcon : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	public float minCameraSize = 15f;
	public float maxCameraSize = 30f;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() {
		GameObject mainCamera = GameObject.FindWithTag("MainCamera");
		float cameraSize = mainCamera.GetComponent<Camera>().orthographicSize;
		float opacity = Mathf.Clamp((cameraSize - minCameraSize) / (maxCameraSize - minCameraSize), 0f, 1f);
		spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
	}

}
