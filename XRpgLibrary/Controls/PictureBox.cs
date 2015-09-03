using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XRpgLibrary.Controls {
    public class PictureBox : Control {
        public Texture2D Image { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Rectangle DesitnationRectangle { get; set; }

        public PictureBox(Texture2D image, Rectangle desitnationRectangle) {
            Image = image;
            DesitnationRectangle = desitnationRectangle;
            SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
        }

        public PictureBox(Texture2D image, Rectangle sourceRectangle, Rectangle desitnationRectangle) {
            Image = image;
            SourceRectangle = sourceRectangle;
            DesitnationRectangle = desitnationRectangle;
            Color = Color.White;
        }

        public override void Update(GameTime gameTime) {}

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Image, DesitnationRectangle, SourceRectangle, Color);
        }

        public override void HandleInput(PlayerIndex playerIndex) {}

        public void SetPosition(Vector2 newPosition) {
            DesitnationRectangle = new Rectangle(
                (int) newPosition.X,
                (int) newPosition.Y,
                SourceRectangle.Width,
                SourceRectangle.Height);
        }
    }
}