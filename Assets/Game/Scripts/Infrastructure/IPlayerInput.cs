namespace Infrastructure
{
    public interface IPlayerInput
    {
        float Horizontal();

        float Vertical();

        float Height();

        bool RPressed();
        bool EPressed();
        bool JumpPressed();
    }
}