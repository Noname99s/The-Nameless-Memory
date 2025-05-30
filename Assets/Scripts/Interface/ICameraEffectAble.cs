namespace Orpul1.Camera
{
    public interface ICameraEffectAble
    {
        public void Execute(int type);
        public void Abort(int type);
    }
}