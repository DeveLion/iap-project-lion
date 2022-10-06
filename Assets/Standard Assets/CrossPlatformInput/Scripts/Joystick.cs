using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}
		public Image BgImg,InputImage;
		Vector3 InputVector;
		[HideInInspector]
		public bool isMoving;
//		public int MovementRange = 100;
//		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input
//
//		Vector3 m_StartPos;
//		bool m_UseX; // Toggle for using the x axis
//		bool m_UseY; // Toggle for using the Y axis
//		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
//		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

//		void OnEnable()
//		{
//			CreateVirtualAxes();
//		}

//        void Start()
//        {
//            m_StartPos = transform.position;
//        }
//
//		void UpdateVirtualAxes(Vector3 value)
//		{
//			var delta = m_StartPos - value;
//			delta.y = -delta.y;
//			delta /= MovementRange;
//			if (m_UseX)
//			{
//				m_HorizontalVirtualAxis.Update(-delta.x);
//			}
//
//			if (m_UseY)
//			{
//				m_VerticalVirtualAxis.Update(delta.y);
//			}
//		}

//		void CreateVirtualAxes()
//		{
//			// set axes to use
//			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
//			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);
//
//			// create new axes based on axes to use
//			if (m_UseX)
//			{
//				m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
//				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
//			}
//			if (m_UseY)
//			{
//				m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
//				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
//			}
//		}


		public void OnDrag(PointerEventData data)
		{
//			Vector3 newPos = Vector3.zero;
//
//			if (m_UseX)
//			{
//				int delta = (int)(data.position.x - m_StartPos.x);
//				//delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
//				newPos.x = delta;
//			}
//
//			if (m_UseY)
//			{
//				int delta = (int)(data.position.y - m_StartPos.y);
//				//delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
//				newPos.y = delta;
//			}
//			transform.position = Vector3.ClampMagnitude( new Vector3(newPos.x, newPos.y, newPos.z),MovementRange)+ m_StartPos;
//			UpdateVirtualAxes(transform.position);

			Vector2 pos;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle (BgImg.rectTransform, data.position, data.pressEventCamera, out pos)) {
				pos.x = (pos.x / BgImg.rectTransform.sizeDelta.x);
				pos.y = (pos.y / BgImg.rectTransform.sizeDelta.y);
				InputVector = new Vector3 (pos.x*2 + 1f,0,pos.y*2 - 1f);
				InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;
				//print ("wrorking"+ pos+ InputVector);
				InputImage.rectTransform.anchoredPosition = new Vector3 (InputVector.x* (BgImg.rectTransform.sizeDelta.x/3),InputVector.z* (BgImg.rectTransform.sizeDelta.y/3));
			}

		}

		public float Horizontal(){
			if (InputVector.x != 0)
				return InputVector.x;
			else
				return Input.GetAxis (horizontalAxisName);
		}

		public float Vertical(){
			if (InputVector.z != 0)
				return InputVector.z;
			else
				return Input.GetAxis (verticalAxisName);
		}


		public void OnPointerUp(PointerEventData data)
		{
//			transform.position = m_StartPos;
//			UpdateVirtualAxes(m_StartPos);
			InputVector = Vector3.zero;
			InputImage.rectTransform.anchoredPosition = Vector3.zero;
			isMoving = false;
		}


		public void OnPointerDown(PointerEventData data) {
			isMoving = true;
		}

//		void OnDisable()
//		{
//			// remove the joysticks from the cross platform input
//			if (m_UseX)
//			{
//				m_HorizontalVirtualAxis.Remove();
//			}
//			if (m_UseY)
//			{
//				m_VerticalVirtualAxis.Remove();
//			}
//		}
	}
}