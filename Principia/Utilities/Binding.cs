using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UpdateControls;
using UpdateControls.Fields;

namespace Principia
{
    public abstract class Binding : IDisposable
    {
        public static Binding Bind<T>(Func<T> value, Action<T> assign)
        {
            return new Binding<T>(value, assign);
        }

        public abstract void Dispose();
    }

    public class Binding<T> : Binding, IUpdatable
    {
        private Dependent<T> _value;
        private Action<T> _assign;
        private T _prior;

        internal Binding(Func<T> value, Action<T> assign)
        {
            _value = new Dependent<T>(value);
            _assign = assign;
            _value.Invalidated += DependentInvalidated;
            _prior = _value.Value;
            _assign(_prior);
        }

        public override void Dispose()
        {
            _value.Invalidated -= DependentInvalidated;
            _value.Dispose();
        }

        private void DependentInvalidated()
        {
            UpdateScheduler.ScheduleUpdate(this);
        }

        public void UpdateNow()
        {
            var current = _value.Value;
            if (!Object.Equals(current, _prior))
            {
                _prior = current;
                _assign(current);
            }
        }
    }
}
