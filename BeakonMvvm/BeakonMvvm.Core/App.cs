using MvvmCross.Platform.IoC;

namespace BeakonMvvm.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart < ViewModels.WelcomeViewModel>();
          //RegisterAppStart<ViewModels.RecievedRequestViewModel>();
        }
    }
}
