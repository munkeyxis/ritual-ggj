using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Parallax scrolling script that should be assigned to a layer
/// </summary>
public class ScrollingScript : MonoBehaviour
{
	public static float speedCoef = 1;

	public static bool doScroll = true;
	/// <summary>
	/// Scrolling speed
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);
	
	/// <summary>
	/// Moving direction
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);
	
	/// <summary>
	/// Movement should be applied to camera
	/// </summary>
	public bool isLinkedToCamera = false;
	
	/// <summary>
	/// 1 - Background is infinite
	/// </summary>
	public bool isLooping = false;
	
	/// <summary>
	/// 2 - List of children with a renderer.
	/// </summary>
	private List<Transform> backgroundPart;
	
	// 3 - Get all the children
	void Start()
	{
		// For infinite background only
		if (isLooping)
		{
			// Get all the children of the layer with a renderer
			backgroundPart = new List<Transform>();
			
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				
				// Add only the visible children
				if (child.GetComponent<Renderer>() != null)
				{
					backgroundPart.Add(child);
				}
			}
			
			// Sort by position.
			// Note: Get the children from left to right.
			// We would need to add a few conditions to handle
			// all the possible scrolling directions.
			backgroundPart = backgroundPart.OrderBy(
				t => t.position.x
				).ToList();
		}
	}
	
	void Update()
	{
		if (!doScroll)
			return;

		int lvl = 1;

		// Movement
		Vector3 movement = new Vector3(
			speed.x * direction.x * speedCoef + ((lvl-1)*0.25f),
			speed.y * direction.y * speedCoef + ((lvl-1)*0.25f),
			0);
		
		movement *= Time.deltaTime;
		transform.Translate(movement);
		
		// Move the camera
		if (isLinkedToCamera)
		{
			Camera.main.transform.Translate(movement);
		}
		
		// 4 - Loop
		if (isLooping)
		{
			// Get the first object.
			// The list is ordered from left (x position) to right.
			// Transform firstChild = backgroundPart.FirstOrDefault();
			Transform firstChild;

			if (speedCoef < 0)
				firstChild = backgroundPart.LastOrDefault();
			else
				firstChild = backgroundPart.FirstOrDefault();

			if (firstChild != null)
			{
				// Check if the child is already (partly) before the camera.
				// We test the position first because the IsVisibleFrom
				// method is a bit heavier to execute.
				if (((speedCoef>=0)&&(firstChild.position.x < Camera.main.transform.position.x))||((speedCoef<0)&&(firstChild.position.x > Camera.main.transform.position.x)))
				{
					// If the child is already on the left of the camera,
					// we test if it's completely outside and needs to be
					// recycled.
					if (firstChild.GetComponent<Renderer>().isVisible == false)
					{
						// Get the last child position.
						Transform lastChild;
						if (speedCoef>=0)
							lastChild = backgroundPart.LastOrDefault();
						else
							lastChild = backgroundPart.FirstOrDefault();

						Vector3 lastPosition = lastChild.transform.position;
						Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);
						
						// Set the position of the recyled one to be AFTER
						// the last child.
						// Note: Only work for horizontal scrolling currently.
						if (speedCoef >= 0)
							firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);
						else
							firstChild.position = new Vector3(lastPosition.x - lastSize.x, firstChild.position.y, firstChild.position.z);

						// Set the recycled child to the last position
						// of the backgroundPart list.
						backgroundPart.Remove(firstChild);
						if (speedCoef >= 0)
							backgroundPart.Add(firstChild);
						else
							backgroundPart.Insert(0,firstChild);

					}
				}
			}
		}
	}
}
