#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

using System.Collections.Generic;
using UnityEngine;

namespace PunctualSolutions.Samples
{
    /// <summary>
    /// Utility Free Camera component.
    /// </summary>
    public class FreeCamera : MonoBehaviour
    {
        const float MouseSensitivityMultiplier = 0.01f;

        /// <summary>
        /// Rotation speed when using a controller.
        /// </summary>
        public float lookSpeedController = 120f;

        /// <summary>
        /// Rotation speed when using the mouse.
        /// </summary>
        public float lookSpeedMouse = 4.0f;

        /// <summary>
        /// Movement speed.
        /// </summary>
        public float moveSpeed = 10.0f;

        /// <summary>
        /// Value added to the speed when incrementing.
        /// </summary>
        public float moveSpeedIncrement = 2.5f;

        /// <summary>
        /// Scale factor of the turbo mode.
        /// </summary>
        public float turbo = 10.0f;

#if ENABLE_LEGACY_INPUT_MANAGER
        static string _mouseX      = "Mouse X";
        static string kMouseY      = "Mouse Y";
        static string kRightStickX = "Controller Right Stick X";

        static string kRightStickY = "Controller Right Stick Y";
        //static string kVertical = "Vertical"; //Arrows used to switch Samples in the SamplesShowcase script
        //static string kHorizontal = "Horizontal"; //Arrows used to switch Samples in the SamplesShowcase script

        static string YAxis      = "YAxis";
        static string kSpeedAxis = "Speed Axis";
#endif

#if ENABLE_INPUT_SYSTEM
        InputAction _lookAction;
        InputAction _moveAction;
        InputAction _speedAction;
        InputAction _yMoveAction;
#endif

        void OnEnable()
        {
            RegisterInputs();
        }

        void RegisterInputs()
        {
#if ENABLE_INPUT_SYSTEM
            var map = new InputActionMap("Free Camera");

            _lookAction  = map.AddAction("look",  binding: "<Mouse>/delta");
            _moveAction  = map.AddAction("move",  binding: "<Gamepad>/leftStick");
            _speedAction = map.AddAction("speed", binding: "<Gamepad>/dpad");
            _yMoveAction = map.AddAction("yMove");

            _lookAction.AddBinding("<Gamepad>/rightStick").WithProcessor("scaleVector2(x=15, y=15)");
            _moveAction.AddCompositeBinding("Dpad").With("Up", "<Keyboard>/w")
                       //.With("Up", "<Keyboard>/upArrow") //Used to switch Samples in the SamplesShowcase script
                      .With("Down", "<Keyboard>/s")
                       //.With("Down", "<Keyboard>/downArrow") //Used to switch Samples in the SamplesShowcase script
                      .With("Left", "<Keyboard>/a")
                       //.With("Left", "<Keyboard>/leftArrow") //Used to switch Samples in the SamplesShowcase script
                      .With("Right", "<Keyboard>/d");
            //.With("Right", "<Keyboard>/rightArrow"); //Used to switch Samples in the SamplesShowcase script
            _speedAction.AddCompositeBinding("Dpad").With("Up", "<Keyboard>/home").With("Down", "<Keyboard>/end");
            _yMoveAction.AddCompositeBinding("Dpad").With("Up", "<Keyboard>/pageUp").With("Down", "<Keyboard>/pageDown")
                       .With("Up",   "<Keyboard>/e").With("Down", "<Keyboard>/q").With("Up", "<Gamepad>/rightshoulder")
                       .With("Down", "<Gamepad>/leftshoulder");

            _moveAction.Enable();
            _lookAction.Enable();
            _speedAction.Enable();
            _yMoveAction.Enable();
#endif

#if UNITY_EDITOR && ENABLE_LEGACY_INPUT_MANAGER
            var inputEntries = new List<InputManagerEntry>
                               {
                                   // Add new bindings
                                   new()
                                   {
                                       name        = kRightStickX,
                                       kind        = InputManagerEntry.Kind.Axis,
                                       axis        = InputManagerEntry.Axis.Fourth,
                                       sensitivity = 1.0f, gravity = 1.0f,
                                       deadZone    = 0.2f,
                                   },
                                   new()
                                   {
                                       name        = kRightStickY,
                                       kind        = InputManagerEntry.Kind.Axis,
                                       axis        = InputManagerEntry.Axis.Fifth,
                                       sensitivity = 1.0f, gravity = 1.0f,
                                       deadZone    = 0.2f, invert  = true,
                                   },
                                   new()
                                   {
                                       name           = YAxis,
                                       kind           = InputManagerEntry.Kind.KeyOrButton,
                                       btnPositive    = "page up",
                                       altBtnPositive = "joystick button 5",
                                       btnNegative    = "page down",
                                       altBtnNegative = "joystick button 4",
                                       gravity        = 1000.0f, deadZone = 0.001f,
                                       sensitivity    = 1000.0f,
                                   },
                                   new()
                                   {
                                       name        = YAxis,
                                       kind        = InputManagerEntry.Kind.KeyOrButton,
                                       btnPositive = "q",
                                       btnNegative = "e", gravity        = 1000.0f,
                                       deadZone    = 0.001f, sensitivity = 1000.0f,
                                   },
                                   new()
                                   {
                                       name        = kSpeedAxis,
                                       kind        = InputManagerEntry.Kind.KeyOrButton,
                                       btnPositive = "home",
                                       btnNegative = "end", gravity      = 1000.0f,
                                       deadZone    = 0.001f, sensitivity = 1000.0f,
                                   },
                                   new()
                                   {
                                       name        = kSpeedAxis,
                                       kind        = InputManagerEntry.Kind.Axis,
                                       axis        = InputManagerEntry.Axis.Seventh,
                                       gravity     = 1000.0f, deadZone = 0.001f,
                                       sensitivity = 1000.0f,
                                   },
                               };

            InputRegistering.RegisterInputs(inputEntries);
#endif
        }

