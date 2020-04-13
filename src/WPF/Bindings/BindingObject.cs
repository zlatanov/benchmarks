using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace WPF.Bindings
{
    public class BindingObject<T> : DependencyObject
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register( "Value", typeof( T ), typeof( BindingObject<T> ) );


        public T Value
        {
            get => (T)GetValue( ValueProperty );
            set => SetValue( ValueProperty, value );
        }


        public BindingExpressionBase SetBinding( BindingBase binding )
            => BindingOperations.SetBinding( this, ValueProperty, binding );


        [DebuggerNonUserCode]
        public void CheckValue( T expected )
        {
            if ( !EqualityComparer<T>.Default.Equals( Value, expected ) )
                throw new InvalidProgramException( $"The expected value \"{expected}\" is different than the actual \"{(Value is null ? "<<null>>" : Value.ToString())}\"." );
        }
    }
}
