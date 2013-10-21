using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zombie.Defense.Ui
{
    public class Camera
    {
        public enum CameraMode
        {
            free = 0            
        }
        public CameraMode currentCameraMode = CameraMode.free;

        private Vector3 position;
        private Vector3 desiredPosition;
        private Vector3 target;
        private Vector3 desiredTarget;
        private Vector3 offsetDistance;

        private float yaw, pitch, roll;
        private float speed;

        private Matrix cameraRotation;
        public Matrix viewMatrix, projectionMatrix;

        public Camera()
        {
            ResetCamera();
        }

        public void ResetCamera()
        {
            position = new Vector3(360, -360, 600);
            desiredPosition = position;
            target = new Vector3();
            desiredTarget = target;

            offsetDistance = new Vector3(0, 0, 50);

            yaw = 0.0f;
            pitch = 0.0f;
            roll = 0.0f;

            speed = 2f;

            cameraRotation = Matrix.Identity;
            viewMatrix = Matrix.Identity;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), 16 / 9, .5f, 10000f);
        }

        public void Update()
        {
            HandleInput();
            UpdateViewMatrix();
        }

        private void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //Rotate Camera
            if (keyboardState.IsKeyDown(Keys.J))
            {                
                    yaw += 1f;                
            }
            if (keyboardState.IsKeyDown(Keys.L))
            {                                
                    yaw += -1f;                
            }
            if (keyboardState.IsKeyDown(Keys.I))
            {                                
                    pitch += 1f;                
            }
            if (keyboardState.IsKeyDown(Keys.K))
            {

                    pitch += -1f;                
            }
            if (keyboardState.IsKeyDown(Keys.U))
            {
                roll += -.02f;
            }
            if (keyboardState.IsKeyDown(Keys.O))
            {
                roll += .02f;
            }

            //Move Camera
            if (currentCameraMode == CameraMode.free)
            {
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    MoveCamera(cameraRotation.Forward);
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    MoveCamera(-cameraRotation.Forward);
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    MoveCamera(-cameraRotation.Right);
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    MoveCamera(cameraRotation.Right);
                }
                if (keyboardState.IsKeyDown(Keys.E))
                {
                    MoveCamera(cameraRotation.Up);
                }
                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    MoveCamera(-cameraRotation.Up);
                }
            }
        }

        private void MoveCamera(Vector3 addedVector)
        {
            position += speed * addedVector;
        }

        private void UpdateViewMatrix()
        {
            switch (currentCameraMode)
            {
                case CameraMode.free:

                    cameraRotation.Forward.Normalize();
                    cameraRotation.Up.Normalize();
                    cameraRotation.Right.Normalize();

                    cameraRotation *= Matrix.CreateFromAxisAngle(cameraRotation.Right, pitch);
                    cameraRotation *= Matrix.CreateFromAxisAngle(cameraRotation.Up, yaw);
                    cameraRotation *= Matrix.CreateFromAxisAngle(cameraRotation.Forward, roll);

                    yaw = 0.0f;
                    pitch = 0.0f;
                    roll = 0.0f;

                    target = position + cameraRotation.Forward;

                    break;
            }

            //We'll always use this line of code to set up the View Matrix.
            viewMatrix = Matrix.CreateLookAt(position, target, cameraRotation.Up);
        }
    }
}
