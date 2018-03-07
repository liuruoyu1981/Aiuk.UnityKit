namespace AiukUnityRuntime
{
    public abstract class AiukAbsGUIElement
    {
        public int StartX { get; protected set; }

        public int StartY { get; protected set; }

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public AiukAbsGUIElement(int x, int y, int w, int h)
        {
            StartX = x;
            StartY = y;
            Width = w;
            Height = h;
        }

        public abstract void Draw();

    }
}