        float _inputRotateAxisX, _inputRotateAxisY;
        float _inputChangeSpeed;
        float _inputVertical,  _inputHorizontal, _inputYAxis;
        bool  _leftShiftBoost, _leftShift,       _fire1;

        void UpdateInputs()
        {
            _inputRotateAxisX = 0.0f;
            _inputRotateAxisY = 0.0f;
            _leftShiftBoost   = false;
            _fire1            = false;

#if ENABLE_INPUT_SYSTEM
            var lookDelta = _lookAction.ReadValue<Vector2>();
            _inputRotateAxisX = lookDelta.x * lookSpeedMouse * MouseSensitivityMultiplier;
            _inputRotateAxisY = lookDelta.y * lookSpeedMouse * MouseSensitivityMultiplier;

            _leftShift = Keyboard.current?.leftShiftKey?.isPressed ?? false;
            _fire1     = Mouse.current?.leftButton?.isPressed == true || Gamepad.current?.xButton?.isPressed == true;

            _inputChangeSpeed = _speedAction.ReadValue<Vector2>().y;

            var moveDelta = _moveAction.ReadValue<Vector2>();
            _inputVertical   = moveDelta.y;
            _inputHorizontal = moveDelta.x;
            _inputYAxis      = _yMoveAction.ReadValue<Vector2>().y;
            
#else
            if (Input.GetMouseButton(1))
            {
                leftShiftBoost = true;
                inputRotateAxisX = Input.GetAxis(mouseX) * LookSpeedMouse;
                inputRotateAxisY = Input.GetAxis(kMouseY) * LookSpeedMouse;
            }
            inputRotateAxisX += (Input.GetAxis(kRightStickX) * LookSpeedController * MouseSensitivityMultiplier);
            inputRotateAxisY += (Input.GetAxis(kRightStickY) * LookSpeedController * MouseSensitivityMultiplier);

            leftShift = Input.GetKey(KeyCode.LeftShift);
            fire1 = Input.GetAxis("Fire1") > 0.0f;

            inputChangeSpeed = Input.GetAxis(kSpeedAxis);

            //Because arrows from the axis controls are taken to switch samples
            inputVertical = Input.GetKey(KeyCode.S) ? - 1 : 0;
            inputVertical = Input.GetKey(KeyCode.W) ? 1 : inputVertical;
            inputHorizontal = Input.GetKey(KeyCode.A) ? - 1 : 0;
            inputHorizontal = Input.GetKey(KeyCode.D) ? 1 : inputHorizontal;

            inputYAxis = Input.GetAxis(YAxis);
#endif
        }

        void Update()
        {
            UpdateInputs();

            if (_inputChangeSpeed != 0.0f)
            {
                moveSpeed += _inputChangeSpeed * moveSpeedIncrement;
                if (moveSpeed < moveSpeedIncrement) moveSpeed = moveSpeedIncrement;
            }

            var moved = _inputRotateAxisX != 0.0f || _inputRotateAxisY != 0.0f || _inputVertical != 0.0f ||
                        _inputHorizontal  != 0.0f || _inputYAxis       != 0.0f;
            if (!moved) return;
            var rotationX    = transform.localEulerAngles.x;
            var newRotationY = transform.localEulerAngles.y + _inputRotateAxisX;

            // Weird clamping code due to weird Euler angle mapping...
            var newRotationX = (rotationX - _inputRotateAxisY);
            newRotationX = rotationX switch
            {
                <= 90.0f when newRotationX >= 0.0f => Mathf.Clamp(newRotationX, 0.0f,   90.0f),
                >= 270.0f                          => Mathf.Clamp(newRotationX, 270.0f, 360.0f),
                _                                  => newRotationX,
            };

            transform.localRotation = Quaternion.Euler(newRotationX, newRotationY, transform.localEulerAngles.z);

            var speed = Time.deltaTime * moveSpeed;
            if (_fire1 || _leftShiftBoost && _leftShift)
                speed *= turbo;
            transform.position += transform.forward * (speed * _inputVertical);
            transform.position += transform.right   * (speed * _inputHorizontal);
            transform.position += Vector3.up        * (speed * _inputYAxis);
        }
    }
}