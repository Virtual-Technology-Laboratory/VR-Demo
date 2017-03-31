using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

	public float thickness = 0.01f;
	private Transform arcObjectsTransform;
	private LineRenderer lineRenderer;
	public Material material;
	public bool showing = false;

	[HideInInspector]
	public int traceLayerMask = 0;

	private float prevThickness = 0.0f;
	private Vector3 startPos;

	private void CreateLineRendererObjects() {
		//Destroy any existing line renderer objects
		if ( arcObjectsTransform != null )
		{
			Destroy( arcObjectsTransform.gameObject );
		}

		GameObject arcObjectsParent = new GameObject( "ArcObjects" );
		arcObjectsTransform = arcObjectsParent.transform;
		arcObjectsTransform.SetParent( this.transform );
		arcObjectsTransform.position = new Vector3 ();

		//Create new line renderer objects
		GameObject newObject = new GameObject( "LineRenderer");
		newObject.transform.SetParent( arcObjectsTransform );
		lineRenderer = newObject.AddComponent<LineRenderer>();
		lineRenderer.receiveShadows = false;
		lineRenderer.useWorldSpace = true;
		lineRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
		lineRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
		lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		lineRenderer.material = material;
		lineRenderer.startWidth = thickness;
		lineRenderer.endWidth = thickness;
		lineRenderer.numPositions = 2;
		//lineRenderer.enabled = false;
	}

	private Vector3 FindProjectileCollision( out RaycastHit hitInfo, Transform t ) {
		hitInfo = new RaycastHit();
		Ray raycast = new Ray (t.position, t.forward);
		bool bHit = Physics.Raycast (raycast, out hitInfo);

		if (bHit) {
			return t.position + t.forward * hitInfo.distance;
		}

		return t.position + t.forward * 100;
	}

	public void DrawLine( out RaycastHit hitInfo, Transform t ) {
		if (lineRenderer == null)
			ShowLine ();
		lineRenderer.SetPosition (0, t.position);
		lineRenderer.SetPosition (1, FindProjectileCollision (out hitInfo, t));
	}

	public void ShowLine() {
		if (showing)
			return;
		CreateLineRendererObjects ();
		showing = true;
	}

	public void DestroyLine() {
		if (!showing)
			return;
		Destroy (lineRenderer);
		showing = false;
	}

	public void SetColor(Color color) {
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
	}

	public Vector3 EndPos() {
		if (lineRenderer != null) {
			return lineRenderer.GetPosition (1);
		} else
			return new Vector3 ();
	}
}
