namespace Infrastructure
{
    public interface IPlayerInput
    {
        float Horizontal();

        float Vertical();

        float Height();

        bool ShiftPressed();

        bool RPressed();
        bool EPressed();
        bool JumpPressed();

        bool AttackPressed();
    }
}