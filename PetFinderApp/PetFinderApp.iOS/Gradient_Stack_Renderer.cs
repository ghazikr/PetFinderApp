using CoreAnimation;
using CoreGraphics;
using PetFinderApp;
using PetFinderApp.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Gradient_Stack), typeof(Gradient_Stack_Renderer))]
namespace PetFinderApp.iOS
{
    public class Gradient_Stack_Renderer : VisualElementRenderer<StackLayout>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            Gradient_Stack stack = (Gradient_Stack)this.Element;

            CGColor startColor = stack.StartColor.ToCGColor();
            CGColor endColor = stack.EndColor.ToCGColor();
            var gradientLayer = new CAGradientLayer()
            {
                StartPoint = new CGPoint(0, 0.5),
                EndPoint = new CGPoint(1, 0.5)
            };
            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor };
            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}