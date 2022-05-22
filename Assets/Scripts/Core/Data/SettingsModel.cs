using Zenject;
using UniRx;
namespace Core.Data
{
   public class SettingsModel {
      public ReactiveProperty<bool> MuteAudio {get; private set;}

      [Inject]
      public SettingsModel() {
         MuteAudio = new ReactiveProperty<bool>(false);
      }
   }
}
