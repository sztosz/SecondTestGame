using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XRpgLibrary.TileEngine {
    public class Camera {
        private Vector2 _position;
        private float _speed;
        private readonly float _zoom;
        private Rectangle _viewportRectangle;

        public Vector2 Position {
            get { return _position; }
            set { _position = value; }
        }

        public float Speed {
            get { return _speed; }
            set {
//                _speed = value;
                _speed = (float) MathHelper.Clamp(_speed, 1f, 16f);
            }
        }

        public float Zoom => _zoom;

        public Camera(Rectangle viewportRectangle) {
            _speed = 4f;
            _zoom = 1f;
            _viewportRectangle = viewportRectangle;
        }

        public Camera(Rectangle viewportRectangle, Vector2 position) {
            _speed = 4f;
            _zoom = 1f;
            _viewportRectangle = viewportRectangle;
            Position = position;
        }

        public void Update(GameTime gameTime) {
            var motion = Vector2.Zero;

            if (InputHandler.KeyDown(Keys.Left)) {
                motion.X -= Speed;
            }
            else if (InputHandler.KeyDown(Keys.Right)) {
                motion.X += Speed;
            }
            if (InputHandler.KeyDown(Keys.Up)) {
                motion.Y -= Speed;
            }
            else if (InputHandler.KeyDown(Keys.Down)) {
                motion.Y += Speed;
            }

            if (motion != Vector2.Zero) {
                motion.Normalize();
            }

            _position += motion * Speed;

            LockCmaera();
        }

        private void LockCmaera() {
            _position.X = MathHelper.Clamp(Position.X, 0, TileMap.WidthInPixels - _viewportRectangle.Width);
            _position.Y = MathHelper.Clamp(Position.Y, 0, TileMap.HeightInPixels - _viewportRectangle.Height);
        }
    }
}