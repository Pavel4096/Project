namespace Project
{
    public interface IView
    {
        GameVector GetPosition();
        void SetPosition(GameVector position);
        void SetAnimatorParameter(string name, float value);
        GameVector GetForwardDirection();
        void RotateTo(GameVector position);
    }
}
