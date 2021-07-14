using System.Reactive.Linq;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using System;
using System.Reactive;
using ReactiveUI;

namespace BoutiqueXamarin.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class SvgButton : SvgCachedImage
    {
        public SvgButton()
        {
            var gestureRecognizer = new TapGestureRecognizer();
            GestureRecognizers.Add(gestureRecognizer);
            Tapped = Observable.FromEventPattern(gestureRecognizer, nameof(gestureRecognizer.Tapped));
        }

        /// <summary>
        /// Событие нажатия
        /// </summary>
        public IObservable<EventPattern<object>> Tapped { get; }
    }
}