namespace UnityEngine.UI.Extensions
{
    internal interface IHorizontalScrollSnap
    {
        void ChangePage(int page);
        void SetLerp(bool value);
        int CurrentPage();
        void StartScreenChange();
    }
}