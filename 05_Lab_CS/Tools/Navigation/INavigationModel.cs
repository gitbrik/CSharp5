namespace K_05_Lab_CS.Tools.Navigation
{
    internal enum ViewType
    {
        AddUser,
        DeleteUser,
        Main
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
