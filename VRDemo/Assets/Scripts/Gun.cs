//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: The bow
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class Gun: MonoBehaviour
	{
		private Hand hand;

		public ItemPackage itemPackage;



		//-------------------------------------------------
		private void OnAttachedToHand( Hand attachedHand )
		{
			hand = attachedHand;
		}

		//-------------------------------------------------
		private void ShutDown()
		{
			if ( hand != null && hand.otherHand.currentAttachedObject != null )
			{
				if ( hand.otherHand.currentAttachedObject.GetComponent<ItemPackageReference>() != null )
				{
					if ( hand.otherHand.currentAttachedObject.GetComponent<ItemPackageReference>().itemPackage == itemPackage )
					{
						hand.otherHand.DetachObject( hand.otherHand.currentAttachedObject );
					}
				}
			}
		}


		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
			gameObject.SetActive( false );
		}


		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
			gameObject.SetActive( true );
			OnAttachedToHand( hand );
		}


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			Destroy( gameObject );
		}


		//-------------------------------------------------
		void OnDestroy()
		{
			ShutDown();
		}
	}
}
