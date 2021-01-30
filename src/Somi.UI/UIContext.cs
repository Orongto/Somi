namespace Somi.UI
{
    public class EmptyUIElement : UIElement
    {
        
    }
    
    public static class UIContext
    {
        public static UIElement Root = new EmptyUIElement();
    }
}