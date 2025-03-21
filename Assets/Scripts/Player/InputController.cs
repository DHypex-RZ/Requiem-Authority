using UnityEngine;

namespace Player
{
	public class InputController: MonoBehaviour
	{
		internal static float HorizontalInput { get; private set; }
		internal static bool IsPressedSpace { get; private set; }
		internal static bool IsPressedShift { get; private set; }
		internal static bool IsPressedCtrl { get; private set; }
		internal static bool IsPressedQ { get; private set; }
		internal static bool IsPressedW { get; private set; }
		internal static bool IsPressedE { get; private set; }
		internal static bool IsPressedF { get; private set; }
		internal static bool IsClickedLeft { get; private set; }
		internal static bool IsClickedRight { get; private set; }

		private void Update()
		{
			HorizontalInput = Input.GetAxis("Horizontal");
			IsPressedSpace = Input.GetKey(KeyCode.Space);
			IsPressedShift = Input.GetKey(KeyCode.LeftShift);
			IsPressedCtrl = Input.GetKey(KeyCode.LeftControl);
			IsPressedF = Input.GetKey(KeyCode.F);
			IsPressedQ = Input.GetKey(KeyCode.Q);
			IsPressedW = Input.GetKey(KeyCode.W);
			IsPressedE = Input.GetKey(KeyCode.E);
			IsClickedLeft = Input.GetMouseButton(0);
			IsClickedRight = Input.GetMouseButton(1);
		}
	}
}