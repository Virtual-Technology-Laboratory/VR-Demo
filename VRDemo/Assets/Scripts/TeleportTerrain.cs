//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: An area that the player can teleport to
//
//=============================================================================

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class TeleportTerrain : TeleportMarkerBase
	{
		//Public properties
		public Bounds meshBounds { get; private set; }

		private bool highlighted = false;

		//-------------------------------------------------
		public void Awake()
		{

			CalculateBounds();
		}


		//-------------------------------------------------
		public void Start()
		{
			
		}


		//-------------------------------------------------
		public override bool ShouldActivate( Vector3 playerPosition )
		{
			return true;
		}


		//-------------------------------------------------
		public override bool ShouldMovePlayer()
		{
			return true;
		}


		//-------------------------------------------------
		public override void Highlight( bool highlight )
		{
			
		}


		//-------------------------------------------------
		public override void SetAlpha( float tintAlpha, float alphaPercent )
		{
			
		}


		//-------------------------------------------------
		public override void UpdateVisuals()
		{
			
		}


		//-------------------------------------------------
		public void UpdateVisualsInEditor()
		{
			
		}


		//-------------------------------------------------
		private bool CalculateBounds()
		{
			MeshFilter meshFilter = GetComponent<MeshFilter>();
			if ( meshFilter == null )
			{
				return false;
			}

			Mesh mesh = meshFilter.sharedMesh;
			if ( mesh == null )
			{
				return false;
			}

			meshBounds = mesh.bounds;
			return true;
		}
	}
}
