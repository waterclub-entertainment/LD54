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
		opacity = opacity * 0.7f + 0.3f;
		var oldColor = spriteRenderer.color;
		spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, opacity);
	}

}
