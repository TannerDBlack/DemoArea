namespace NodeDemo
{
    public interface GraphicBarElement
    {
        int GetValue();
        void SetValue(int val);
        bool NeedsUpdate();
        void Draw();
    }

}