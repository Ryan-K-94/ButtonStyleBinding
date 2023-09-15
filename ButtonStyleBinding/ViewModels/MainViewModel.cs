using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Globalization;
using System;
using System.ComponentModel;
using Avalonia;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using System.Windows.Input;

namespace ButtonStyleBinding.ViewModels;

public class MainViewModel : ViewModelBase
{
    private Matrix matrix;

    public Matrix PatchMatrix
    {
        get { return matrix; }
        set { this.RaiseAndSetIfChanged(ref matrix, value); }
    }

    public MainViewModel()
    {
        CreateMatrix();
    }

    private async void CreateMatrix()
    {
        PatchMatrix = CreateMatrixColumn(4, 4);
    }

    private Matrix CreateMatrixColumn(int satBoxChCount, int inputChCount)
    {
        var matrix = new Matrix();

        matrix.Column = new List<List<MatrixItem>>();

        for (int y = 0; y < inputChCount; y++)
        {
            matrix.Column.Add(new List<MatrixItem>());

            for (int x = 0; x < satBoxChCount; x++)
            {
                matrix.Column[y].Add(new MatrixItem() { IsAssignedTooInOut = InOut.In });
            }
        }

        return matrix;
    }
}

public enum InOut
{
    [Description("Input Patch")]
    In = 0,

    [Description("Output Patch")]
    Out = 1,
}

public class Matrix : ReactiveObject
{
    public List<List<MatrixItem>>? Column { get; set; }
}

public class MatrixItem : ReactiveObject
{
    private InOut isAssignedTooInOut;

    public InOut IsAssignedTooInOut
    {
        get { return isAssignedTooInOut; }
        set { this.RaiseAndSetIfChanged(ref isAssignedTooInOut, value); }
    }
}

public class MatrixToggleBtnColourConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch ((InOut)value)
        {
            default:
                // Should never get here.
                return Brush.Parse("#ED1A69");

            case InOut.In:
                // Solotech Pink
                return Brush.Parse("#ED1A69");

            case InOut.Out:
                // Turquoise 
                return Brush.Parse("#03A38E");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
