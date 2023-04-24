using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool attack;
		public bool itemGain;
		public bool bowNArrow;
		public bool axe;
		public bool item3;
		public bool IsAiming;
		public bool IsShooting;


		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAttack(InputValue value)
        {
			AttackInput(value.isPressed);
        }

		public void OnGainItem(InputValue value)
        {
			ItemGainInput(value.isPressed);
        }


		public void OnSwapItem1(InputValue value)
        {
			SwapItem1Input(value.isPressed);
        }

		public void OnSwapItem2(InputValue value)
        {
			SwapItem2Input(value.isPressed);
        }

		public void OnSwapItem3(InputValue value)
        {
			SwapItem3Input(value.isPressed);
        }

		public void OnAiming(InputValue value)
        {
			IsAiming = value.isPressed;
        }

		public void OnShoot(InputValue value)
        {
			IsShooting = value.isPressed;
        }
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AttackInput(bool newAttackState)
        {
			attack = newAttackState;
        }

		public void ItemGainInput(bool newItemGainState)
        {
			itemGain = newItemGainState;
        }

		public void SwapItem1Input(bool newSwapItem1State)
        {
			bowNArrow = newSwapItem1State;
        }

		public void SwapItem2Input(bool newSwapItem2State)
        {
			axe = newSwapItem2State;
        }

		public void SwapItem3Input(bool newSwapItem3State)
        {
			item3 = newSwapItem3State;
        }

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}