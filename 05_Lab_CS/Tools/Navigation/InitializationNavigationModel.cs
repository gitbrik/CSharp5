using System;
using _05_Lab_CS.Views;
using MainView = _05_Lab_CS.Views.TaskManagerView;
using K_05_Lab_CS.Tools.Navigation;

namespace _05_Lab_CS.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {
            
        }
   
        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
      
                case ViewType.Main:
                    ViewsDictionary.Add(viewType, new MainView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
