using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ab1Analyzer.Visualizer.Views
{
    /// <summary>
    /// NumericUpDown.xaml の相互作用ロジック
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        /// <summary>
        /// <see cref="Increment"/>を表します。
        /// </summary>
        public static DependencyProperty IncrementProperty;

        /// <summary>
        /// <see cref="Maximum"/>を表します。
        /// </summary>
        public static DependencyProperty MaximumProperty;

        /// <summary>
        /// <see cref="Minimum"/>を表します。
        /// </summary>
        public static DependencyProperty MinimumProperty;

        /// <summary>
        /// <see cref="Value"/>を表します。
        /// </summary>
        public static DependencyProperty ValueProperty;

        static NumericUpDown()
        {
            IncrementProperty = DependencyProperty.Register(nameof(Increment),
                typeof(int),
                typeof(NumericUpDown),
                new PropertyMetadata(1));
            MaximumProperty = DependencyProperty.Register(nameof(Maximum),
                typeof(int),
                typeof(NumericUpDown),
                new PropertyMetadata(int.MaxValue, OnMaximumChanged));
            MinimumProperty = DependencyProperty.Register(nameof(Minimum),
                typeof(int),
                typeof(NumericUpDown),
                new PropertyMetadata(int.MinValue, OnMinimumChanged));
            ValueProperty = DependencyProperty.Register(nameof(Value),
                typeof(int),
                typeof(NumericUpDown),
                new PropertyMetadata(0));
        }

        /// <summary>
        /// 格納する値を取得または設定します。
        /// </summary>
        public int Increment
        {
            get => (int)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }

        /// <summary>
        /// 格納する値を取得または設定します。
        /// </summary>
        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// 格納する値を取得または設定します。
        /// </summary>
        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// 格納する値を取得または設定します。
        /// </summary>
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set
            {
                if (value < Minimum) value = Minimum;
                if (value > Maximum) value = Maximum;
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// <see cref="NumericUpDown"/>の新しいインスタンスを初期化します。
        /// </summary>
        public NumericUpDown()
        {
            InitializeComponent();
        }

        /// <summary>
        /// <see cref="Maximum"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="d">変更が起きたオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (NumericUpDown)d;
            if (sender.Value > sender.Maximum) sender.Value = sender.Maximum;
        }

        /// <summary>
        /// <see cref="Minimum"/>が変更されたときに実行されます。
        /// </summary>
        /// <param name="d">変更が起きたオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (NumericUpDown)d;
            if (sender.Value < sender.Minimum) sender.Value = sender.Minimum;
        }

        /// <summary>
        /// <see cref="upButton"/>がクリックされたときに実行されます。
        /// </summary>
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            Value += Increment;
        }

        /// <summary>
        /// <see cref="downButton"/>がクリックされたときに実行されます。
        /// </summary>
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            Value -= Increment;
        }

        /// <summary>
        /// マウスホイールを受けたときに実行されます。
        /// </summary>
        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Value += Increment * Math.Sign(e.Delta);
        }
    }
}
